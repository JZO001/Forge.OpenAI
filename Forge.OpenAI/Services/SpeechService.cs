using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Audio.Speech;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Forge.OpenAI.Services
{

    /// <summary>Represents the speech service</summary>
    public class SpeechService : ISpeechService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;
        private readonly IProviderEndpointService _providerEndpointService;

        /// <summary>Initializes a new instance of the <see cref="SpeechService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API communication service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiCommunicationService</exception>
        public SpeechService(OpenAIOptions options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (apiHttpService == null) throw new ArgumentNullException(nameof(apiHttpService));
            if (providerEndpointService == null) throw new ArgumentNullException(nameof(providerEndpointService));

            _options = options;
            _apiHttpService = apiHttpService;
            _providerEndpointService = providerEndpointService;
        }

        /// <summary>Initializes a new instance of the <see cref="SpeechService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API communication service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        public SpeechService(IOptions<OpenAIOptions> options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
            : this(options?.Value, apiHttpService, providerEndpointService)
        {
        }

        /// <summary>Create a sőeech.</summary>
        /// <param name="request">The request parameters.</param>
        /// <param name="resultStream">The result stream.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   Output Stream, which can receive the data from the underlying network stream.
        /// </returns>
        public async Task<HttpOperationResult<Stream>> CreateSpeechAsync(SpeechRequest request, Stream resultStream, CancellationToken cancellationToken = default)
        {
            if (request == null) return new HttpOperationResult<Stream>(new ArgumentNullException(nameof(request)), System.Net.HttpStatusCode.BadRequest);

            var validationResult = request.Validate<Stream>();
            if (validationResult != null) return validationResult;

            if (resultStream == null) return new HttpOperationResult<Stream>(new ArgumentNullException(nameof(resultStream)), System.Net.HttpStatusCode.BadRequest);

            return await _apiHttpService.GetContentAsStream(GetDownloadFileUri(), resultStream, request, null, HttpMethod.Post, cancellationToken).ConfigureAwait(false);
        }

        private string GetDownloadFileUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.AudioSpeechUri);
        }

    }

}
