/// <summary>
/// CityFinderService
/// </summary>
namespace AXA.CitySearch.Service
{
    using AXA.CityData;
    using AXA.CitySearch.Interface;
    using System.Collections.Generic;
    using System.Linq;

    public class CityFinderService : ICityFinder
    {
		HashSet<string> sourceData;
		private readonly ICityResult cityResult;
		public CityFinderService(ICityResult cityResult)
		{
			// initialize source data
			sourceData = new HashSet<string>(new CityNames().cities);
			this.cityResult = cityResult;
		}
		public ICityResult Search(string searchString)
		{
			
			// normalize search string to lowercase
			string searchStringNormalized = searchString.ToLower();
			int nexCharPosition = searchString.Length;

			var result = sourceData.Where(x => x.ToLower().StartsWith(searchStringNormalized)).ToList();
			var chars = result.Where(r => r.ToCharArray().Count() > nexCharPosition).Select(c=> c.ToCharArray().ElementAt(nexCharPosition).ToString().ToLower()).Distinct().ToList();
			cityResult.NextCities = result;
			cityResult.NextLetters = chars;

			return cityResult;
		}
	}
}
