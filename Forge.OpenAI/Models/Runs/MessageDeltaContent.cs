using System.Text.Json.Serialization;
using Forge.OpenAI.Models.Messages;

namespace Forge.OpenAI.Models.Runs
{

    /// <summary>
    ///   <a href="https://platform.openai.com/docs/api-reference/assistants-streaming/message-delta-object#assistants-streaming/message-delta-object-delta">https://platform.openai.com/docs/api-reference/assistants-streaming/message-delta-object#assistants-streaming/message-delta-object-delta</a>
    /// </summary>
    public class MessageDeltaContent : Content
    {

        /// <summary>See the constants above</summary>
        /// <value>The type.</value>
        [JsonPropertyName("index")]
        public int Index { get; set; }

    }

}
