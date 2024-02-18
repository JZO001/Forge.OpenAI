using System;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>Represents the OpenAI service</summary>
    public interface IOpenAIService
    {

        /// <summary>Gets the model service.</summary>
        /// <value>The model service.</value>
        IModelService ModelService { get; }

        /// <summary>Gets the text completion service.</summary>
        /// <value>The text completion service.</value>
        [Obsolete]
        ITextCompletionService TextCompletionService { get; }

        /// <summary>Gets the text edit service.</summary>
        /// <value>The text edit service.</value>
        [Obsolete]
        ITextEditService TextEditService { get; }

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

        /// <summary>Gets the fine tune service.</summary>
        /// <value>The fine tune service.</value>
        [Obsolete]
        IFineTuneService FineTuneService { get; }

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

    }

}
