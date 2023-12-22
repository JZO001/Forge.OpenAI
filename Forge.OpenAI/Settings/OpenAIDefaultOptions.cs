using System.Text.Json.Serialization;
using System.Text.Json;
using Forge.OpenAI.Models;
using Forge.OpenAI.Services.Endpoints;
using System;

namespace Forge.OpenAI.Settings
{

    /// <summary>Default options for OpenAI API</summary>
    public static class OpenAIDefaultOptions
    {

        /// <summary>Gets or sets the default base address.</summary>
        /// <value>The default base address.</value>
        public static string DefaultOpenAIBaseAddress { get; set; } = "https://api.openai.com";

        /// <summary>Gets or sets the default base address.</summary>
        /// <value>The default base address.</value>
        public static string DefaultAzureBaseAddress { get; set; } = "https://{0}.openai.azure.com";

        /// <summary>
        /// Gets or sets the default name of the azure resource.
        /// For more information, see https://learn.microsoft.com/en-us/azure/cognitive-services/openai/reference
        /// </summary>
        /// <value>The default name of the azure resource.</value>
        public static string DefaultAzureResourceName { get; set; } = string.Empty;

        /// <summary>The deployment name you chose when you deployed the model.</summary>
        /// <value>The default azure deployment identifier.</value>
        public static string DefaultAzureDeploymentId { get; set; } = string.Empty;

        /// <summary>The API version to use for this operation. This follows the YYYY-MM-DD format.</summary>
        public static string DefaultAzureApiVersion { get; set; } = AzureProviderEndpointApiVersions.Default;

        /// <summary>Gets or sets the default API version for the OpenAI provider.</summary>
        /// <value>The default API version.</value>
        public static string DefaultOpenAIApiVersion { get; set; } = "v1";

        /// <summary>Gets or sets the default embeddings model.</summary>
        /// <value>The default embeddings model.</value>
        public static string DefaultEmbeddingsModel { get; set; } = KnownModelTypes.TextEmbeddingAda002;

        /// <summary>Gets or sets the default moderation model.</summary>
        /// <value>The default moderation model.</value>
        public static string DefaultModerationModel { get; set; } = KnownModelTypes.TextModerationLatest;

        /// <summary>Gets or sets the default text completion model.</summary>
        /// <value>The default text completion model.</value>
        public static string DefaultTextCompletionModel { get; set; } = KnownModelTypes.TextDavinci003;

        /// <summary>Gets or sets the default text edit model.</summary>
        /// <value>The default text edit model.</value>
        public static string DefaultTextEditModel { get; set; } = KnownModelTypes.TextDavinciEdit001;

        /// <summary>Gets or sets the default chat completion model.</summary>
        /// <value>The default chat completion model.</value>
        public static string DefaultChatCompletionModel { get; set; } = KnownModelTypes.Gpt3_5Turbo;

        /// <summary>Gets or sets the default models URI.</summary>
        /// <value>The default models URI.</value>
        public static string DefaultModelsUri { get; set; } = "models";



        /// <summary>Gets or sets the default completions URI.</summary>
        /// <value>The default completions URI.</value>
        [Obsolete]
        public static string DefaultTextCompletionsUri { get; set; } = "completions";

        /// <summary>Gets or sets the default text edit URI.</summary>
        /// <value>The default text edit URI.</value>
        [Obsolete]
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
        [Obsolete]
        public static string DefaultFineTuneCreateUri { get; set; } = "fine-tunes";

        /// <summary>Gets or sets the default fine tune list URI.</summary>
        /// <value>The default fine tune list URI.</value>
        [Obsolete]
        public static string DefaultFineTuneListUri { get; set; } = "fine-tunes";

        /// <summary>Gets or sets the default fine tune get URI.</summary>
        /// <value>The default fine tune get URI.</value>
        [Obsolete]
        public static string DefaultFineTuneGetUri { get; set; } = "fine-tunes/{0}";

        /// <summary>Gets or sets the default fine tune cancel URI.</summary>
        /// <value>The default fine tune cancel URI.</value>
        [Obsolete]
        public static string DefaultFineTuneCancelUri { get; set; } = "fine-tunes/{0}/cancel";

        /// <summary>Gets or sets the default fine tune events URI.</summary>
        /// <value>The default fine tune events URI.</value>
        [Obsolete]
        public static string DefaultFineTuneEventsUri { get; set; } = "fine-tunes/{0}/events";

        /// <summary>Gets or sets the default fine tune streamed events URI.</summary>
        /// <value>The default fine tune streamed events URI.</value>
        [Obsolete]
        public static string DefaultFineTuneStreamedEventsUri { get; set; } = "fine-tunes/{0}/events?stream=true";



        /// <summary>Gets or sets the default fine tuning job create URI.</summary>
        /// <value>The default fine tune create URI.</value>
        public static string DefaultFineTuningJobCreateUri { get; set; } = "fine_tuning/jobs";

