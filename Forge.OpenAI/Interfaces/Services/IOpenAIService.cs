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
        ITextCompletionService TextCompletionService { get; }

        /// <summary>Gets the text edit service.</summary>
        /// <value>The text edit service.</value>
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
        IFineTuneService FineTuneService { get; }

    }

}
