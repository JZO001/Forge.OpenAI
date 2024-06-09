using System;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>Represents the OpenAI service</summary>
    public interface IOpenAIService
    {

        /// <summary>Gets the model service.</summary>
        /// <value>The model service.</value>
        IModelService ModelService { get; }

        /// <summary>Gets the moderation service.</summary>
        /// <value>The moderation service.</value>
        IModerationService ModerationService { get; }

        /// <summary>Gets the embeddings service.</summary>
        /// <value>The embeddings service.</value>
        IEmbeddingsService EmbeddingsService { get; }

        /// <summary>Gets the image service.</summary>
        /// <value>The image service.</value>
        IImageService ImageService { get; }

        /// <summary>Gets the file service.</summary>
        /// <value>The file service.</value>
        IFileService FileService { get; }

        /// <summary>Gets the fine tuning job service.</summary>
        /// <value>The fine tuning job service.</value>
        IFineTuningJobService FineTuningJobService { get; }

        /// <summary>Gets the speech service.</summary>
        /// <value>The speech service.</value>
        ISpeechService SpeechService { get; }

        /// <summary>Gets the transcription service.</summary>
        /// <value>The transcription service.</value>
        ITranscriptionService TranscriptionService { get; }

        /// <summary>Gets the translation service.</summary>
        /// <value>The translation service.</value>
        ITranslationService TranslationService { get; }

        /// <summary>Gets the chat completion service.</summary>
        /// <value>The chat completion service.</value>
        IChatCompletionService ChatCompletionService { get; }

        /// <summary>Gets the assistant service.</summary>
        /// <value>The assistant service.</value>
        IAssistantService AssistantService { get; }

        /// <summary>Gets the assistant file service.</summary>
        /// <value>The assistant file service.</value>
        IAssistantFileService AssistantFileService { get; }

        /// <summary>Gets the thread service.</summary>
        /// <value>The thread service.</value>
        IThreadsService ThreadsService { get; }

        /// <summary>Gets the message service.</summary>
        /// <value>The message service.</value>
        IMessageService MessagesService { get; }

        /// <summary>Gets the message file service.</summary>
        /// <value>The message file service.</value>
        IMessageFileService MessageFileService { get; }

        /// <summary>Gets the run service.</summary>
        /// <value>The run service.</value>
        IRunService RunService { get; }

        /// <summary>Gets the run step service.</summary>
        /// <value>The run step service.</value>
        IRunStepService RunStepService { get; }

        /// <summary>Gets the batch service.</summary>
        /// <value>The batch service.</value>
        IBatchService BatchService { get; }

        /// <summary>
        /// Gets the vector store service.
        /// </summary>
        /// <value>
        /// The vector store service.
        /// </value>
        IVectorStoreService VectorStoreService { get; }

        /// <summary>
        /// Gets the vector store file service.
        /// </summary>
        /// <value>
        /// The vector store file service.
        /// </value>
        IVectorStoreFileService VectorStoreFileService { get; }

        /// <summary>
        /// Gets the vector store file batch service.
        /// </summary>
        /// <value>
        /// The vector store file batch service.
        /// </value>
        IVectorStoreFileBatchService VectorStoreFileBatchService { get; }

    }

}
