using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.ChatCompletions
{

    /// <summary>
    ///   <a href="https://platform.openai.com/docs/api-reference/chat/create#chat-create-response_format">https://platform.openai.com/docs/api-reference/chat/create#chat-create-response_format</a>
    /// </summary>
    public class ChatResponseFormat
    {

        /// <summary>Gets or sets the type.</summary>
        /// <value>"json_object" or "text"</value>
        [JsonPropertyName("type")]
        public string Type { get; set; }

    }

}
