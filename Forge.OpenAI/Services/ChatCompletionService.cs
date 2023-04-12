using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.ChatCompletions;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Services
{

    /// <summary>Given a chat conversation, the model will return a chat completion response.</summary>
    public class ChatCompletionService : IChatCompletionService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;
        private readonly IProviderEndpointService _providerEndpointService;

        /// <summary>Initializes a new instance of the <see cref="ChatCompletionService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API communication service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiCommunicationService</exception>
        public ChatCompletionService(OpenAIOptions options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (apiHttpService == null) throw new ArgumentNullException(nameof(apiHttpService));
            if (providerEndpointService == null) throw new ArgumentNullException(nameof(providerEndpointService));

            _options = options;
            _apiHttpService = apiHttpService;
            _providerEndpointService = providerEndpointService;
        }

        /// <summary>Initializes a new instance of the <see cref="ChatCompletionService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API communication service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        public ChatCompletionService(IOptions<OpenAIOptions> options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
            : this(options?.Value, apiHttpService, providerEndpointService)
        {
        }

        /// <summary>Request a chat completion asynchronously</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   HttpOperationResult
        /// </returns>
        public async Task<HttpOperationResult<ChatCompletionResponse>> GetAsync(ChatCompletionRequest request, CancellationToken cancellationToken)
        {
            var validationResult = request.Validate<ChatCompletionResponse>();
            if (validationResult != null) return validationResult;

            return await _apiHttpService.PostAsync<ChatCompletionRequest, ChatCompletionResponse>(GetUri(), request, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Request a chat completion asynchronously in streamed mode.</summary>
        /// <param name="request">The request.</param>
        /// <param name="resultCallback">The result callback which handles the incoming data fragments.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   HttpOperationResult
        /// </returns>
        public async Task<HttpOperationResult> GetStreamAsync(ChatCompletionRequest request, Action<HttpOperationResult<ChatCompletionStreamedResponse>> resultCallback, CancellationToken cancellationToken)
        {
            var validationResult = request.Validate();
            if (validationResult != null) return validationResult;
            request.Stream = true;

            return await _apiHttpService.StreamedPostAsync(GetUri(), request, resultCallback, cancellationToken).ConfigureAwait(false);
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <summary>Request a chat completion asynchronously in streamed mode. This method is only available in .NET Core applications.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   IAsyncEnumerable
        /// </returns>
        public IAsyncEnumerable<HttpOperationResult<ChatCompletionStreamedResponse>> GetStreamAsync(ChatCompletionRequest request, CancellationToken cancellationToken)
        {
            var validationResult = request.Validate<ChatCompletionStreamedResponse>();
            if (validationResult != null) return RequestBase.GetValidationResultAsAsyncEnumerable<ChatCompletionStreamedResponse>(validationResult);
            request.Stream = true;

            return _apiHttpService.StreamedPostAsync<ChatCompletionRequest, ChatCompletionStreamedResponse>(GetUri(), request, cancellationToken);
        }
#endif

        private string GetUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.ChatCompletionsUri);
        }

    }

}
