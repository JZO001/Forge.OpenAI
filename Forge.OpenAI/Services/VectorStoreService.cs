using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.VectorStores;
using Forge.OpenAI.Models;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Forge.OpenAI.Services
{

    /// <summary>
    /// Vectore Store Service
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Interfaces.Services.IVectorStoreService" />
    public class VectorStoreService : IVectorStoreService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;
        private readonly IProviderEndpointService _providerEndpointService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorStoreService"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        /// <exception cref="ArgumentNullException">
        /// options
        /// or
        /// serviceProvider
        /// or
        /// providerEndpointService
        /// </exception>
        public VectorStoreService(OpenAIOptions options, IServiceProvider serviceProvider, IProviderEndpointService providerEndpointService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));
            if (providerEndpointService == null) throw new ArgumentNullException(nameof(providerEndpointService));

            _options = options;
            _apiHttpService = serviceProvider.GetRequiredService<IApiHttpService>();
            _providerEndpointService = providerEndpointService;

            _apiHttpService.OnPrepareRequest += OnPrepareRequestHandler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorStoreService"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        public VectorStoreService(IOptions<OpenAIOptions> options, IServiceProvider serviceProvider, IProviderEndpointService providerEndpointService)
            : this(options?.Value, serviceProvider, providerEndpointService)
        {
        }

        /// <summary>Creates a vectore store asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   CreateVectorStoreResponse
        /// </returns>
        public async Task<HttpOperationResult<CreateVectorStoreResponse>> CreateAsync(CreateVectorStoreRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) return new HttpOperationResult<CreateVectorStoreResponse>(new ArgumentNullException(nameof(request)), System.Net.HttpStatusCode.BadRequest);

            var validationResult = request.Validate<CreateVectorStoreResponse>();
            if (validationResult != null) return validationResult;

            return await _apiHttpService.PostAsync<CreateVectorStoreRequest, CreateVectorStoreResponse>(GetCreateUri(), request, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns a list of vector stores.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>VectoreStoreResponse</returns>
        public async Task<HttpOperationResult<VectorStoreListResponse>> GetAsync(VectorStoreListRequest request, CancellationToken cancellationToken = default)
        {
            return await _apiHttpService.GetAsync<VectorStoreListResponse>(GetListUri(request), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves a vector store.
        /// </summary>
        /// <param name="vectorStoreId">The vector store identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>VectoreStoreResponse</returns>
        public async Task<HttpOperationResult<VectoreStoreResponse>> GetAsync(string vectorStoreId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(vectorStoreId)) return new HttpOperationResult<VectoreStoreResponse>(new ArgumentNullException(nameof(vectorStoreId)), System.Net.HttpStatusCode.BadRequest);

            return await _apiHttpService.GetAsync<VectoreStoreResponse>(GetUri(vectorStoreId), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Modifies a vector store.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>VectoreStoreResponse</returns>
        public async Task<HttpOperationResult<VectoreStoreResponse>> ModifyAsync(ModifyVectorStoreRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) return new HttpOperationResult<VectoreStoreResponse>(new ArgumentNullException(nameof(request)), System.Net.HttpStatusCode.BadRequest);

            var validationResult = request.Validate<VectoreStoreResponse>();
            if (validationResult != null) return validationResult;

            return await _apiHttpService.PostAsync<ModifyVectorStoreRequest, VectoreStoreResponse>(GetModifyUri(request), request, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete a vector store.
        /// </summary>
        /// <param name="vectorStoreId">The vector store identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>DeleteVectorStoreResponse</returns>
        public async Task<HttpOperationResult<DeleteVectorStoreResponse>> DeleteAsync(string vectorStoreId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(vectorStoreId)) return new HttpOperationResult<DeleteVectorStoreResponse>(new ArgumentNullException(nameof(vectorStoreId)), System.Net.HttpStatusCode.BadRequest);

            return await _apiHttpService.DeleteAsync<DeleteVectorStoreResponse>(GetDeleteUri(vectorStoreId), cancellationToken).ConfigureAwait(false);
        }

        private string GetCreateUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.VectorStoreCreateUri);
        }

        private string GetUri(string vectorStoreId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.VectorStoreGetUri, vectorStoreId));
        }

        private string GetListUri(VectorStoreListRequest request)
        {
            StringBuilder sb = new StringBuilder(string.Format(_providerEndpointService.BuildBaseUri(), _options.VectorStoreListUri));
            if (request != null)
            {
                List<string> queryParams = new List<string>();

                if (!string.IsNullOrEmpty(request.Order)) queryParams.Add($"order={WebUtility.UrlEncode(request.Order)}");

                if (!string.IsNullOrEmpty(request.After)) queryParams.Add($"after={WebUtility.UrlEncode(request.After)}");

                if (request.Limit.HasValue) queryParams.Add($"limit={request.Limit.Value}");

                if (!string.IsNullOrEmpty(request.Before)) queryParams.Add($"before={WebUtility.UrlEncode(request.Before)}");

                if (queryParams.Count > 0) sb.Append($"?{string.Join("&", queryParams)}");
            }
            return sb.ToString();
        }

        private string GetModifyUri(ModifyVectorStoreRequest request)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.VectorStoreModifyUri, request.VectorStoreId));
        }

        private string GetDeleteUri(string vectorStoreId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.VectorStoreDeleteUri, vectorStoreId));
        }

        /// <summary>Called when api requires request to be prepared before sending</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="HttpRequestMessageEventArgs" /> instance containing the event data.</param>
        protected virtual void OnPrepareRequestHandler(object
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            sender, HttpRequestMessageEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_options.AssistantHeaderName))
            {
                e.RequestMessage.Headers.Add(_options.AssistantHeaderName, _options.AssistantHeaderValue);
            }
        }

    }

}
