/// <summary>
/// StepTransformations
/// </summary>
namespace AXA.CitySearch.Tests.Helpers
{

    using System;
    using System.Net.Http;
    using TechTalk.SpecFlow;

    /// <summary>
    /// HTTP related step transforms.
    /// </summary>
    [Binding]
    public static class StepTransformations
    {
        /// <summary>
        /// Transformation to produce a Uri.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The Uri.</returns>
        [StepArgumentTransformation]
        public static Uri UriTransformation(string value)
        {
            return new Uri(value, UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Transformation to produce a <see cref="HttpMethod"/>.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The HttpMethod.</returns>
        [StepArgumentTransformation]
        public static HttpMethod HttpMethodTransformation(string method)
        {
            return new HttpMethod(method);
        }

        /// <summary>
        /// Transformation to produce a <see cref="HttpMethod"/>.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The HttpMethod.</returns>
        [StepArgumentTransformation]
        public static string spaceTransformation(string value)
        {
            return value.Replace("<space>"," ");
        }
    }

}
