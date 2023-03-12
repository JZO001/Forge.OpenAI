using Forge.OpenAI.Infrastructure;
using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Audio.Translation;
using Forge.OpenAI.Models.Common;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Services
{

    /// <summary>Represents the transcription service</summary>
    public class TranslationService : ServiceBase, ITranslationService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;

        /// <summary>Initializes a new instance of the <see cref="TranslationService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API HTTP service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiHttpService</exception>
        public TranslationService(OpenAIOptions options, IApiHttpService apiHttpService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (apiHttpService == null) throw new ArgumentNullException(nameof(apiHttpService));
            _options = options;
            _apiHttpService = apiHttpService;
        }

        /// <summary>Initializes a new instance of the <see cref="TranslationService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API HTTP service.</param>
        public TranslationService(IOptions<OpenAIOptions> options, IApiHttpService apiHttpService)
            : this(options?.Value, apiHttpService)
        {
        }

        /// <summary>Request an audio file transcripted and get back the recognised text</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>TranscriptionResponse</returns>
        public async Task<HttpOperationResult<TranslationResponse>> GetAsync(TranslationRequest request, CancellationToken cancellationToken = default)
        {
            var validationResult = request.Validate<TranslationResponse>();
            if (validationResult != null) return validationResult;
            return await _apiHttpService.PostAsync<TranslationRequest, TranslationResponse>(GetTranscriptUri(), request, TranslateHttpContentFactoryAsync, cancellationToken).ConfigureAwait(false);
        }

        private string GetTranscriptUri()
        {
            return $"{GetBaseUri(_options)}{_options.AudioTranslationUri}";
        }

        private async Task<HttpContent> TranslateHttpContentFactoryAsync(TranslationRequest request, CancellationToken cancellationToken)
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
            if (request.Temperature.HasValue) content.Add(new StringContent(request.Temperature.Value.ToString()), "temperature");

            return content;
        }

    }

}
