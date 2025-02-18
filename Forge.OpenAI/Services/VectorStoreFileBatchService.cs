using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.VectorStoreFileBatches;
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
    /// Vector Store File Batches service
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Interfaces.Services.IVectorStoreFileBatchService" />
    public class VectorStoreFileBatchService : IVectorStoreFileBatchService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;
        private readonly IProviderEndpointService _providerEndpointService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorStoreFileBatchService"/> class.
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
        public VectorStoreFileBatchService(OpenAIOptions options, IServiceProvider serviceProvider, IProviderEndpointService providerEndpointService)
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
        /// Initializes a new instance of the <see cref="VectorStoreFileBatchService"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        public VectorStoreFileBatchService(IOptions<OpenAIOptions> options, IServiceProvider serviceProvider, IProviderEndpointService providerEndpointService)
            : this(options?.Value, serviceProvider, providerEndpointService)
        {
        }

        /// <summary>
        /// Create vector store batch file
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>CreateVectorStoreFileBatchResponse</returns>
        public async Task<HttpOperationResult<CreateVectorStoreFileBatchResponse>> CreateAsync(CreateVectorStoreFileBatchRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) return new HttpOperationResult<CreateVectorStoreFileBatchResponse>(new ArgumentNullException(nameof(request)), System.Net.HttpStatusCode.BadRequest);

            var validationResult = request.Validate<CreateVectorStoreFileBatchResponse>();
            if (validationResult != null) return validationResult;

            return await _apiHttpService.PostAsync<CreateVectorStoreFileBatchRequest, CreateVectorStoreFileBatchResponse>(GetCreateUri(request.VectorStoreId), request, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// List vector store batch files
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>VectorStoreFileBatchFileListResponse</returns>
        public async Task<HttpOperationResult<VectorStoreFileBatchFileListResponse>> GetAsync(VectorStoreFileBatchFileListRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.VectorStoreId)) return new HttpOperationResult<VectorStoreFileBatchFileListResponse>(new ArgumentException(nameof(request.VectorStoreId)), System.Net.HttpStatusCode.BadRequest);
            if (string.IsNullOrWhiteSpace(request.BatchId)) return new HttpOperationResult<VectorStoreFileBatchFileListResponse>(new ArgumentException(nameof(request.BatchId)), System.Net.HttpStatusCode.BadRequest);

            return await _apiHttpService.GetAsync<VectorStoreFileBatchFileListResponse>(GetListUri(request), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve vector store batch file
        /// </summary>
        /// <param name="vectorStoreId">The vector store identifier.</param>
        /// <param name="batchId">The batch identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>VectorStoreFileBatchResponse</returns>
        public async Task<HttpOperationResult<VectorStoreFileBatchResponse>> GetAsync(string vectorStoreId, string batchId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(vectorStoreId)) return new HttpOperationResult<VectorStoreFileBatchResponse>(new ArgumentNullException(nameof(vectorStoreId)), System.Net.HttpStatusCode.BadRequest);
            if (string.IsNullOrWhiteSpace(batchId)) return new HttpOperationResult<VectorStoreFileBatchResponse>(new ArgumentNullException(nameof(batchId)), System.Net.HttpStatusCode.BadRequest);

            return await _apiHttpService.GetAsync<VectorStoreFileBatchResponse>(GetUri(vectorStoreId, batchId), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel vector store batch file
        /// </summary>
        /// <param name="vectorStoreId">The vector store identifier.</param>
        /// <param name="batchId">The batch identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>DeleteVectorStoreFileBatchResponse</returns>
        public async Task<HttpOperationResult<DeleteVectorStoreFileBatchResponse>> CancelAsync(string vectorStoreId, string batchId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(vectorStoreId)) return new HttpOperationResult<DeleteVectorStoreFileBatchResponse>(new ArgumentNullException(nameof(vectorStoreId)), System.Net.HttpStatusCode.BadRequest);
            if (string.IsNullOrWhiteSpace(batchId)) return new HttpOperationResult<DeleteVectorStoreFileBatchResponse>(new ArgumentNullException(nameof(batchId)), System.Net.HttpStatusCode.BadRequest);

            return await _apiHttpService.PostAsync<object, DeleteVectorStoreFileBatchResponse>(GetCancelUri(vectorStoreId, batchId), null, null, cancellationToken).ConfigureAwait(false);
        }

        private string GetCreateUri(string vectorStoreId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.VectorStoreFileBatchesCreateUri, vectorStoreId));
        }

        private string GetUri(string vectorStoreId, string batchId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.VectorStoreFileBatchesGetUri, vectorStoreId, batchId));
        }

        private string GetListUri(VectorStoreFileBatchFileListRequest request)
        {
            StringBuilder sb = new StringBuilder(
                string.Format(
                    _providerEndpointService.BuildBaseUri(), 
                    string.Format(_options.VectorStoreFileBatchesFileListUri, request.VectorStoreId, request.BatchId)
                )
            );

            if (request != null)
            {
                List<string> queryParams = new List<string>();

                if (!string.IsNullOrEmpty(request.Order)) queryParams.Add($"order={WebUtility.UrlEncode(request.Order)}");

                if (!string.IsNullOrEmpty(request.After)) queryParams.Add($"after={WebUtility.UrlEncode(request.After)}");

                if (request.Limit.HasValue) queryParams.Add($"limit={request.Limit.Value}");

                if (!string.IsNullOrEmpty(request.Before)) queryParams.Add($"before={WebUtility.UrlEncode(request.Before)}");

                if (!string.IsNullOrEmpty(request.Filter)) queryParams.Add($"filter={WebUtility.UrlEncode(request.Filter)}");

                if (queryParams.Count > 0) sb.Append($"?{string.Join("&", queryParams)}");
            }

            return sb.ToString();
        }

        private string GetCancelUri(string vectorStoreId, string batchId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.VectorStoreFileCancelUri, vectorStoreId, batchId));
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
