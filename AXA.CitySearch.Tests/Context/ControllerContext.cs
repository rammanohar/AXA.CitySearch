/// <summary>
/// ControllerContext
/// </summary>
namespace AXA.CitySearch.Tests.Context
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.TestHost;
    using Newtonsoft.Json;

    /// <summary>
    /// The context for any controller tests
    /// </summary>
    public class ControllerContext : IDisposable
    {
        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Finalizes an instance of the <see cref="ControllerContext"/> class.
        /// </summary>
        ~ControllerContext()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets or sets the test server.
        /// </summary>
        public TestServer TestServer { get; set; }

        /// <summary>
        /// Gets or sets the test client.
        /// </summary>
        public HttpClient TestClient { get; set; }

        /// <summary>
        /// Gets or sets the payload to be included in a request body.
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// Gets or sets the response from the last call to the controller.
        /// </summary>
        public HttpResponseMessage Response { get; set; }

        
        /// <summary>
        /// Casts the content of the HttpResponseMessage instance to an instance of the desired type.
        /// </summary>
        /// <typeparam name="T">The type to cast to.</typeparam>
        /// <returns>An instance of type T.</returns>
        public async Task<T> CastResponseAs<T>()
        {
            if (Response?.Content != null)
            {
                return JsonConvert.DeserializeObject<T>(await Response.Content.ReadAsStringAsync());
            }

            return default;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.TestClient?.Dispose();
                    this.TestServer?.Dispose();
                }

                disposedValue = true;
            }
        }
    }
}