        /// <summary>Gets or sets the default fine tuning job list URI.</summary>
        /// <value>The default fine tune list URI.</value>
        public static string DefaultFineTuningJobListUri { get; set; } = "fine_tuning/jobs";

        /// <summary>Gets or sets the default fine tuning job get URI.</summary>
        /// <value>The default fine tune get URI.</value>
        public static string DefaultFineTuningJobGetUri { get; set; } = "fine_tuning/jobs/{0}";

        /// <summary>Gets or sets the default fine tuning job cancel URI.</summary>
        /// <value>The default fine tune cancel URI.</value>
        public static string DefaultFineTuningJobCancelUri { get; set; } = "fine_tuning/jobs/{0}/cancel";

        /// <summary>Gets or sets the default fine tuning job events URI.</summary>
        /// <value>The default fine tune events URI.</value>
        public static string DefaultFineTuningJobEventsUri { get; set; } = "fine_tuning/{0}/events";

        /// <summary>Gets or sets the default fine tune streamed events URI.</summary>
        /// <value>The default fine tune streamed events URI.</value>
        public static string DefaultFineTuningJobStreamedEventsUri { get; set; } = "fine_tuning/{0}/events?stream=true";



        /// <summary>Gets or sets the default fine tune delete model URI.</summary>
        /// <value>The default fine tune delete model URI.</value>
        public static string DefaultFineTuneDeleteModelUri { get; set; } = "models/{0}";

        /// <summary>Gets or sets the default audio speech URI.</summary>
        /// <value>The default audio speech URI.</value>
        public static string DefaultAudioSpeechUri { get; set; } = "audio/speech";

        /// <summary>Gets or sets the default audio transcript URI.</summary>
        /// <value>The default audio transcript URI.</value>
        public static string DefaultAudioTranscriptUri { get; set; } = "audio/transcriptions";

        /// <summary>Gets or sets the default audio translation URI.</summary>
        /// <value>The default audio translation URI.</value>
        public static string DefaultAudioTranslationUri { get; set; } = "audio/translations";

        /// <summary>Gets or sets the default chat completions URI.</summary>
        /// <value>The default chat completions URI.</value>
        public static string DefaultChatCompletionsUri { get; set; } = "chat/completions";


        /// <summary>Gets or sets the default assistant create URI.</summary>
        /// <value>The default assistant create URI.</value>
        public static string DefaultAssistantCreateUri { get; set; } = "assistants";

        /// <summary>Gets or sets the default assistant get URI.</summary>
        /// <value>The default assistant get URI.</value>
        public static string DefaultAssistantGetUri { get; set; } = "assistants/{0}";

        /// <summary>Gets or sets the default assistant list URI.</summary>
        /// <value>The default assistant list URI.</value>
        public static string DefaultAssistantListUri { get; set; } = "assistants";

        /// <summary>Gets or sets the default assistant modify URI.</summary>
        /// <value>The default assistant modify URI.</value>
        public static string DefaultAssistantModifyUri { get; set; } = "assistants/{0}";

        /// <summary>Gets or sets the default assistant delete URI.</summary>
        /// <value>The default assistant delete URI.</value>
        public static string DefaultAssistantDeleteUri { get; set; } = "assistants/{0}";

        /// <summary>Gets or sets the default assistant file create URI.</summary>
        /// <value>The default assistant file create URI.</value>
        public static string DefaultAssistantFileCreateUri { get; set; } = "assistants/{0}/files";

        /// <summary>Gets or sets the default assistant file get URI.</summary>
        /// <value>The default assistant file get URI.</value>
        public static string DefaultAssistantFileGetUri { get; set; } = "assistants/{0}/files/{1}";

        /// <summary>Gets or sets the default assistant file list URI.</summary>
        /// <value>The default assistant file list URI.</value>
        public static string DefaultAssistantFileListUri { get; set; } = "assistants/{0}/files";

        /// <summary>Gets or sets the default assistant file delete URI.</summary>
        /// <value>The default assistant file delete URI.</value>
        public static string DefaultAssistantFileDeleteUri { get; set; } = "assistants/{0}/files/{1}";



        /// <summary>Gets or sets a value indicating whether [default log requests and responses].</summary>
        /// <value>
        ///   <c>true</c> if [default log requests and responses]; otherwise, <c>false</c>.</value>
        public static bool DefaultLogRequestsAndResponses { get; set; } = false;

        /// <summary>Gets or sets the default log requests and responses folder.</summary>
        /// <value>The default log requests and responses folder.</value>
        public static string DefaultLogRequestsAndResponsesFolder { get; set; } = "netlogs";

        /// <summary>Gets or sets the default HTTP client timeout in milliseconds.</summary>
        /// <value>The default HTTP client timeout in milliseconds.</value>
        public static int? DefaultHttpClientTimeoutInMilliseconds { get; set; }

        /// <summary>The default json serializer options</summary>
        public static JsonSerializerOptions DefaultJsonSerializerOptions { get; set; } = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

    }

}
