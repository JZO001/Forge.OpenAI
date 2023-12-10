using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.ChatCompletions
{

    public class ChatToolChoiceFunction
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

    }

}
