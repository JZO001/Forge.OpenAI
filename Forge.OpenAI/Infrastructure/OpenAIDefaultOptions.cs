using System.Text.Json.Serialization;
using System.Text.Json;
using System;

namespace Forge.OpenAI.Infrastructure
{

    /// <summary>Default options for OpenAI API</summary>
    public static class OpenAIDefaultOptions
    {

        /// <summary>Gets or sets the default base address.</summary>
        /// <value>The default base address.</value>
        public static string DefaultBaseAddress { get; set; } = "https://api.openai.com";

        /// <summary>Gets or sets the default API version.</summary>
        /// <value>The default API version.</value>
        public static string DefaultApiVersion { get; set; } = "v1";

        /// <summary>Gets or sets the default models URI.</summary>
        /// <value>The default models URI.</value>
        public static string DefaultModelsUri { get; set; } = "models";

        /// <summary>Gets or sets the default completions URI.</summary>
        /// <value>The default completions URI.</value>
        public static string DefaultTextCompletionsUri { get; set; } = "completions";

        /// <summary>Gets or sets the default text edit URI.</summary>
        /// <value>The default text edit URI.</value>
        public static string DefaultTextEditUri { get; set; } = "edits";

        /// <summary>Gets or sets the default moderation URI.</summary>
        /// <value>The default moderation URI.</value>
        public static string DefaultModerationUri { get; set; } = "moderations";

        /// <summary>Gets or sets the default embeddings URI.</summary>
        /// <value>The default embeddings URI.</value>
        public static string DefaultEmbeddingsUri { get; set; } = "embeddings";

        /// <summary>Gets or sets the default image create URI.</summary>
        /// <value>The default image create URI.</value>
        public static string DefaultImageCreateUri { get; set; } = "images/generations";

        /// <summary>Gets or sets the default image edit URI.</summary>
        /// <value>The default image edit URI.</value>
        public static string DefaultImageEditUri { get; set; } = "images/edits";

        /// <summary>Gets or sets the default image variation URI.</summary>
        /// <value>The default image variation URI.</value>
        public static string DefaultImageVariationUri { get; set; } = "images/variations";

        /// <summary>Gets or sets the default file list URI.</summary>
        /// <value>The default file list URI.</value>
        public static string DefaultFileListUri { get; set; } = "files";

        /// <summary>Gets or sets the default file upload URI.</summary>
        /// <value>The default file upload URI.</value>
        public static string DefaultFileUploadUri { get; set; } = "files";

        /// <summary>Gets or sets the default file data URI.</summary>
        /// <value>The default file data URI.</value>
        public static string DefaultFileDataUri { get; set; } = "files/{0}";

        /// <summary>Gets or sets the default file download URI.</summary>
        /// <value>The default file download URI.</value>
        public static string DefaultFileDownloadUri { get; set; } = "files/{0}/content";

        /// <summary>Gets or sets the default file delete URI.</summary>
        /// <value>The default file delete URI.</value>
        public static string DefaultFileDeleteUri { get; set; } = "files";

        /// <summary>Gets or sets the default fine tune create URI.</summary>
        /// <value>The default fine tune create URI.</value>
        public static string DefaultFineTuneCreateUri { get; set; } = "fine-tunes";

        /// <summary>Gets or sets the default fine tune list URI.</summary>
        /// <value>The default fine tune list URI.</value>
        public static string DefaultFineTuneListUri { get; set; } = "fine-tunes";

        /// <summary>Gets or sets the default fine tune get URI.</summary>
        /// <value>The default fine tune get URI.</value>
        public static string DefaultFineTuneGetUri { get; set; } = "fine-tunes/{0}";

        /// <summary>Gets or sets the default fine tune cancel URI.</summary>
        /// <value>The default fine tune cancel URI.</value>
        public static string DefaultFineTuneCancelUri { get; set; } = "fine-tunes/{0}/cancel";

        /// <summary>Gets or sets the default fine tune events URI.</summary>
        /// <value>The default fine tune events URI.</value>
        public static string DefaultFineTuneEventsUri { get; set; } = "fine-tunes/{0}/events";

        /// <summary>Gets or sets the default fine tune streamed events URI.</summary>
        /// <value>The default fine tune streamed events URI.</value>
        public static string DefaultFineTuneStreamedEventsUri { get; set; } = "fine-tunes/{0}/events?stream=true";

        /// <summary>Gets or sets the default fine tune delete model URI.</summary>
        /// <value>The default fine tune delete model URI.</value>
        public static string DefaultFineTuneDeleteModelUri { get; set; } = "models/{0}";

        /// <summary>Gets or sets a value indicating whether [default log requests and responses].</summary>
        /// <value>
        ///   <c>true</c> if [default log requests and responses]; otherwise, <c>false</c>.</value>
        public static bool DefaultLogRequestsAndResponses { get; set; } = false;

        /// <summary>Gets or sets the default log requests and responses folder.</summary>
        /// <value>The default log requests and responses folder.</value>
        public static string DefaultLogRequestsAndResponsesFolder { get; set; } = "netlogs";

        /// <summary>The default json serializer options</summary>
        public static JsonSerializerOptions DefaultJsonSerializerOptions { get; set; } = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

    }

}
