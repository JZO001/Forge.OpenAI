using Forge.OpenAI.Infrastructure;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Forge.OpenAI.Services
{

    /// <summary>Represents the OpenAI service</summary>
    public class OpenAIService : IOpenAIService
    {

        /// <summary>Initializes a new instance of the <see cref="OpenAIService" /> class.</summary>
        /// <param name="modelService">The model service.</param>
        /// <param name="moderationService">The moderation service.</param>
        /// <param name="embeddingsService">The embeddings service.</param>
        /// <param name="imageService">The image service.</param>
        /// <param name="fileService">The file service.</param>
        /// <param name="fineTuningJobService">The fine tune job service.</param>
        /// <param name="transcriptionService">The transcription service.</param>
        /// <param name="speechService">The speech service.</param>
        /// <param name="translationService">The translation service.</param>
        /// <param name="chatCompletionService">The chat completion service.</param>
        /// <param name="assistantService">The assistant service.</param>
        /// <param name="assistantFileService">The assistant file service.</param>
        /// <param name="threadsService">The assistant file service.</param>
        /// <param name="messagesService">The message service.</param>
        /// <param name="messageFileService">The message file service.</param>
        /// <param name="runService">The run service.</param>
        /// <param name="runStepService">The run step service.</param>
        /// <param name="batchService">The batch service.</param>
        /// <param name="vectorStoreService">The vector store service.</param>
        /// <param name="vectorStoreFileService">The vector store file service.</param>
        /// <param name="vectorStoreFileBatchService">The vector store file batch service.</param>
        /// <exception cref="System.ArgumentNullException">
        /// modelService
        /// or
        /// moderationService
        /// or
        /// embeddingsService
        /// or
        /// imageService
        /// or
        /// fileService
        /// or
        /// fineTuningJobService
        /// or
        /// speechService
        /// or
        /// transcriptionService
        /// or
        /// translationService
        /// or
        /// chatCompletionService
        /// or
        /// assistantService
        /// or
        /// assistantFileService
        /// or
        /// threadsService
        /// or
        /// messagesService
        /// or
        /// messageFileService
        /// or
        /// runService
        /// or
        /// runStepService
        /// or
        /// batchService
        /// </exception>
        public OpenAIService(IModelService modelService,
            IModerationService moderationService,
            IEmbeddingsService embeddingsService,
            IImageService imageService,
            IFileService fileService,
            IFineTuningJobService fineTuningJobService,
            ISpeechService speechService,
            ITranscriptionService transcriptionService,
            ITranslationService translationService,
            IChatCompletionService chatCompletionService,
            IAssistantService assistantService,
            IAssistantFileService assistantFileService,
            IThreadsService threadsService,
            IMessageService messagesService,
            IMessageFileService messageFileService,
            IRunService runService,
            IRunStepService runStepService,
            IBatchService batchService,
            IVectorStoreService vectorStoreService,
            IVectorStoreFileService vectorStoreFileService,
            IVectorStoreFileBatchService vectorStoreFileBatchService)
        {
            if (modelService == null) throw new ArgumentNullException(nameof(modelService));
            if (moderationService == null) throw new ArgumentNullException(nameof(moderationService));
            if (embeddingsService == null) throw new ArgumentNullException(nameof(embeddingsService));
            if (imageService == null) throw new ArgumentNullException(nameof(imageService));
            if (fileService == null) throw new ArgumentNullException(nameof(fileService));
            if (fineTuningJobService == null) throw new ArgumentNullException(nameof(fineTuningJobService));
            if (speechService == null) throw new ArgumentNullException(nameof(speechService));
            if (transcriptionService == null) throw new ArgumentNullException(nameof(transcriptionService));
            if (translationService == null) throw new ArgumentNullException(nameof(translationService));
            if (chatCompletionService == null) throw new ArgumentNullException(nameof(chatCompletionService));
            if (assistantService == null) throw new ArgumentNullException(nameof(assistantService));
            if (assistantFileService == null) throw new ArgumentNullException(nameof(assistantFileService));
            if (threadsService == null) throw new ArgumentNullException(nameof(threadsService));
            if (messagesService == null) throw new ArgumentNullException(nameof(messagesService));
            if (messageFileService == null) throw new ArgumentNullException(nameof(messageFileService));
            if (runService == null) throw new ArgumentNullException(nameof(runService));
            if (runStepService == null) throw new ArgumentNullException(nameof(runStepService));
            if (batchService == null) throw new ArgumentNullException(nameof(batchService));
            if (vectorStoreService == null) throw new ArgumentNullException(nameof(vectorStoreService));
            if (vectorStoreFileService == null) throw new ArgumentNullException(nameof(vectorStoreFileService));
            if (vectorStoreFileBatchService == null) throw new ArgumentNullException(nameof(vectorStoreFileBatchService));

            ModelService = modelService;
            ModerationService = moderationService;
            EmbeddingsService = embeddingsService;
            ImageService = imageService;
            FileService = fileService;
            FineTuningJobService = fineTuningJobService;
            SpeechService = speechService;
            TranscriptionService = transcriptionService;
            TranslationService = translationService;
            ChatCompletionService = chatCompletionService;
            AssistantService = assistantService;
            AssistantFileService = assistantFileService;
            ThreadsService = threadsService;
            MessagesService = messagesService;
            MessageFileService = messageFileService;
            RunService = runService;
            RunStepService = runStepService;
            BatchService = batchService;
            VectorStoreService = vectorStoreService;
            VectorStoreFileService = vectorStoreFileService;
            VectorStoreFileBatchService = vectorStoreFileBatchService;
        }

        /// <summary>
        /// Creates a new service instance with individual options.
        /// The method gets back the ServiceProvider instance for further use.
        /// It is the caller responsibility to dispose it, at the end of the OpenAIService instance lifecycle.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The constructed IServiceProvider instance.</param>
        /// <returns>
        ///   IOpenAIService
        /// </returns>
        public static IOpenAIService CreateService(OpenAIOptions options, out ServiceProvider serviceProvider)
        {
            return CreateService((OpenAIOptions o) =>
            {
                MappingHelper.Map(options, o);
            }, out serviceProvider);
        }

        /// <summary>
        /// Creates a new service instance with individual options.
        /// The method gets back the ServiceProvider instance for further use.
        /// It is the caller responsibility to dispose it, at the end of the OpenAIService instance lifecycle.
        /// </summary>
        /// <param name="configure">The configuration handler.</param>
        /// <param name="serviceProvider">The constructed IServiceProvider instance.</param>
        /// <returns>
        ///   IOpenAIService
        /// </returns>
        /// <exception cref="System.ArgumentNullException">configure</exception>
        public static IOpenAIService CreateService(Action<OpenAIOptions> configure, out ServiceProvider serviceProvider)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            ServiceCollection services = new ServiceCollection();
            services.AddForgeOpenAI(configure);
            serviceProvider = services.BuildServiceProvider();

            return CreateService(serviceProvider);
        }

        /// <summary>
        /// Creates a new service instance with individual options.
        /// The method gets back the ServiceProvider instance for further use.
        /// It is the caller responsibility to dispose it, at the end of the OpenAIService instance lifecycle.
        /// </summary>
        /// <param name="configure">The configuration handler.</param>
        /// <param name="serviceProvider">The constructed IServiceProvider instance.</param>
        /// <returns>
        ///   IOpenAIService
        /// </returns>
        /// <exception cref="System.ArgumentNullException">options</exception>
        public static IOpenAIService CreateService(Action<IServiceCollection> configure, out ServiceProvider serviceProvider)
        {
            if (configure == null) throw new ArgumentNullException(nameof(configure));

            ServiceCollection services = new ServiceCollection();
            configure(services);
            serviceProvider = services.BuildServiceProvider();

            return CreateService(serviceProvider);
        }

        /// <summary>Creates a new service instance with individual options.</summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns>
        ///   IOpenAIService
        /// </returns>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiHttpService</exception>
        public static IOpenAIService CreateService(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));

            return new OpenAIService(
                serviceProvider.GetRequiredService<IModelService>(),
                serviceProvider.GetRequiredService<IModerationService>(),
                serviceProvider.GetRequiredService<IEmbeddingsService>(),
                serviceProvider.GetRequiredService<IImageService>(),
                serviceProvider.GetRequiredService<IFileService>(),
                serviceProvider.GetRequiredService<IFineTuningJobService>(),
                serviceProvider.GetRequiredService<ISpeechService>(),
                serviceProvider.GetRequiredService<ITranscriptionService>(),
                serviceProvider.GetRequiredService<ITranslationService>(),
                serviceProvider.GetRequiredService<IChatCompletionService>(),
                serviceProvider.GetRequiredService<IAssistantService>(),
                serviceProvider.GetRequiredService<IAssistantFileService>(),
                serviceProvider.GetRequiredService<IThreadsService>(),
                serviceProvider.GetRequiredService<IMessageService>(),
                serviceProvider.GetRequiredService<IMessageFileService>(),
                serviceProvider.GetRequiredService<IRunService>(),
                serviceProvider.GetRequiredService<IRunStepService>(),
                serviceProvider.GetRequiredService<IBatchService>(),
                serviceProvider.GetRequiredService<IVectorStoreService>(),
                serviceProvider.GetRequiredService<IVectorStoreFileService>(),
                serviceProvider.GetRequiredService<IVectorStoreFileBatchService>()
            );
        }

        /// <summary>Gets the model service.</summary>
        /// <value>The model service.</value>
        public IModelService ModelService { get; }

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

        /// <summary>Gets the fine tuning job service.</summary>
        /// <value>The fine tuning job service.</value>
        public IFineTuningJobService FineTuningJobService { get; }

        /// <summary>Gets the speech service.</summary>
        /// <value>The speech service.</value>
        public ISpeechService SpeechService { get; }

        /// <summary>Gets the transcription service.</summary>
        /// <value>The transcription service.</value>
        public ITranscriptionService TranscriptionService { get; }

        /// <summary>Gets the translation service.</summary>
        /// <value>The translation service.</value>
        public ITranslationService TranslationService { get; }

        /// <summary>Gets the chat completion service.</summary>
        /// <value>The chat completion service.</value>
        public IChatCompletionService ChatCompletionService { get; }

        /// <summary>Gets the assistant service.</summary>
        /// <value>The assistant service.</value>
        public IAssistantService AssistantService { get; }

        /// <summary>Gets the assistant file service.</summary>
        /// <value>The assistant file service.</value>
        public IAssistantFileService AssistantFileService { get; }

        /// <summary>Gets the thread file service.</summary>
        /// <value>The thread file service.</value>
        public IThreadsService ThreadsService { get; }

        /// <summary>Gets the message service.</summary>
        /// <value>The message service.</value>
        public IMessageService MessagesService { get; }

        /// <summary>Gets the message file service.</summary>
        /// <value>The message file service.</value>
        public IMessageFileService MessageFileService { get; }

        /// <summary>Gets the run service.</summary>
        /// <value>The run service.</value>
        public IRunService RunService { get; }

        /// <summary>Gets the run step service.</summary>
        /// <value>The run step service.</value>
        public IRunStepService RunStepService { get; }

        /// <summary>Gets the batch service.</summary>
        /// <value>The batch service.</value>
        public IBatchService BatchService { get; }

        /// <summary>
        /// Gets the vector store service.
        /// </summary>
        /// <value>
        /// The vector store service.
        /// </value>
        public IVectorStoreService VectorStoreService { get; }

        /// <summary>
        /// Gets the vector store file service.
        /// </summary>
        /// <value>
        /// The vector store file service.
        /// </value>
        public IVectorStoreFileService VectorStoreFileService { get; }

        /// <summary>
        /// Gets the vector store file batch service.
        /// </summary>
        /// <value>
        /// The vector store file batch service.
        /// </value>
        public IVectorStoreFileBatchService VectorStoreFileBatchService { get; }

    }

}
