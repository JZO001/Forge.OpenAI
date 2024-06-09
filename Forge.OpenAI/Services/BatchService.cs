using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Batch;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Forge.OpenAI.Services
{

    /// <summary>
    /// Batch service
    /// </summary>
    public class BatchService : IBatchService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;
        private readonly IProviderEndpointService _providerEndpointService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchService"/> class.
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
        public BatchService(OpenAIOptions options, IServiceProvider serviceProvider, IProviderEndpointService providerEndpointService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));
            if (providerEndpointService == null) throw new ArgumentNullException(nameof(providerEndpointService));

            _options = options;
            _apiHttpService = serviceProvider.GetRequiredService<IApiHttpService>();
            _providerEndpointService = providerEndpointService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchService"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        public BatchService(IOptions<OpenAIOptions> options, IServiceProvider serviceProvider, IProviderEndpointService providerEndpointService)
            : this(options?.Value, serviceProvider, providerEndpointService)
        {
        }

        /// <summary>Creates and executes a batch from an uploaded file of requests</summary>
        /// <param name="request">The fine tune job checkpoint request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   CreateBatchResponse
        /// </returns>
        public async Task<HttpOperationResult<CreateBatchResponse>> CreateAsync(CreateBatchRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) return new HttpOperationResult<CreateBatchResponse>(new ArgumentNullException(nameof(request)), System.Net.HttpStatusCode.BadRequest);

            var validationResult = request.Validate<CreateBatchResponse>();
            if (validationResult != null) return validationResult;

            return await _apiHttpService.PostAsync<CreateBatchRequest, CreateBatchResponse>(GetBatchesUri(), request, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the batches asynchronously.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>ListBatchesResponse</returns>
        public async Task<HttpOperationResult<ListBatchesResponse>> GetAsync(ListBatchesRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) return new HttpOperationResult<ListBatchesResponse>(new ArgumentNullException(nameof(request)), System.Net.HttpStatusCode.BadRequest);

            var validationResult = request.Validate<ListBatchesResponse>();
            if (validationResult != null) return validationResult;

            return await _apiHttpService.GetAsync<ListBatchesResponse>(GetBatchesUri(), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the batch by id asynchronously.
        /// </summary>
        /// <param name="batchId">The batch identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>BatchResponse</returns>
        public async Task<HttpOperationResult<BatchResponse>> GetAsync(string batchId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(batchId)) return new HttpOperationResult<BatchResponse>(new ArgumentNullException(nameof(batchId)), System.Net.HttpStatusCode.BadRequest);

            return await _apiHttpService.GetAsync<BatchResponse>(GetBatchesUri(batchId), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancels a job the asynchronously.
        /// </summary>
        /// <param name="batchId">The batch identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>CancelBatchResponse</returns>
        public async Task<HttpOperationResult<CancelBatchResponse>> CancelAsync(string batchId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(batchId)) return new HttpOperationResult<CancelBatchResponse>(new ArgumentNullException(nameof(batchId)), System.Net.HttpStatusCode.BadRequest);

            return await _apiHttpService.PostAsync<object, CancelBatchResponse>(CancelBatchesUri(batchId), null, null, cancellationToken).ConfigureAwait(false);
        }

        private string GetBatchesUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.BatchUri);
        }

        private string GetBatchesUri(string batchId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.BatchGetUri, batchId));
        }

        private string CancelBatchesUri(string batchId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.BatchCancelUri, batchId));
        }

    }

}
