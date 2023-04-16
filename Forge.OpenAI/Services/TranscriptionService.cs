using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Audio.Transcription;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Services
{

    /// <summary>Represents the transcription service</summary>
    public class TranscriptionService : ITranscriptionService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;
        private readonly IProviderEndpointService _providerEndpointService;

        /// <summary>Initializes a new instance of the <see cref="TranscriptionService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API HTTP service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiHttpService</exception>
        public TranscriptionService(OpenAIOptions options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (apiHttpService == null) throw new ArgumentNullException(nameof(apiHttpService));
            if (providerEndpointService == null) throw new ArgumentNullException(nameof(providerEndpointService));

            _options = options;
            _apiHttpService = apiHttpService;
            _providerEndpointService = providerEndpointService;
        }

        /// <summary>Initializes a new instance of the <see cref="TranscriptionService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API HTTP service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        public TranscriptionService(IOptions<OpenAIOptions> options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
            : this(options?.Value, apiHttpService, providerEndpointService)
        {
        }

        /// <summary>Request an audio file transcripted and get back the recognised text</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>TranscriptionResponse</returns>
        public async Task<HttpOperationResult<TranscriptionResponse>> GetAsync(TranscriptionRequest request, CancellationToken cancellationToken = default)
        {
            var validationResult = request.Validate<TranscriptionResponse>();
            if (validationResult != null) return validationResult;
            return await _apiHttpService.PostAsync<TranscriptionRequest, TranscriptionResponse>(GetTranscriptUri(), request, TranscriptHttpContentFactoryAsync, cancellationToken).ConfigureAwait(false);
        }

        private string GetTranscriptUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.AudioTranscriptUri);
        }

        private async Task<HttpContent> TranscriptHttpContentFactoryAsync(TranscriptionRequest request, CancellationToken cancellationToken)
        {
            if (request.AudioFile == null) throw new InvalidOperationException("Missing audio file content data");
            if (request.AudioFile.SourceContent == null && request.AudioFile.SourceStream == null) throw new InvalidOperationException("No audio file content nor file stream defined in file content data.");
            if (string.IsNullOrWhiteSpace(request.AudioFile.ContentName)) throw new InvalidOperationException("Missing audio file name in file content data");

            MultipartFormDataContent content = new MultipartFormDataContent();

            // add file content
            if (request.AudioFile.SourceContent != null)
            {
                content.Add(new ByteArrayContent(request.AudioFile.SourceContent), "file", request.AudioFile.ContentName);
            }
            else
            {
                using (MemoryStream fileData = new MemoryStream())
                {
                    await request.AudioFile.SourceStream.CopyToAsync(fileData).ConfigureAwait(false);
                    content.Add(new ByteArrayContent(fileData.ToArray()), "file", request.AudioFile.ContentName);
                    fileData.SetLength(0);
                }
            }

            content.Add(new StringContent(request.Model), "model");

            if (!string.IsNullOrWhiteSpace(request.Prompt)) content.Add(new StringContent(request.Prompt), "prompt");
            if (!string.IsNullOrWhiteSpace(request.ResponseFormat)) content.Add(new StringContent(request.ResponseFormat), "response_format");
            if (!string.IsNullOrWhiteSpace(request.Language)) content.Add(new StringContent(request.Language), "language");
            if (request.Temperature.HasValue) content.Add(new StringContent(request.Temperature.Value.ToString()), "temperature");

            return content;
        }

    }

}
