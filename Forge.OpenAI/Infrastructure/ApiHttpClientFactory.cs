using Forge.OpenAI.Interfaces.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace Forge.OpenAI.Infrastructure
{

    /// <summary>Represents a HttpClient factory with a unique configuration</summary>
    public class ApiHttpClientFactory : IApiHttpClientFactory
    {

        private readonly ILogger<ApiHttpClientFactory> _logger;
        private readonly OpenAIOptions _options;

        /// <summary>Initializes a new instance of the <see cref="ApiHttpClientFactory" /> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="System.ArgumentNullException">logger
        /// or
        /// options</exception>
        public ApiHttpClientFactory(ILogger<ApiHttpClientFactory> logger, OpenAIOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            _logger = logger;
            _options = options;
        }

        /// <summary>Initializes a new instance of the <see cref="ApiHttpClientFactory" /> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="options">The options.</param>
        public ApiHttpClientFactory(ILogger<ApiHttpClientFactory> logger, IOptions<OpenAIOptions> options)
            : this(logger, options?.Value)
        {
        }

        /// <summary>Gets the HTTP client.</summary>
        /// <value>The HTTP client.</value>
        public HttpClient GetHttpClient()
        {
            HttpClient
#if NETCOREAPP3_1_OR_GREATER
                ?
#endif
                httpClient = null;
            if (_options.HttpMessageHandlerFactory == null)
            {
                _logger?.LogDebug($"HttpMessageHandler not set, BaseAddress: {_options.BaseAddress}");
                httpClient = new HttpClient { BaseAddress = new Uri(_options.BaseAddress) };
            }
            else
            {
                _logger?.LogDebug($"HttpMessageHandler presents, BaseAddress: {_options.BaseAddress}");
                httpClient = new HttpClient(_options.HttpMessageHandlerFactory()) { BaseAddress = new Uri(_options.BaseAddress) };
            }
            return httpClient
#if NETCOREAPP3_1_OR_GREATER
                !
#endif
                ;
        }

    }

}
