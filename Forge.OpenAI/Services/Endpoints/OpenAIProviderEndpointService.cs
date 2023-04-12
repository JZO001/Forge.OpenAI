using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace Forge.OpenAI.Services.Endpoints
{

    /// <summary>Provider endpoint service for OpenAI</summary>
    public class OpenAIProviderEndpointService : IProviderEndpointService
    {

        private readonly OpenAIOptions _options;

        /// <summary>Initializes a new instance of the <see cref="OpenAIProviderEndpointService" /> class.</summary>
        /// <param name="options">The options.</param>
        public OpenAIProviderEndpointService(OpenAIOptions options)
        {
            _options = options;
        }

        /// <summary>Initializes a new instance of the <see cref="OpenAIProviderEndpointService" /> class.</summary>
        /// <param name="options">The options.</param>
        public OpenAIProviderEndpointService(IOptions<OpenAIOptions> options)
            : this(options?.Value)
        {
        }

        /// <summary>Builds the base URI.</summary>
        /// <returns>
        ///   URI as string
        /// </returns>
        public virtual string BuildBaseUri()
        {
            if (string.IsNullOrWhiteSpace(_options.BaseAddress)) throw new ValidationException(nameof(_options.BaseAddress));
            if (string.IsNullOrWhiteSpace(_options.OpenAIApiVersion)) throw new ValidationException(nameof(_options.OpenAIApiVersion));
            
            return $"{_options.BaseAddress}/{_options.OpenAIApiVersion}/" + "{0}";
        }

        /// <summary>Configures the HTTP request headers.</summary>
        /// <param name="requestHeaders">The request headers.</param>
        public virtual void ConfigureHttpRequestHeaders(HttpRequestHeaders requestHeaders)
        {
            requestHeaders.Add("Authorization", $"Bearer {_options.AuthenticationInfo.ApiKey}");

            if (!string.IsNullOrEmpty(_options.AuthenticationInfo.Organization))
            {
                requestHeaders.Add("OpenAI-Organization", $"{_options.AuthenticationInfo.Organization}");
            }

            requestHeaders.Add("User-Agent", "Forge.OpenAI");
        }

    }

}
