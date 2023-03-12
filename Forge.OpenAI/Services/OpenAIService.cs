using Forge.OpenAI.Infrastructure;
using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Services;
using System;

namespace Forge.OpenAI.Services
{

    /// <summary>Represents the OpenAI service</summary>
    public class OpenAIService : IOpenAIService
    {

        public OpenAIService(IModelService modelService,
            ITextCompletionService textCompletionService,
            ITextEditService textEditService,
            IModerationService moderationService,
            IEmbeddingsService embeddingsService,
            IImageService imageService,
            IFileService fileService,
            IFineTuneService fineTuneService,
            ITranscriptionService transcriptionService,
            ITranslationService translationService)
        {
            if (modelService == null) throw new ArgumentNullException(nameof(modelService));
            if (textCompletionService == null) throw new ArgumentNullException(nameof(textCompletionService));
            if (textEditService == null) throw new ArgumentNullException(nameof(textEditService));
            if (moderationService == null) throw new ArgumentNullException(nameof(moderationService));
            if (embeddingsService == null) throw new ArgumentNullException(nameof(embeddingsService));
            if (imageService == null) throw new ArgumentNullException(nameof(imageService));
            if (fileService == null) throw new ArgumentNullException(nameof(fileService));
            if (fineTuneService == null) throw new ArgumentNullException(nameof(fineTuneService));
            if (transcriptionService == null) throw new ArgumentNullException(nameof(transcriptionService));
            if (translationService == null) throw new ArgumentNullException(nameof(translationService));

            ModelService = modelService;
            TextCompletionService = textCompletionService;
            TextEditService = textEditService;
            ModerationService = moderationService;
            EmbeddingsService = embeddingsService;
            ImageService = imageService;
            FileService = fileService;
            FineTuneService = fineTuneService;
            TranscriptionService = transcriptionService;
            TranslationService = translationService;
        }

        /// <summary>Initializes a new instance of the <see cref="OpenAIService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API HTTP service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiHttpService</exception>
        public OpenAIService(OpenAIOptions options, IApiHttpService apiHttpService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (apiHttpService == null) throw new ArgumentNullException(nameof(apiHttpService));

            ModelService = new ModelService(options, apiHttpService);
            TextCompletionService = new TextCompletionService(options, apiHttpService);
            TextEditService = new TextEditService(options, apiHttpService);
            ModerationService = new ModerationService(options, apiHttpService);
            EmbeddingsService = new EmbeddingsService(options, apiHttpService);
            ImageService = new ImageService(options, apiHttpService);
            FileService = new FileService(options, apiHttpService);
            FineTuneService = new FineTuneService(options, apiHttpService);
            TranscriptionService = new TranscriptionService(options, apiHttpService);
        }

        /// <summary>Creates a new service instance with individual options.</summary>
        /// <param name="options">The options.</param>
        /// <returns>
        ///   IOpenAIService
        /// </returns>
        /// <exception cref="System.ArgumentNullException">options</exception>
        public static IOpenAIService CreateService(OpenAIOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            IApiHttpClientFactory apiHttpClientFactory = new ApiHttpClientFactory(options);
            IApiHttpLoggerService apiHttpLoggerService = new ApiHttpLoggerService(options);
            IApiHttpService apiHttpService = new ApiHttpService(apiHttpClientFactory, apiHttpLoggerService, options);

            return CreateService(options, apiHttpService);
        }

        /// <summary>Creates a new service instance with individual options.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API HTTP service.</param>
        /// <returns>
        ///   IOpenAIService
        /// </returns>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiHttpService</exception>
        public static IOpenAIService CreateService(OpenAIOptions options, IApiHttpService apiHttpService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (apiHttpService == null) throw new ArgumentNullException(nameof(apiHttpService));

            return new OpenAIService(options, apiHttpService);
        }

        /// <summary>Gets the model service.</summary>
        /// <value>The model service.</value>
        public IModelService ModelService { get; }

        /// <summary>Gets the text completion service.</summary>
        /// <value>The text completion service.</value>
        public ITextCompletionService TextCompletionService { get; }

        /// <summary>Gets the text edit service.</summary>
        /// <value>The text edit service.</value>
        public ITextEditService TextEditService { get; }

        /// <summary>Gets the moderation service.</summary>
        /// <value>The moderation service.</value>
        public IModerationService ModerationService { get; }

        /// <summary>Gets the embeddings service.</summary>
        /// <value>The embeddings service.</value>
        public IEmbeddingsService EmbeddingsService { get; }

        /// <summary>Gets the image service.</summary>
        /// <value>The image service.</value>
        public IImageService ImageService { get; }

        /// <summary>Gets the file service.</summary>
        /// <value>The file service.</value>
        public IFileService FileService { get; }

        /// <summary>Gets the fine tune service.</summary>
        /// <value>The fine tune service.</value>
        public IFineTuneService FineTuneService { get; }

        /// <summary>Gets the transcription service.</summary>
        /// <value>The transcription service.</value>
        public ITranscriptionService TranscriptionService { get; }

        public ITranslationService TranslationService { get; }

    }

}
