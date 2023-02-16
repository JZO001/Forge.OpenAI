using Forge.OpenAI.Infrastructure;
using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.TextEdits;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Services
{

    /// <summary>Represents a text edit service</summary>
    public class TextEditService : ServiceBase, ITextEditService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;

        /// <summary>Initializes a new instance of the <see cref="TextEditService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API communication service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiCommunicationService</exception>
        public TextEditService(OpenAIOptions options, IApiHttpService apiHttpService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (apiHttpService == null) throw new ArgumentNullException(nameof(apiHttpService));
            _options = options;
            _apiHttpService = apiHttpService;
        }

        /// <summary>Initializes a new instance of the <see cref="TextEditService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API communication service.</param>
        public TextEditService(IOptions<OpenAIOptions> options, IApiHttpService apiHttpService)
            : this(options?.Value, apiHttpService)
        {
        }

        /// <summary>Request a text edit operation</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   TextEditResponse
        /// </returns>
        public async Task<HttpOperationResult<TextEditResponse>> GetAsync(TextEditRequest request, CancellationToken cancellationToken)
        {
            var validationResult = request.Validate<TextEditResponse>();
            if (validationResult != null) return validationResult;
            return await _apiHttpService.PostAsync<TextEditRequest, TextEditResponse>(GetUri(), request, null, cancellationToken).ConfigureAwait(false);
        }

        private string GetUri()
        {
            return $"{GetBaseUri(_options)}{_options.TextEditUri}";
        }

    }

}
