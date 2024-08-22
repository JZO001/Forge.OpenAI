using Forge.OpenAI.Models.Common;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.ChatCompletions
{

    public abstract class ChatCompletionResponseBase : ResponseBase
    {

        /// <summary>
        /// The identifier of the result, which may be used during troubleshooting
        /// </summary>
        [JsonPropertyName("id")] 
        public string Id { get; set; }

        /// <summary>Gets the token usage numbers.</summary>
        /// <value>The usage.</value>
        [JsonPropertyName("usage")]
        public Usage Usage { get; set; }

        /// <summary>
        /// This fingerprint represents the backend configuration that the model runs with. Can be used in conjunction with the seed request parameter to understand when backend changes have been made that might impact determinism.
        /// </summary>
        /// <value>The system fingerprint.</value>
        [JsonPropertyName("system_fingerprint")]
        public string SystemFingerprint { get; set; }

        /// <summary>
        /// The service tier used for processing the request. This field is only included if the service_tier parameter is specified in the request.
        /// https://platform.openai.com/docs/api-reference/chat/object#chat/object-service_tier
        /// </summary>
        /// <value>The service tier.</value>
        [JsonPropertyName("service_tier")]
        public string ServiceTier { get; set; }

    }

}
