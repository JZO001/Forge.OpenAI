using Forge.OpenAI.Authentication;
using System;
using System.Net.Http;
using System.Text.Json;

namespace Forge.OpenAI.Infrastructure
{

    /// <summary>Represents the settings of the OpenAI client</summary>
    public class OpenAIOptions
    {

        /// <summary>Gets or sets the base address of the HttpClient.</summary>
        /// <value>The base address.</value>
        public string BaseAddress { get; set; } = OpenAIDefaultOptions.DefaultBaseAddress;

        /// <summary>Gets or sets the API version.</summary>
        /// <value>The API version.</value>
        public string ApiVersion { get; set; } = OpenAIDefaultOptions.DefaultApiVersion;

        /// <summary>Gets or sets the models URI.</summary>
        /// <value>The models URI.</value>
        public string ModelsUri { get; set; } = OpenAIDefaultOptions.DefaultModelsUri;

        /// <summary>Gets or sets the completions URI.</summary>
        /// <value>The completions URI.</value>
        public string TextCompletionsUri { get; set; } = OpenAIDefaultOptions.DefaultTextCompletionsUri;

        /// <summary>Gets or sets the text edit URI.</summary>
        /// <value>The text edit URI.</value>
        public string TextEditUri { get; set; } = OpenAIDefaultOptions.DefaultTextEditUri;

        /// <summary>Gets or sets the moderation URI.</summary>
        /// <value>The moderation URI.</value>
        public string ModerationUri { get; set; } = OpenAIDefaultOptions.DefaultModerationUri;

        /// <summary>Gets or sets the embeddings URI.</summary>
        /// <value>The embeddings URI.</value>
        public string EmbeddingsUri { get; set; } = OpenAIDefaultOptions.DefaultEmbeddingsUri;

        /// <summary>Gets or sets the image create URI.</summary>
        /// <value>The image create URI.</value>
        public string ImageCreateUri { get; set; } = OpenAIDefaultOptions.DefaultImageCreateUri;

        /// <summary>Gets or sets the image edit URI.</summary>
        /// <value>The image edit URI.</value>
        public string ImageEditUri { get; set; } = OpenAIDefaultOptions.DefaultImageEditUri;

        /// <summary>Gets or sets the image variation URI.</summary>
        /// <value>The image variation URI.</value>
        public string ImageVariationUri { get; set; } = OpenAIDefaultOptions.DefaultImageVariationUri;

        /// <summary>Gets or sets the file list URI.</summary>
        /// <value>The file list URI.</value>
        public string FileListUri { get; set; } = OpenAIDefaultOptions.DefaultFileListUri;

        /// <summary>Gets or sets the file upload URI.</summary>
        /// <value>The file upload URI.</value>
        public string FileUploadUri { get; set; } = OpenAIDefaultOptions.DefaultFileUploadUri;

        /// <summary>Gets or sets the file data URI.</summary>
        /// <value>The file data URI.</value>
        public string FileDataUri { get; set; } = OpenAIDefaultOptions.DefaultFileDataUri;

        /// <summary>Gets or sets the file download URI.</summary>
        /// <value>The file download URI.</value>
        public string FileDownloadUri { get; set; } = OpenAIDefaultOptions.DefaultFileDownloadUri;

        /// <summary>Gets or sets the file delete URI.</summary>
        /// <value>The file delete URI.</value>
        public string FileDeleteUri { get; set; } = OpenAIDefaultOptions.DefaultFileDeleteUri;

        /// <summary>Gets or sets the default fine tune create URI.</summary>
        /// <value>The default fine tune create URI.</value>
        public string FineTuneCreateUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuneCreateUri;

        /// <summary>Gets or sets the default fine tune list URI.</summary>
        /// <value>The default fine tune list URI.</value>
        public string FineTuneListUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuneListUri;

        /// <summary>Gets or sets the fine tune get URI.</summary>
        /// <value>The default fine tune get URI.</value>
        public string FineTuneGetUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuneGetUri;

        /// <summary>Gets or sets the fine tune cancel URI.</summary>
        /// <value>The default fine tune cancel URI.</value>
        public string FineTuneCancelUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuneCancelUri;

        /// <summary>Gets or sets the fine tune events URI.</summary>
        /// <value>The default fine tune events URI.</value>
        public string FineTuneEventsUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuneEventsUri;

        /// <summary>Gets or sets the fine tune streamed events URI.</summary>
        /// <value>The fine tune streamed events URI.</value>
        public string FineTuneStreamedEventsUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuneStreamedEventsUri;

        /// <summary>Gets or sets the fine tune delete model URI.</summary>
        /// <value>The default fine tune delete model URI.</value>
        public string FineTuneDeleteModelUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuneDeleteModelUri;

        /// <summary>Gets or sets a value indicating whether [log requests and responses].</summary>
        /// <value>
        ///   <c>true</c> if [log requests and responses]; otherwise, <c>false</c>.</value>
        public bool LogRequestsAndResponses { get; set; } = OpenAIDefaultOptions.DefaultLogRequestsAndResponses;

        /// <summary>Gets or sets the log requests and responses folder.</summary>
        /// <value>The log requests and responses folder.</value>
        public string LogRequestsAndResponsesFolder { get; set; } = OpenAIDefaultOptions.DefaultLogRequestsAndResponsesFolder;

        /// <summary>Gets or sets the json serializer options.</summary>
        /// <value>The json serializer options.</value>
        public JsonSerializerOptions JsonSerializerOptions { get; set; } = OpenAIDefaultOptions.DefaultJsonSerializerOptions;

        /// <summary>Gets or sets the HTTP message handler for the HttpClient.</summary>
        /// <value>The HTTP message handler.</value>
        public Func<HttpMessageHandler> HttpMessageHandlerFactory { get; set; }

        /// <summary>Gets or sets the authentication data.
        /// Here your can optionally set the OpenAI apiKey and the Organization information.</summary>
        /// <value>The authentication information.</value>
        public AuthenticationInfo AuthenticationInfo { get; set; } = AuthenticationInfo.Default;

    }

}
