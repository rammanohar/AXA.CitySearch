/// <summary>
/// ControllerContextExtensions
/// </summary>
namespace AXA.CitySearch.Tests.Context
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Net.Http;


    /// <summary>
    /// Extensions for the <see cref="ControllerContext"/> class.
    /// </summary>
    public static class ControllerContextExtensions
    {
        /// <summary>
        /// Creates the in memory server and client for controller tests.
        /// </summary>
        /// <typeparam name="TStartup">The startup class for the server.</typeparam>
        /// <param name="controllerContext">The controller context.</param>
        public static void CreateInMemoryServerAndClient<TStartup>(this ControllerContext controllerContext)
            where TStartup : class
        {
            controllerContext.CreateInMemoryServerAndClient<TStartup>(null);
        }


        /// <summary>
        /// Creates the in memory server and client for controller tests.
        /// </summary>
        /// <typeparam name="TStartup">The startup class for the server.</typeparam>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="configure">Action to add configuration providers to the server.</param>
        public static void CreateInMemoryServerAndClient<TStartup>(this ControllerContext controllerContext, Action<IConfigurationBuilder> configure)
            where TStartup : class
        {
            TestServer tempServer = null;
            HttpClient tempClient = null;

            if (controllerContext == null)
            {
                throw new ArgumentNullException(nameof(controllerContext));
            }

            try
            {
                WebHostBuilder builder = new WebHostBuilder();
                if (configure != null)
                {
                    builder.ConfigureAppConfiguration(configure);
                }

                builder.UseStartup<TStartup>();
                
                tempServer = new TestServer(builder);
                tempClient = tempServer.CreateClient();
                controllerContext.TestServer = tempServer;
                controllerContext.TestClient = tempClient;
                tempServer = null;
                tempClient = null;
            }
            finally
            {
                if (tempServer != null)
                {
                    tempServer.Dispose();
                }

                if (tempClient != null)
                {
                    tempClient.Dispose();
                }
            }
        }
        private static string GetEnvironment()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }
    }
}
