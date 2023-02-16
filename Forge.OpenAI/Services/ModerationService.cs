using Forge.OpenAI.Infrastructure;
using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Moderations;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Services
{

    /// <summary>Moderation service</summary>
    public class ModerationService : ServiceBase, IModerationService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;

        /// <summary>Initializes a new instance of the <see cref="ModerationService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API communication service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiCommunicationService</exception>
        public ModerationService(OpenAIOptions options, IApiHttpService apiHttpService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (apiHttpService == null) throw new ArgumentNullException(nameof(apiHttpService));
            _options = options;
            _apiHttpService = apiHttpService;
        }

        /// <summary>Initializes a new instance of the <see cref="ModerationService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API communication service.</param>
        public ModerationService(IOptions<OpenAIOptions> options, IApiHttpService apiHttpService)
            : this(options?.Value, apiHttpService)
        {
        }

        /// <summary>Acquire moderation</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ModerationResponse
        /// </returns>
        public async Task<HttpOperationResult<ModerationResponse>> GetAsync(ModerationRequest request, CancellationToken cancellationToken = default)
        {
            var validationResult = request.Validate<ModerationResponse>();
            if (validationResult != null) return validationResult;
            return await _apiHttpService.PostAsync<ModerationRequest, ModerationResponse>(GetUri(), request, null, cancellationToken).ConfigureAwait(false);
        }

        private string GetUri()
        {
            return $"{GetBaseUri(_options)}{_options.ModerationUri}";
        }

    }

}
