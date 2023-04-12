using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.ChatCompletions
{

    public class ChatChoiceStreamed
    {

        [JsonPropertyName("delta")]
        public Delta Delta { get; set; }

        [JsonPropertyName("index")]
        public int? Index { get; set; }

        [JsonPropertyName("finish_reason")]
        public object FinishReason { get; set; }

    }

}
