using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.ChatCompletions
{

    public class StreamOptions
    {

        /// <summary>
        /// Get token usage data for streamed requests
        /// </summary>
        /// <value>True, if retrieving token usage for stream response</value>
        [JsonPropertyName("include_usage")]
        public bool? IncludeUsage { get; set; } = true;

    }

}
