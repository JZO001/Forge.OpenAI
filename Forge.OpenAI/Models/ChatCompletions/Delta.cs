using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.ChatCompletions
{

    public class Delta
    {

        /// <summary>
        /// Chat message content
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }

    }

}
