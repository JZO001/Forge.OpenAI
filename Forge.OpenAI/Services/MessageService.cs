using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Settings;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Messages;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace Forge.OpenAI.Services
{

    /// <summary>
    /// Create messages within threads
    /// https://platform.openai.com/docs/api-reference/messages
    /// </summary>
    public class MessageService : IMessageService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;
        private readonly IProviderEndpointService _providerEndpointService;

        /// <summary>Initializes a new instance of the <see cref="MessageService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API HTTP service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiHttpService
        /// or
        /// providerEndpointService</exception>
        public MessageService(OpenAIOptions options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (apiHttpService == null) throw new ArgumentNullException(nameof(apiHttpService));
            if (providerEndpointService == null) throw new ArgumentNullException(nameof(providerEndpointService));

            _options = options;
            _apiHttpService = apiHttpService;
            _providerEndpointService = providerEndpointService;
        }

        /// <summary>Initializes a new instance of the <see cref="MessageService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API HTTP service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        public MessageService(IOptions<OpenAIOptions> options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
            : this(options?.Value, apiHttpService, providerEndpointService)
        {
        }

        /// <summary>Creates a message asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   CreateMessageResponse
        /// </returns>
        public async Task<HttpOperationResult<CreateMessageResponse>> CreateAsync(CreateMessageRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) return new HttpOperationResult<CreateMessageResponse>(new ArgumentNullException(nameof(request)), System.Net.HttpStatusCode.BadRequest);
            
            var validationResult = request.Validate<CreateMessageResponse>();
            if (validationResult != null) return validationResult;
            
            return await _apiHttpService.PostAsync<CreateMessageRequest, CreateMessageResponse>(GetCreateUri(request.ThreadId), request, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Gets a message data asynchronously.</summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   MessageResponse
        /// </returns>
        public async Task<HttpOperationResult<MessageResponse>> GetAsync(string threadId, string messageId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(threadId)) return new HttpOperationResult<MessageResponse>(new ArgumentNullException(nameof(threadId)), System.Net.HttpStatusCode.BadRequest);
            if (string.IsNullOrWhiteSpace(messageId)) return new HttpOperationResult<MessageResponse>(new ArgumentNullException(nameof(messageId)), System.Net.HttpStatusCode.BadRequest);
            
            return await _apiHttpService.GetAsync<MessageResponse>(GetUri(threadId, messageId), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Gets the list of messages asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   MessageListResponse
        /// </returns>
        public async Task<HttpOperationResult<MessageListResponse>> GetAsync(MessageListRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) return new HttpOperationResult<MessageListResponse>(new ArgumentNullException(nameof(request)), System.Net.HttpStatusCode.BadRequest);
            
            var validationResult = request.Validate<MessageListResponse>();
            if (validationResult != null) return validationResult;
            
            return await _apiHttpService.GetAsync<MessageListResponse>(GetListUri(request), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Modifies a message asynchronously</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ModifyMessageResponse
        /// </returns>
        public async Task<HttpOperationResult<ModifyMessageResponse>> ModifyAsync(ModifyMessageRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) return new HttpOperationResult<ModifyMessageResponse>(new ArgumentNullException(nameof(request)), System.Net.HttpStatusCode.BadRequest);
            
            var validationResult = request.Validate<ModifyMessageResponse>();
            if (validationResult != null) return validationResult;
            
            return await _apiHttpService.PostAsync<ModifyMessageRequest, ModifyMessageResponse>(GetModifyUri(request), request, null, cancellationToken).ConfigureAwait(false);
        }

        private string GetCreateUri(string threadId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.MessageCreateUri, threadId));
        }

        private string GetUri(string threadId, string messageId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.MessageGetUri, threadId, messageId));
        }

        private string GetListUri(MessageListRequest request)
        {
            StringBuilder sb = new StringBuilder(string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.MessageListUri, request.ThreadId)));

            List<string> queryParams = new List<string>();

            if (!string.IsNullOrEmpty(request.Order)) queryParams.Add($"order={WebUtility.UrlEncode(request.Order)}");

            if (!string.IsNullOrEmpty(request.After)) queryParams.Add($"after={WebUtility.UrlEncode(request.After)}");

            if (request.Limit.HasValue) queryParams.Add($"limit={request.Limit.Value}");

            if (!string.IsNullOrEmpty(request.Before)) queryParams.Add($"before={WebUtility.UrlEncode(request.Before)}");

            if (queryParams.Count > 0) sb.Append($"?{string.Join("&", queryParams)}");

            return sb.ToString();
        }

        private string GetModifyUri(ModifyMessageRequest request)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.MessageModifyUri, request.ThreadId, request.MessageId));
        }

    }

}
