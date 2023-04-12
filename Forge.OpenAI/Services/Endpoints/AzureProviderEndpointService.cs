using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Headers;

namespace Forge.OpenAI.Services.Endpoints
{

    /// <summary>Provider endpoint service for Azure</summary>
    public class AzureProviderEndpointService : IProviderEndpointService
    {

        private readonly OpenAIOptions _options;

        /// <summary>Initializes a new instance of the <see cref="AzureProviderEndpointService" /> class.</summary>
        /// <param name="options">The options.</param>
        public AzureProviderEndpointService(OpenAIOptions options)
        {
            _options = options;
        }

        /// <summary>Initializes a new instance of the <see cref="AzureProviderEndpointService" /> class.</summary>
        /// <param name="options">The options.</param>
        public AzureProviderEndpointService(IOptions<OpenAIOptions> options)
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
            if (string.IsNullOrWhiteSpace(_options.AzureApiVersion)) throw new ValidationException(nameof(_options.AzureApiVersion));
            if (string.IsNullOrWhiteSpace(_options.AzureResourceName)) throw new ValidationException(nameof(_options.AzureResourceName));
            if (string.IsNullOrWhiteSpace(_options.AzureDeploymentId)) throw new ValidationException(nameof(_options.AzureDeploymentId));

            return $"https://{_options.AzureResourceName}.openai.azure.com/openai/deployments/{WebUtility.UrlEncode(_options.AzureDeploymentId)}/" + "{0}" + $"?api-version={_options.AzureApiVersion}";
        }

        /// <summary>Configures the HTTP request headers.</summary>
        /// <param name="requestHeaders">The request headers.</param>
        public virtual void ConfigureHttpRequestHeaders(HttpRequestHeaders requestHeaders)
        {
            requestHeaders.Add("api-key", _options.AuthenticationInfo.ApiKey);

            if (!string.IsNullOrEmpty(_options.AuthenticationInfo.Organization))
            {
                requestHeaders.Add("OpenAI-Organization", $"{_options.AuthenticationInfo.Organization}");
            }

            requestHeaders.Add("User-Agent", "Forge.OpenAI");
        }

    }

}
