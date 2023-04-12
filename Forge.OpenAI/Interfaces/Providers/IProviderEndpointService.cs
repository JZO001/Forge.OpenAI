using Forge.OpenAI.Settings;
using System.Net.Http.Headers;

namespace Forge.OpenAI.Interfaces.Providers
{

    /// <summary>Provider endpoint service</summary>
    public interface IProviderEndpointService
    {

        /// <summary>Builds the base URI.</summary>
        /// <returns>
        ///   URI as string
        /// </returns>
        string BuildBaseUri();

        /// <summary>Configures the HTTP request headers.</summary>
        /// <param name="requestHeaders">The request headers.</param>
        void ConfigureHttpRequestHeaders(HttpRequestHeaders requestHeaders);

    }

}
