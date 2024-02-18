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
    public class AssistantService : IAssistantService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;
        private readonly IProviderEndpointService _providerEndpointService;

        /// <summary>Initializes a new instance of the <see cref="AssistantService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API HTTP service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiHttpService
        /// or
        /// providerEndpointService</exception>
        public AssistantService(OpenAIOptions options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (apiHttpService == null) throw new ArgumentNullException(nameof(apiHttpService));
            if (providerEndpointService == null) throw new ArgumentNullException(nameof(providerEndpointService));

            _options = options;
            _apiHttpService = apiHttpService;
            _providerEndpointService = providerEndpointService;

            _apiHttpService.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v1");    
        }

        /// <summary>Initializes a new instance of the <see cref="AssistantService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API HTTP service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        public AssistantService(IOptions<OpenAIOptions> options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
            : this(options?.Value, apiHttpService, providerEndpointService)
        {
        }

        /// <summary>Creates a assistant asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   AssistantResponse
        /// </returns>
        public async Task<HttpOperationResult<AssistantResponse>> CreateAsync(CreateAssistantRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) return new HttpOperationResult<AssistantResponse>(new ArgumentNullException(nameof(request)), System.Net.HttpStatusCode.BadRequest);
            
            var validationResult = request.Validate<AssistantResponse>();
            if (validationResult != null) return validationResult;
            
            return await _apiHttpService.PostAsync<CreateAssistantRequest, AssistantResponse>(GetCreateUri(), request, null, cancellationToken).ConfigureAwait(false);
        }


        /// <summary>Gets a assistant data asynchronously.</summary>
        /// <param name="assistantId">The assistant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   AssistantResponse
        /// </returns>
        public async Task<HttpOperationResult<AssistantResponse>> GetAsync(string assistantId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(assistantId)) return new HttpOperationResult<AssistantResponse>(new ArgumentNullException(nameof(assistantId)), System.Net.HttpStatusCode.BadRequest);
            
            return await _apiHttpService.GetAsync<AssistantResponse>(GetUri(assistantId), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Gets the list of assistants asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   AssistantListResponse
        /// </returns>
        public async Task<HttpOperationResult<AssistantListResponse>> GetAsync(AssistantListRequest request, CancellationToken cancellationToken = default)
        {
            return await _apiHttpService.GetAsync<AssistantListResponse>(GetListUri(request), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Modifies a assistant asynchronously</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   AssistantResponse
        /// </returns>
        public async Task<HttpOperationResult<AssistantResponse>> ModifyAsync(ModifyAssistantRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) return new HttpOperationResult<AssistantResponse>(new ArgumentNullException(nameof(request)), System.Net.HttpStatusCode.BadRequest);
            
            var validationResult = request.Validate<AssistantResponse>();
            if (validationResult != null) return validationResult;
            
            return await _apiHttpService.PostAsync<ModifyAssistantRequest, AssistantResponse>(GetModifyUri(request), request, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Deletes an assistant asynchronously.</summary>
        /// <param name="assistantId">The assistant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   DeleteStateResponse
        /// </returns>
        public async Task<HttpOperationResult<DeleteStateResponse>> DeleteAsync(string assistantId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(assistantId)) return new HttpOperationResult<DeleteStateResponse>(new ArgumentNullException(nameof(assistantId)), System.Net.HttpStatusCode.BadRequest);
            
            return await _apiHttpService.DeleteAsync<DeleteStateResponse>(GetDeleteUri(assistantId), cancellationToken).ConfigureAwait(false);
        }

        private string GetCreateUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.AssistantCreateUri);
        }

        private string GetUri(string assistantId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.AssistantGetUri, assistantId));
        }

        private string GetListUri(AssistantListRequest request)
        {
            StringBuilder sb = new StringBuilder(string.Format(_providerEndpointService.BuildBaseUri(), _options.AssistantListUri));
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

        private string GetModifyUri(ModifyAssistantRequest request)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.AssistantModifyUri, request.AssistantId));
        }

        private string GetDeleteUri(string assistantId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.AssistantDeleteUri, assistantId));
        }

    }

}
