/// <summary>
/// CoreStepDefinitions
/// </summary>

namespace AXA.CitySearch.Tests.StepDefinitions
{
    using AXA.CitySearch.Service;
    using AXA.CitySearch.Tests.Context;
    using FluentAssertions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using TechTalk.SpecFlow;
    [Binding]
    public class CoreStepDefinitions
    {
        private readonly ControllerContext controllerContext;

        /// <summary>Initializes a new instance of the <see cref="CoreStepDefinitions"/> class.</summary>
        /// <param name="controllerContext">The controller Context.</param>
        public CoreStepDefinitions(ControllerContext controllerContext)
        {
            this.controllerContext = controllerContext;
        }

        /// <summary>
        /// Given the i am running pus inmemory configured for test authentication.
        /// </summary>
        [Given(@"I am running in-memory configured for Test Authentication")]
        public void GivenIAmRunningIn_MemoryConfiguredForTestAuthentication()
        {
            // Create test service and client
            this.controllerContext.CreateInMemoryServerAndClient<Startup>();
        }



        /// <summary>
        /// Given I am user 'x' who is a 'comment'.
        /// </summary>
        /// <param name="upn">The upn.</param>
        /// <param name="comment">The comment for the user.</param>
        [Given(@"I am user '(.*)' who is a '(.*)'")]
        public void GivenIAmUserWhoIsA(string upn, string comment)
        {
            if (string.IsNullOrEmpty(comment))
            {
                throw new ArgumentNullException(nameof(comment));
            }

        }

        /// <summary>
        /// When I send an HTTP Request
        /// </summary>
        /// <param name="method">The request HTTP method.</param>
        /// <param name="uri">The request URI.</param>
        /// <returns>Task.</returns>
        [When(@"I '(.*)' the API '(.*)'")]
        public async Task WhenITheAPI(HttpMethod method, Uri uri)
        {

            using (var request = new HttpRequestMessage(method, uri))
            {
                if (!string.IsNullOrEmpty(this.controllerContext.Payload))
                {
                    request.Content = new StringContent(this.controllerContext.Payload, Encoding.UTF8, "application/json");
                }

                this.controllerContext.Response = await this.controllerContext.TestClient.SendAsync(request);
            }
        }

        /// <summary>
        /// Then I receive HTTP status code 'n'.
        /// </summary>
        /// <param name="expectedStatusCode">The expected status code.</param>
        [Then(@"I receive HTTP status code '(.*)'")]
        public void ThenIReceiveHTTPStatusCode(HttpStatusCode expectedStatusCode)
        {
            this.controllerContext.Response.StatusCode
                .Should().Be(expectedStatusCode);
        }

        [Then(@"the response contains '(.*)' and '(.*)'")]
        public async Task ThenTheResponseContainsAndAsync(string cities, string letters)
        {
           
            var cityResult = await this.controllerContext.CastResponseAs<CityResult>();
            var expectedcities = new HashSet<string>(cities.ToLower().Split(';'));
            HashSet<string> expectedletters = new HashSet<string>();
            if (!string.IsNullOrEmpty(letters))
                expectedletters = new HashSet<string>(letters.ToLower().Split(';'));

            cityResult.NextCities.Select(c=>c.ToLower()).Should().BeEquivalentTo(expectedcities, options => options.WithStrictOrdering());
            cityResult.NextLetters.Should().BeEquivalentTo(expectedletters, options => options.WithStrictOrdering());
        }

    }
}
