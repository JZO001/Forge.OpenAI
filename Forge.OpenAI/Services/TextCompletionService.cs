using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.TextCompletions;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Services
{

    /// <summary>
    /// Given a prompt, the model will return one or more predicted completions, <br />
    /// and can also return the probabilities of alternative tokens at each position.
    /// </summary>
    public class TextCompletionService : ITextCompletionService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;
        private readonly IProviderEndpointService _providerEndpointService;

        /// <summary>Initializes a new instance of the <see cref="TextCompletionService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API communication service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiCommunicationService</exception>
        public TextCompletionService(OpenAIOptions options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (apiHttpService == null) throw new ArgumentNullException(nameof(apiHttpService));
            if (providerEndpointService == null) throw new ArgumentNullException(nameof(providerEndpointService));

            _options = options;
            _apiHttpService = apiHttpService;
            _providerEndpointService = providerEndpointService;
        }

        /// <summary>Initializes a new instance of the <see cref="TextCompletionService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API communication service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        public TextCompletionService(IOptions<OpenAIOptions> options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
            : this(options?.Value, apiHttpService, providerEndpointService)
        {
        }

        /// <summary>Request a text completion asynchronously</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   HttpOperationResult
        /// </returns>
        public async Task<HttpOperationResult<TextCompletionResponse>> GetAsync(TextCompletionRequest request, CancellationToken cancellationToken = default)
        {
            var validationResult = request.Validate<TextCompletionResponse>();
            if (validationResult != null) return validationResult;

            return await _apiHttpService.PostAsync<TextCompletionRequest, TextCompletionResponse>(GetUri(), request, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Request a text completion asynchronously in streamed mode.</summary>
        /// <param name="request">The request.</param>
        /// <param name="resultCallback">The result callback which handles the incoming data fragments.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   HttpOperationResult
        /// </returns>
        public async Task<HttpOperationResult> GetStreamAsync(TextCompletionRequest request, Action<HttpOperationResult<TextCompletionResponse>> resultCallback, CancellationToken cancellationToken = default)
        {
            var validationResult = request.Validate();
            if (validationResult != null) return validationResult;
            request.Stream = true;
            
            return await _apiHttpService.StreamedPostAsync(GetUri(), request, resultCallback, cancellationToken).ConfigureAwait(false);
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <summary>Request a text completion asynchronously in streamed mode. This method is only available in .NET Core applications.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   IAsyncEnumerable
        /// </returns>
        public IAsyncEnumerable<HttpOperationResult<TextCompletionResponse>> GetStreamAsync(TextCompletionRequest request, CancellationToken cancellationToken = default)
        {
            var validationResult = request.Validate<TextCompletionResponse>();
            if (validationResult != null) return RequestBase.GetValidationResultAsAsyncEnumerable<TextCompletionResponse>(validationResult);
            request.Stream = true;

            return _apiHttpService.StreamedPostAsync<TextCompletionRequest, TextCompletionResponse>(GetUri(), request, cancellationToken);
        }
#endif

        private string GetUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.TextCompletionsUri);
        }

    }

}
