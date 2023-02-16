using Forge.OpenAI.Infrastructure;
using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Embeddings;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Services
{

    /// <summary>Represents the embeddings service</summary>
    public class EmbeddingsService : ServiceBase, IEmbeddingsService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;

        /// <summary>Initializes a new instance of the <see cref="EmbeddingsService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API communication service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiCommunicationService</exception>
        public EmbeddingsService(OpenAIOptions options, IApiHttpService apiHttpService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (apiHttpService == null) throw new ArgumentNullException(nameof(apiHttpService));
            _options = options;
            _apiHttpService = apiHttpService;
        }

        /// <summary>Initializes a new instance of the <see cref="EmbeddingsService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API communication service.</param>
        public EmbeddingsService(IOptions<OpenAIOptions> options, IApiHttpService apiHttpService)
            : this(options?.Value, apiHttpService)
        {
        }

        /// <summary>Acquire embeddings asynchronously</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   EmbeddingsResponse
        /// </returns>
        public async Task<HttpOperationResult<EmbeddingsResponse>> GetAsync(EmbeddingsRequest request, CancellationToken cancellationToken)
        {
            var validationResult = request.Validate<EmbeddingsResponse>();
            if (validationResult != null) return validationResult;
            return await _apiHttpService.PostAsync<EmbeddingsRequest, EmbeddingsResponse>(GetUri(), request, null, cancellationToken).ConfigureAwait(false);
        }

        private string GetUri()
        {
            return $"{GetBaseUri(_options)}{_options.EmbeddingsUri}";
        }

    }

}
