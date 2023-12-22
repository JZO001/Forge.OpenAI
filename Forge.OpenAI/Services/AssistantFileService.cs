using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Assistants;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Services
{

    /// <summary>
    /// Build assistants that can call models and use tools to perform tasks.
    /// https://platform.openai.com/docs/api-reference/assistants
    /// https://platform.openai.com/docs/assistants/overview
    /// </summary>
    public class AssistantFileService : IAssistantFileService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;
        private readonly IProviderEndpointService _providerEndpointService;

        /// <summary>Initializes a new instance of the <see cref="AssistantFileService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API HTTP service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiHttpService
        /// or
        /// providerEndpointService</exception>
        public AssistantFileService(OpenAIOptions options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (apiHttpService == null) throw new ArgumentNullException(nameof(apiHttpService));
            if (providerEndpointService == null) throw new ArgumentNullException(nameof(providerEndpointService));

            _options = options;
            _apiHttpService = apiHttpService;
            _providerEndpointService = providerEndpointService;
        }

        /// <summary>Initializes a new instance of the <see cref="AssistantFileService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API HTTP service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        public AssistantFileService(IOptions<OpenAIOptions> options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
            : this(options?.Value, apiHttpService, providerEndpointService)
        {
        }

        /// <summary>Creates a assistant file asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   AssistantFileResponse
        /// </returns>
        public async Task<HttpOperationResult<AssistantFileResponse>> CreateAsync(CreateAssistantFileRequest request, CancellationToken cancellationToken = default)
        {
            var validationResult = request.Validate<AssistantFileResponse>();
            if (validationResult != null) return validationResult;
            return await _apiHttpService.PostAsync<CreateAssistantFileRequest, AssistantFileResponse>(GetCreateUri(request.AssistantId), request, null, cancellationToken).ConfigureAwait(false);
        }


        /// <summary>Gets a assistant file asynchronously.</summary>
        /// <param name="assistantId">The assistant identifier.</param>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   AssistantFileResponse
        /// </returns>
        public async Task<HttpOperationResult<AssistantFileResponse>> GetAsync(string assistantId, string fileId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(assistantId)) return new HttpOperationResult<AssistantFileResponse>(new ArgumentNullException(nameof(assistantId)), System.Net.HttpStatusCode.BadRequest);
            if (string.IsNullOrWhiteSpace(fileId)) return new HttpOperationResult<AssistantFileResponse>(new ArgumentNullException(nameof(assistantId)), System.Net.HttpStatusCode.BadRequest);
            return await _apiHttpService.GetAsync<AssistantFileResponse>(GetUri(assistantId, fileId), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Gets the list of assistant files asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   AssistantFileListResponse
        /// </returns>
        public async Task<HttpOperationResult<AssistantFileListResponse>> GetAsync(AssistantFileListRequest request, CancellationToken cancellationToken = default)
        {
            return await _apiHttpService.GetAsync<AssistantFileListResponse>(GetListUri(request), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Deletes an assistant file asynchronously.</summary>
        /// <param name="assistantId">The assistant identifier.</param>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   DeleteStateResponse
        /// </returns>
        public async Task<HttpOperationResult<DeleteStateResponse>> DeleteAsync(string assistantId, string fileId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(assistantId)) return new HttpOperationResult<DeleteStateResponse>(new ArgumentNullException(nameof(assistantId)), System.Net.HttpStatusCode.BadRequest);
            if (string.IsNullOrWhiteSpace(fileId)) return new HttpOperationResult<DeleteStateResponse>(new ArgumentNullException(nameof(assistantId)), System.Net.HttpStatusCode.BadRequest);
            return await _apiHttpService.DeleteAsync<DeleteStateResponse>(GetDeleteUri(assistantId, fileId), cancellationToken).ConfigureAwait(false);
        }

        private string GetCreateUri(string assistantId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.AssistantFileCreateUri, assistantId));
        }

        private string GetUri(string assistantId, string fileId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.AssistantFileGetUri, assistantId, fileId));
        }

        private string GetListUri(AssistantFileListRequest request)
        {
            StringBuilder sb = new StringBuilder(string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.AssistantFileListUri, request.AssistantId)));
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

        private string GetDeleteUri(string assistantId, string fileId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.AssistantFileDeleteUri, assistantId, fileId));
        }

    }

}
