using Forge.OpenAI.Authentication;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Settings
{

    /// <summary>Represents the common settings of the providers</summary>
    public class OpenAIOptions : ICloneable
    {

        /// <summary>The configuration section name</summary>
        public static readonly string ConfigurationSectionName = typeof(OpenAIOptions).Name;

        /// <summary>Gets the type of the AI provider. Currently OpenAI and Azure-OpenAI providers are supported.</summary>
        /// <value>The type of the provider.</value>
        public AIServiceProviderTypeEnum AIServiceProviderType { get; set; } = AIServiceProviderTypeEnum.OpenAI;

        /// <summary>Gets or sets the base address of the HttpClient for the OpenAI provider.</summary>
        /// <value>The base address.</value>
        public string BaseAddress { get; set; } = OpenAIDefaultOptions.DefaultOpenAIBaseAddress;

        /// <summary>Gets or sets the API version of the OpenAI provider.</summary>
        /// <value>The API version.</value>
        public string OpenAIApiVersion { get; set; } = OpenAIDefaultOptions.DefaultOpenAIApiVersion;

        /// <summary>Gets or sets the API version for Azure provider.</summary>
        /// <value>The API version.</value>
        public string AzureApiVersion { get; set; } = OpenAIDefaultOptions.DefaultAzureApiVersion;

        /// <summary>
        /// Gets or sets the default name of the azure resource.
        /// For more information, see https://learn.microsoft.com/en-us/azure/cognitive-services/openai/reference
        /// </summary>
        /// <value>The default name of the azure resource.</value>
        public string AzureResourceName { get; set; } = OpenAIDefaultOptions.DefaultAzureResourceName;

        /// <summary>
        /// The deployment name you chose when you deployed the model.
        /// For more information, see https://learn.microsoft.com/en-us/azure/cognitive-services/openai/reference
        /// </summary>
        /// <value>The default azure deployment identifier.</value>
        public string AzureDeploymentId { get; set; } = OpenAIDefaultOptions.DefaultAzureDeploymentId;

        /// <summary>Gets or sets the models URI.</summary>
        /// <value>The models URI.</value>
        public string ModelsUri { get; set; } = OpenAIDefaultOptions.DefaultModelsUri;



        /// <summary>Gets or sets the completions URI.</summary>
        /// <value>The completions URI.</value>
        [Obsolete]
        public string TextCompletionsUri { get; set; } = OpenAIDefaultOptions.DefaultTextCompletionsUri;

        /// <summary>Gets or sets the text edit URI.</summary>
        /// <value>The text edit URI.</value>
        [Obsolete]
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
        [Obsolete]
        public string FineTuneCreateUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuneCreateUri;

        /// <summary>Gets or sets the default fine tune list URI.</summary>
        /// <value>The default fine tune list URI.</value>
        [Obsolete]
        public string FineTuneListUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuneListUri;

        /// <summary>Gets or sets the fine tune get URI.</summary>
        /// <value>The default fine tune get URI.</value>
        [Obsolete]
        public string FineTuneGetUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuneGetUri;

        /// <summary>Gets or sets the fine tune cancel URI.</summary>
        /// <value>The default fine tune cancel URI.</value>
        [Obsolete]
        public string FineTuneCancelUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuneCancelUri;

        /// <summary>Gets or sets the fine tune events URI.</summary>
        /// <value>The default fine tune events URI.</value>
        [Obsolete]
        public string FineTuneEventsUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuneEventsUri;

        /// <summary>Gets or sets the fine tune streamed events URI.</summary>
        /// <value>The fine tune streamed events URI.</value>
        [Obsolete]
        public string FineTuneStreamedEventsUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuneStreamedEventsUri;

        /// <summary>Gets or sets the fine tune delete model URI.</summary>
        /// <value>The default fine tune delete model URI.</value>
        [Obsolete]
        public string FineTuneDeleteModelUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuneDeleteModelUri;



        /// <summary>Gets or sets the default fine tuning job create URI.</summary>
        /// <value>The default fine tune create URI.</value>
        public string FineTuningJobCreateUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuningJobCreateUri;

        /// <summary>Gets or sets the default fine tuning job list URI.</summary>
        /// <value>The default fine tune list URI.</value>
        public string FineTuningJobListUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuningJobListUri;

        /// <summary>Gets or sets the fine tuning job get URI.</summary>
        /// <value>The default fine tune get URI.</value>
        public string FineTuningJobGetUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuningJobGetUri;

        /// <summary>Gets or sets the fine tuning job cancel URI.</summary>
        /// <value>The default fine tune cancel URI.</value>
        public string FineTuningJobCancelUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuningJobCancelUri;

        /// <summary>Gets or sets the fine tuning job events URI.</summary>
        /// <value>The default fine tune events URI.</value>
        public string FineTuningJobEventsUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuningJobEventsUri;

        /// <summary>Gets or sets the fine tuning job streamed events URI.</summary>
        /// <value>The fine tune streamed events URI.</value>
        public string FineTuningJobStreamedEventsUri { get; set; } = OpenAIDefaultOptions.DefaultFineTuningJobStreamedEventsUri;



        /// <summary>Gets or sets the audio speech URI.</summary>
        /// <value>The audio speech URI.</value>
        public string AudioSpeechUri { get; set; } = OpenAIDefaultOptions.DefaultAudioSpeechUri;

        /// <summary>Gets or sets the audio transcript URI.</summary>
        /// <value>The audio transcript URI.</value>
        public string AudioTranscriptUri { get; set; } = OpenAIDefaultOptions.DefaultAudioTranscriptUri;

        /// <summary>Gets or sets the audio translation URI.</summary>
        /// <value>The audio translation URI.</value>
        public string AudioTranslationUri { get; set; } = OpenAIDefaultOptions.DefaultAudioTranslationUri;

        /// <summary>Gets or sets the chat completions URI.</summary>
        /// <value>The chat completions URI.</value>
        public string ChatCompletionsUri { get; set; } = OpenAIDefaultOptions.DefaultChatCompletionsUri;



        /// <summary>Gets or sets the assistant create URI.</summary>
        /// <value>The assistant create URI.</value>
        public string AssistantCreateUri { get; set; } = OpenAIDefaultOptions.DefaultAssistantCreateUri;

        /// <summary>Gets or sets the assistant get URI.</summary>
        /// <value>The assistant get URI.</value>
        public string AssistantGetUri { get; set; } = OpenAIDefaultOptions.DefaultAssistantGetUri;

        /// <summary>Gets or sets the assistant list URI.</summary>
        /// <value>The assistant list URI.</value>
        public string AssistantListUri { get; set; } = OpenAIDefaultOptions.DefaultAssistantListUri;

        /// <summary>Gets or sets the assistant modify URI.</summary>
        /// <value>The assistant modify URI.</value>
        public string AssistantModifyUri { get; set; } = OpenAIDefaultOptions.DefaultAssistantModifyUri;

        /// <summary>Gets or sets the assistant delete URI.</summary>
        /// <value>The default delete URI.</value>
        public string AssistantDeleteUri { get; set; } = OpenAIDefaultOptions.DefaultAssistantDeleteUri;

        /// <summary>Gets or sets the assistant file create URI.</summary>
        /// <value>The assistant file create URI.</value>
        public string AssistantFileCreateUri { get; set; } = OpenAIDefaultOptions.DefaultAssistantFileCreateUri;

        /// <summary>Gets or sets the assistant file get URI.</summary>
        /// <value>The assistant file get URI.</value>
        public string AssistantFileGetUri { get; set; } = OpenAIDefaultOptions.DefaultAssistantFileGetUri;

        /// <summary>Gets or sets the assistant file list URI.</summary>
        /// <value>The assistant file list URI.</value>
        public string AssistantFileListUri { get; set; } = OpenAIDefaultOptions.DefaultAssistantFileListUri;

        /// <summary>Gets or sets the assistant file delete URI.</summary>
        /// <value>The assistant file delete URI.</value>
        public string AssistantFileDeleteUri { get; set; } = OpenAIDefaultOptions.DefaultAssistantFileDeleteUri;



        /// <summary>Gets or sets the thread create URI.</summary>
        /// <value>The thread create URI.</value>
        public string ThreadCreateUri { get; set; } = OpenAIDefaultOptions.DefaultThreadCreateUri;

        /// <summary>Gets or sets the thread get URI.</summary>
        /// <value>The thread get URI.</value>
        public string ThreadGetUri { get; set; } = OpenAIDefaultOptions.DefaultThreadGetUri;

        /// <summary>Gets or sets the thread modify URI.</summary>
        /// <value>The thread modify URI.</value>
        public string ThreadModifyUri { get; set; } = OpenAIDefaultOptions.DefaultThreadModifyUri;

        /// <summary>Gets or sets the thread delete URI.</summary>
        /// <value>The default delete URI.</value>
        public string ThreadDeleteUri { get; set; } = OpenAIDefaultOptions.DefaultThreadDeleteUri;



        /// <summary>Gets or sets the message create URI.</summary>
        /// <value>The message create URI.</value>
        public string MessageCreateUri { get; set; } = OpenAIDefaultOptions.DefaultMessageCreateUri;

        /// <summary>Gets or sets the message list URI.</summary>
        /// <value>The message list URI.</value>
        public string MessageListUri { get; set; } = OpenAIDefaultOptions.DefaultMessageListUri;

        /// <summary>Gets or sets the message get URI.</summary>
        /// <value>The message get URI.</value>
        public string MessageGetUri { get; set; } = OpenAIDefaultOptions.DefaultMessageGetUri;

        /// <summary>Gets or sets the message modify URI.</summary>
        /// <value>The message modify URI.</value>
        public string MessageModifyUri { get; set; } = OpenAIDefaultOptions.DefaultMessageModifyUri;

        /// <summary>Gets or sets the message file get URI.</summary>
        /// <value>The message file get URI.</value>
        public string MessageFileGetUri { get; set; } = OpenAIDefaultOptions.DefaultMessageFileGetUri;

        /// <summary>Gets or sets the message file list URI.</summary>
        /// <value>The message file list URI.</value>
        public string MessageFileListUri { get; set; } = OpenAIDefaultOptions.DefaultMessageFileListUri;


        /// <summary>Gets or sets the run create URI.</summary>
        /// <value>The run create URI.</value>
        public string RunCreateUri { get; set; } = OpenAIDefaultOptions.DefaultRunCreateUri;

        /// <summary>Gets or sets the thread and run create URI.</summary>
        /// <value>The thread and run create URI.</value>
        public string RunThreadAndRunCreateUri { get; set; } = OpenAIDefaultOptions.DefaultRunThreadAndRunCreateUri;

        /// <summary>Gets or sets the run get URI.</summary>
        /// <value>The run get URI.</value>
        public string RunGetUri { get; set; } = OpenAIDefaultOptions.DefaultRunGetUri;

        /// <summary>Gets or sets the run list URI.</summary>
        /// <value>The run list URI.</value>
        public string RunListUri { get; set; } = OpenAIDefaultOptions.DefaultRunListUri;

        /// <summary>Gets or sets the run modify URI.</summary>
        /// <value>The run modify URI.</value>
        public string RunModifyUri { get; set; } = OpenAIDefaultOptions.DefaultRunModifyUri;

        /// <summary>Gets or sets the run tool outputs to run URI.</summary>
        /// <value>The run modify URI.</value>
        public string RunSubmitToolOutputsToRunUri { get; set; } = OpenAIDefaultOptions.DefaultRunSubmitToolOutputsToRunUri;

        /// <summary>Gets or sets the run cancel URI.</summary>
        /// <value>The run cancel URI.</value>
        public string RunCancelUri { get; set; } = OpenAIDefaultOptions.DefaultRunCancelUri;

        /// <summary>Gets or sets the run steps get URI.</summary>
        /// <value>The run steps get URI.</value>
        public string RunStepsGetUri { get; set; } = OpenAIDefaultOptions.DefaultRunStepsGetUri;

        /// <summary>Gets or sets the run steps list URI.</summary>
        /// <value>The run steps list URI.</value>
        public string RunStepsListUri { get; set; } = OpenAIDefaultOptions.DefaultRunStepsListUri;



        /// <summary>Gets or sets a value indicating whether [log requests and responses].</summary>
        /// <value>
        ///   <c>true</c> if [log requests and responses]; otherwise, <c>false</c>.</value>
        public bool LogRequestsAndResponses { get; set; } = OpenAIDefaultOptions.DefaultLogRequestsAndResponses;

        /// <summary>Gets or sets the log requests and responses folder.</summary>
        /// <value>The log requests and responses folder.</value>
        public string LogRequestsAndResponsesFolder { get; set; } = OpenAIDefaultOptions.DefaultLogRequestsAndResponsesFolder;

        /// <summary>Gets or sets the default HTTP client timeout in milliseconds.</summary>
        /// <value>The default HTTP client timeout in milliseconds.</value>
        public int? HttpClientTimeoutInMilliseconds { get; set; } = OpenAIDefaultOptions.DefaultHttpClientTimeoutInMilliseconds;

        /// <summary>Gets or sets the json serializer options.</summary>
        /// <value>The json serializer options.</value>
        [JsonIgnore]
        public JsonSerializerOptions JsonSerializerOptions { get; set; } = OpenAIDefaultOptions.DefaultJsonSerializerOptions;

        /// <summary>Gets or sets the HTTP message handler for the HttpClient.</summary>
        /// <value>The HTTP message handler.</value>
        [JsonIgnore]
        public Func<HttpMessageHandler> HttpMessageHandlerFactory { get; set; }

        /// <summary>Gets or sets the authentication data.
        /// Here your can optionally set the OpenAI apiKey and the Organization information.</summary>
        /// <value>The authentication information.</value>
        public AuthenticationInfo AuthenticationInfo { get; set; } = AuthenticationInfo.Default;

        /// <summary>Creates a new object that is a copy of the current instance.</summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public virtual object Clone()
        {
            OpenAIOptions cloned = (OpenAIOptions)GetType().GetConstructor(Type.EmptyTypes).Invoke(null);

            cloned.AIServiceProviderType = AIServiceProviderType;
            cloned.OpenAIApiVersion = OpenAIApiVersion;
            cloned.BaseAddress = BaseAddress;
            cloned.AzureApiVersion = AzureApiVersion;
            cloned.AzureDeploymentId = AzureDeploymentId;
            cloned.AzureResourceName = AzureResourceName;
            cloned.AuthenticationInfo = AuthenticationInfo;
            cloned.EmbeddingsUri = EmbeddingsUri;
            cloned.FileDataUri = FileDataUri;
            cloned.FileDeleteUri = FileDeleteUri;
            cloned.FileDownloadUri = FileDownloadUri;
            cloned.FileListUri = FileListUri;
            cloned.FileUploadUri = FileUploadUri;
            cloned.FineTuneCancelUri = FineTuneCancelUri;
            cloned.FineTuneCreateUri = FineTuneCreateUri;
            cloned.FineTuneDeleteModelUri = FineTuneDeleteModelUri;
            cloned.FineTuneEventsUri = FineTuneEventsUri;
            cloned.FineTuneGetUri = FineTuneGetUri;
            cloned.FineTuneListUri = FineTuneListUri;
            cloned.FineTuneStreamedEventsUri = FineTuneGetUri;
            cloned.HttpMessageHandlerFactory = HttpMessageHandlerFactory;
            cloned.ImageCreateUri = ImageCreateUri;
            cloned.ImageEditUri = ImageEditUri;
            cloned.ImageVariationUri = ImageVariationUri;
            cloned.JsonSerializerOptions = JsonSerializerOptions;
            cloned.LogRequestsAndResponses = LogRequestsAndResponses;
            cloned.LogRequestsAndResponsesFolder = LogRequestsAndResponsesFolder;
            cloned.ModelsUri = ModelsUri;
            cloned.ModerationUri = ModerationUri;
            cloned.TextCompletionsUri = TextCompletionsUri;
            cloned.TextEditUri = TextEditUri;
            cloned.ChatCompletionsUri = ChatCompletionsUri;

            return cloned;
        }

    }

}
