using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IServiceProvider _serviceProvider;

        /// <summary>Initializes a new instance of the <see cref="ApiHttpClientFactory" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The serviceProvider.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="System.ArgumentNullException">logger
        /// or
        /// options</exception>
        public ApiHttpClientFactory(OpenAIOptions options, IServiceProvider serviceProvider = null, ILogger<ApiHttpClientFactory> logger = null)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            _logger = logger;
            _options = options;
            _serviceProvider = serviceProvider;
        }

        /// <summary>Initializes a new instance of the <see cref="ApiHttpClientFactory" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The serviceProvider.</param>
        /// <param name="logger">The logger.</param>
        public ApiHttpClientFactory(IOptions<OpenAIOptions> options, IServiceProvider serviceProvider = null, ILogger<ApiHttpClientFactory> logger = null)
            : this(options?.Value, serviceProvider, logger)
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
                if (_serviceProvider == null)
                {
                    _logger?.LogDebug($"IServiceProvider not set, BaseAddress: {_options.BaseAddress}");
                    httpClient = new HttpClient { BaseAddress = new Uri(_options.BaseAddress) };
                }
                else
                {
                    _logger?.LogDebug($"IServiceProvider presents, BaseAddress: {_options.BaseAddress}");
                    IHttpClientFactory httpClientFactory = _serviceProvider.GetService<IHttpClientFactory>();
                    httpClient = httpClientFactory.CreateClient(Consts.HTTP_CLIENT_FACTORY_NAME);
                    httpClient.BaseAddress = new Uri(_options.BaseAddress);
                }
            }
            else
            {
                _logger?.LogDebug($"HttpMessageHandler presents, BaseAddress: {_options.BaseAddress}");
                httpClient = new HttpClient(_options.HttpMessageHandlerFactory()) { BaseAddress = new Uri(_options.BaseAddress) };
            }

            if (_options.HttpClientTimeoutInMilliseconds.HasValue)
            {
                httpClient.Timeout = TimeSpan.FromMilliseconds(_options.HttpClientTimeoutInMilliseconds.Value);
            }

            return httpClient
#if NETCOREAPP3_1_OR_GREATER
                !
#endif
                ;
        }

    }

}
