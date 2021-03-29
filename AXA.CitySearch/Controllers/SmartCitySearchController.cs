namespace AXA.CitySearch.Controllers
{
    using AXA.CitySearch.Interface;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    [Route("api/[controller]")]
    [ApiController]
    public class SmartCitySearchController : ControllerBase
    {
        private readonly ICityFinder cityFinderService;

        /// <summary>
        /// SmartCitySearch constructor
        /// </summary>
        /// <param name="cityFinderService"></param>
        public SmartCitySearchController(ICityFinder cityFinderService)
        {
            this.cityFinderService = cityFinderService;
        }

        /// <summary>
        /// Gets the Cities names on a search
        /// </summary>
        /// <returns>Returns a list cities and next letter for an search.</returns>
        /// <response code="400">If the request is invalid, a 400 response is returned.</response>
        /// <response code="200">If the operation entirely succeeds, a 200 response is returned.</response>
        /// <response code="404">If the entity with the specified key does not exist, a 404 response is returned.</response>
        /// <response code="500">If the service experienced an internal error, a 500 response is returned.</response>
        /// <response code="503">If the service circuit breaker is in place such that further calls should be blocked, a 503
        [ProducesResponseType(typeof(ICityResult), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(503)]
        [HttpGet("CitySearch")]
        public IActionResult CitySearch(string city)
        {
            // Validate searchString
            if (city.ToCharArray().Count() < 1)
            {
                return this.BadRequest();
            }

            var result = cityFinderService.Search(city);

            // Validate that the NextCities exists
            if (!result.NextCities.Any())
            {
                return this.NotFound();
            }

            return Ok(result);

        }


    }
}
