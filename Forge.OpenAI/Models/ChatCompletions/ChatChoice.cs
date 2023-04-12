using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.ChatCompletions
{

    public class ChatChoice
    {

        [JsonPropertyName("message")]
        public ChatMessage Message { get; set; }

        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }

        [JsonPropertyName("index")]
        public int? Index { get; set; }

    }

}
