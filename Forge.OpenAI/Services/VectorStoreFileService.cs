using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.VectorStoreFiles;
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
    /// Vector store file service
    /// </summary>
    public class VectorStoreFileService : IVectorStoreFileService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;
        private readonly IProviderEndpointService _providerEndpointService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorStoreFileService"/> class.
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
        public VectorStoreFileService(OpenAIOptions options, IServiceProvider serviceProvider, IProviderEndpointService providerEndpointService)
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
        /// Initializes a new instance of the <see cref="VectorStoreFileService"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        public VectorStoreFileService(IOptions<OpenAIOptions> options, IServiceProvider serviceProvider, IProviderEndpointService providerEndpointService)
            : this(options?.Value, serviceProvider, providerEndpointService)
        {
        }

        /// <summary>
        /// Create vector store file
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>CreateVectorStoreFilesResponse</returns>
        public async Task<HttpOperationResult<CreateVectorStoreFilesResponse>> CreateAsync(CreateVectorStoreFilesRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) return new HttpOperationResult<CreateVectorStoreFilesResponse>(new ArgumentNullException(nameof(request)), System.Net.HttpStatusCode.BadRequest);

            var validationResult = request.Validate<CreateVectorStoreFilesResponse>();
            if (validationResult != null) return validationResult;

            return await _apiHttpService.PostAsync<CreateVectorStoreFilesRequest, CreateVectorStoreFilesResponse>(GetCreateUri(), request, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// List vector store files
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>VectorStoreFilesListResponse</returns>
        public async Task<HttpOperationResult<VectorStoreFilesListResponse>> GetAsync(VectorStoreFilesListRequest request, CancellationToken cancellationToken = default)
        {
            return await _apiHttpService.GetAsync<VectorStoreFilesListResponse>(GetListUri(request), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve vector store file
        /// </summary>
        /// <param name="vectorStoreFileId">The vector store file identifier.</param>
        /// <param name="fileId">The ID of the file being retrieved.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>VectorStoreFileResponse</returns>
        public async Task<HttpOperationResult<VectorStoreFileResponse>> GetAsync(string vectorStoreFileId, string fileId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(vectorStoreFileId)) return new HttpOperationResult<VectorStoreFileResponse>(new ArgumentNullException(nameof(vectorStoreFileId)), System.Net.HttpStatusCode.BadRequest);
            if (string.IsNullOrWhiteSpace(fileId)) return new HttpOperationResult<VectorStoreFileResponse>(new ArgumentNullException(nameof(fileId)), System.Net.HttpStatusCode.BadRequest);

            return await _apiHttpService.GetAsync<VectorStoreFileResponse>(GetUri(vectorStoreFileId, fileId), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete vector store file
        /// </summary>
        /// <param name="vectorStoreFileId">The vector store file identifier.</param>
        /// <param name="fileId">The ID of the file being retrieved.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>DeleteVectorStoreFileResponse</returns>
        public async Task<HttpOperationResult<DeleteVectorStoreFileResponse>> DeleteAsync(string vectorStoreFileId, string fileId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(vectorStoreFileId)) return new HttpOperationResult<DeleteVectorStoreFileResponse>(new ArgumentNullException(nameof(vectorStoreFileId)), System.Net.HttpStatusCode.BadRequest);
            if (string.IsNullOrWhiteSpace(fileId)) return new HttpOperationResult<DeleteVectorStoreFileResponse>(new ArgumentNullException(nameof(fileId)), System.Net.HttpStatusCode.BadRequest);

            return await _apiHttpService.DeleteAsync<DeleteVectorStoreFileResponse>(GetDeleteUri(vectorStoreFileId, fileId), cancellationToken).ConfigureAwait(false);
        }

        private string GetCreateUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.VectorStoreFileCreateUri);
        }

        private string GetUri(string vectorStoreFileId, string fileId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.VectorStoreFileGetUri, vectorStoreFileId, fileId));
        }

        private string GetListUri(VectorStoreFilesListRequest request)
        {
            StringBuilder sb = new StringBuilder(string.Format(_providerEndpointService.BuildBaseUri(), _options.VectorStoreListUri));
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

        private string GetDeleteUri(string vectorStoreFileId, string fileId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.VectorStoreFileDeleteUri, vectorStoreFileId, fileId));
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
