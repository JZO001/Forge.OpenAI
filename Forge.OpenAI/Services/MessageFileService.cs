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
using Microsoft.Extensions.DependencyInjection;

namespace Forge.OpenAI.Services
{

    /// <summary>
    /// Create messages within threads
    /// https://platform.openai.com/docs/api-reference/messages
    /// </summary>
    public class MessageFileService : IMessageFileService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;
        private readonly IProviderEndpointService _providerEndpointService;

        /// <summary>Initializes a new instance of the <see cref="MessageFileService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiHttpService
        /// or
        /// providerEndpointService</exception>
        public MessageFileService(OpenAIOptions options, IServiceProvider serviceProvider, IProviderEndpointService providerEndpointService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));
            if (providerEndpointService == null) throw new ArgumentNullException(nameof(providerEndpointService));

            _options = options;
            _apiHttpService = serviceProvider.GetRequiredService<IApiHttpService>();
            _providerEndpointService = providerEndpointService;
        }

        /// <summary>Initializes a new instance of the <see cref="MessageFileService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        public MessageFileService(IOptions<OpenAIOptions> options, IServiceProvider serviceProvider, IProviderEndpointService providerEndpointService)
            : this(options?.Value, serviceProvider, providerEndpointService)
        {
        }

        /// <summary>Gets a message files asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   AssistantFileResponse
        /// </returns>
        public async Task<HttpOperationResult<MessageFileListResponse>> GetAsync(MessageFileListRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) return new HttpOperationResult<MessageFileListResponse>(new ArgumentNullException(nameof(request)), System.Net.HttpStatusCode.BadRequest);

            var validationResult = request.Validate<MessageFileListResponse>();
            if (validationResult != null) return validationResult;

            return await _apiHttpService.GetAsync<MessageFileListResponse>(GetListUri(request), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Gets the list of assistant files asynchronously.</summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   AssistantFileListResponse
        /// </returns>
        public async Task<HttpOperationResult<MessageFileResponse>> GetAsync(string threadId, string messageId, string fileId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(threadId)) return new HttpOperationResult<MessageFileResponse>(new ArgumentNullException(nameof(threadId)), System.Net.HttpStatusCode.BadRequest);
            if (string.IsNullOrWhiteSpace(messageId)) return new HttpOperationResult<MessageFileResponse>(new ArgumentNullException(nameof(messageId)), System.Net.HttpStatusCode.BadRequest);
            if (string.IsNullOrWhiteSpace(fileId)) return new HttpOperationResult<MessageFileResponse>(new ArgumentNullException(nameof(fileId)), System.Net.HttpStatusCode.BadRequest);

            return await _apiHttpService.GetAsync<MessageFileResponse>(GetUri(threadId, messageId, fileId), cancellationToken).ConfigureAwait(false);
        }

        private string GetUri(string threadId, string messageId, string fileId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.MessageFileGetUri, threadId, messageId, fileId));
        }

        private string GetListUri(MessageFileListRequest request)
        {
            StringBuilder sb = new StringBuilder(string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.MessageFileListUri, request.ThreadId, request.MessageId)));

            List<string> queryParams = new List<string>();

            if (!string.IsNullOrEmpty(request.Order)) queryParams.Add($"order={WebUtility.UrlEncode(request.Order)}");

            if (!string.IsNullOrEmpty(request.After)) queryParams.Add($"after={WebUtility.UrlEncode(request.After)}");

            if (request.Limit.HasValue) queryParams.Add($"limit={request.Limit.Value}");

            if (!string.IsNullOrEmpty(request.Before)) queryParams.Add($"before={WebUtility.UrlEncode(request.Before)}");

            if (queryParams.Count > 0) sb.Append($"?{string.Join("&", queryParams)}");

            return sb.ToString();
        }

    }

}
