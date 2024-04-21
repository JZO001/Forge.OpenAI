using Forge.OpenAI.Models.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Runs
{

    /// <summary>
    ///   <a href="https://platform.openai.com/docs/api-reference/assistants-streaming/message-delta-object">https://platform.openai.com/docs/api-reference/assistants-streaming/message-delta-object</a>
    /// </summary>
    public class MessageDeltaResponse : ResponseBase
    {

        public const string ROLE_ASSISTANT = "assistant";
        public const string ROLE_USER = "user";

        /// <summary>The entity that produced the message. One of user or assistant.</summary>
        /// <value>The role.</value>
        [JsonPropertyName("role")]
        public string Role { get; set; }

        /// <summary>
        /// The list of File IDs the assistant used for this run.
        /// </summary>
        [JsonPropertyName("content")]
        public IReadOnlyList<MessageDeltaContent> Contents { get; set; }

        /// <summary>
        /// The list of File IDs the assistant used for this run.
        /// </summary>
        [JsonPropertyName("file_ids")]
        public IReadOnlyList<string> FileIds { get; set; }

    }

}
