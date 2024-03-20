using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Models;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Services
{

    /// <summary>Represents the model service</summary>
    public class ModelService : IModelService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;
        private readonly IProviderEndpointService _providerEndpointService;

        /// <summary>Initializes a new instance of the <see cref="ModelService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiCommunicationService</exception>
        public ModelService(OpenAIOptions options, IServiceProvider serviceProvider, IProviderEndpointService providerEndpointService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));
            if (providerEndpointService == null) throw new ArgumentNullException(nameof(providerEndpointService));

            _options = options;
            _apiHttpService = serviceProvider.GetRequiredService<IApiHttpService>();
            _providerEndpointService = providerEndpointService;
        }

        /// <summary>Initializes a new instance of the <see cref="ModelService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        public ModelService(IOptions<OpenAIOptions> options, IServiceProvider serviceProvider, IProviderEndpointService providerEndpointService)
            : this(options?.Value, serviceProvider, providerEndpointService)
        {
        }

        /// <summary>Gets the all available models</summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>ModelsResponse</returns>
        public async Task<HttpOperationResult<ModelsResponse>> GetAsync(CancellationToken cancellationToken = default)
        {
            return await _apiHttpService.GetAsync<ModelsResponse>(GetUri(), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Gets the specified model</summary>
        /// <param name="modelId">The model identifier. Check the KnownModelTypes constants.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>
        ///   Model
        /// </returns>
        public async Task<HttpOperationResult<Model>> GetAsync(string modelId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(modelId)) new HttpOperationResult(new ArgumentNullException(nameof(modelId)), System.Net.HttpStatusCode.BadRequest);

            return await _apiHttpService.GetAsync<Model>(GetUri(modelId), cancellationToken).ConfigureAwait(false);
        }

        private string GetUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.ModelsUri);
        }

        private string GetUri(string modelId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), $"{_options.ModelsUri}/{modelId}");
        }

    }

}
