using Forge.OpenAI.Infrastructure;
using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Services.Endpoints;
using Forge.OpenAI.Settings;
using System;

namespace Forge.OpenAI.Services
{

    /// <summary>Represents the OpenAI service</summary>
    public class OpenAIService : IOpenAIService
    {

        /// <summary>Initializes a new instance of the <see cref="OpenAIService" /> class.</summary>
        /// <param name="modelService">The model service.</param>
        /// <param name="textCompletionService">The text completion service.</param>
        /// <param name="textEditService">The text edit service.</param>
        /// <param name="moderationService">The moderation service.</param>
        /// <param name="embeddingsService">The embeddings service.</param>
        /// <param name="imageService">The image service.</param>
        /// <param name="fileService">The file service.</param>
        /// <param name="fineTuneService">The fine tune service.</param>
        /// <param name="transcriptionService">The transcription service.</param>
        /// <param name="translationService">The translation service.</param>
        /// <param name="chatCompletionService">The chat completion service.</param>
        /// <exception cref="System.ArgumentNullException">
        /// modelService
        /// or
        /// textCompletionService
        /// or
        /// textEditService
        /// or
        /// moderationService
        /// or
        /// embeddingsService
        /// or
        /// imageService
        /// or
        /// fileService
        /// or
        /// fineTuneService
        /// or
        /// transcriptionService
        /// or
        /// translationService
        /// </exception>
        public OpenAIService(IModelService modelService,
            ITextCompletionService textCompletionService,
            ITextEditService textEditService,
            IModerationService moderationService,
            IEmbeddingsService embeddingsService,
            IImageService imageService,
            IFileService fileService,
            IFineTuneService fineTuneService,
            ITranscriptionService transcriptionService,
            ITranslationService translationService,
            IChatCompletionService chatCompletionService)
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
            if (chatCompletionService == null) throw new ArgumentNullException(nameof(chatCompletionService));

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
            ChatCompletionService = chatCompletionService;
        }

        /// <summary>Initializes a new instance of the <see cref="OpenAIService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API HTTP service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiHttpService</exception>
        public OpenAIService(OpenAIOptions options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (apiHttpService == null) throw new ArgumentNullException(nameof(apiHttpService));
            if (providerEndpointService == null) throw new ArgumentNullException(nameof(providerEndpointService));

            ModelService = new ModelService(options, apiHttpService, providerEndpointService);
            TextCompletionService = new TextCompletionService(options, apiHttpService, providerEndpointService);
            TextEditService = new TextEditService(options, apiHttpService, providerEndpointService);
            ModerationService = new ModerationService(options, apiHttpService, providerEndpointService);
            EmbeddingsService = new EmbeddingsService(options, apiHttpService, providerEndpointService);
            ImageService = new ImageService(options, apiHttpService, providerEndpointService);
            FileService = new FileService(options, apiHttpService, providerEndpointService);
            FineTuneService = new FineTuneService(options, apiHttpService, providerEndpointService);
            TranscriptionService = new TranscriptionService(options, apiHttpService, providerEndpointService);
            TranslationService = new TranslationService(options, apiHttpService, providerEndpointService);
            ChatCompletionService = new ChatCompletionService(options, apiHttpService, providerEndpointService);
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
            
            IProviderEndpointService providerEndpointService = null;
            if (options.AIServiceProviderType == AIServiceProviderTypeEnum.OpenAI)
                providerEndpointService = new OpenAIProviderEndpointService(options);
            else
                providerEndpointService = new AzureProviderEndpointService(options);

            IApiHttpService apiHttpService = new ApiHttpService(apiHttpClientFactory, apiHttpLoggerService, providerEndpointService, options);

            return CreateService(options, apiHttpService, providerEndpointService);
        }

        /// <summary>Creates a new service instance with individual options.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API HTTP service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        /// <returns>
        ///   IOpenAIService
        /// </returns>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiHttpService</exception>
        public static IOpenAIService CreateService(OpenAIOptions options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (apiHttpService == null) throw new ArgumentNullException(nameof(apiHttpService));
            if (providerEndpointService == null) throw new ArgumentNullException(nameof(providerEndpointService));

            return new OpenAIService(options, apiHttpService, providerEndpointService);
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

        /// <summary>Gets the translation service.</summary>
        /// <value>The translation service.</value>
        public ITranslationService TranslationService { get; }

        /// <summary>Gets the chat completion service.</summary>
        /// <value>The chat completion service.</value>
        public IChatCompletionService ChatCompletionService { get; }

    }

}
