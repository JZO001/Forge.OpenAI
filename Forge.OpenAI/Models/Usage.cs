using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models
{

    /// <summary>Represents the usage of the tokens</summary>
    public class Usage
    {

        /// <summary>Initializes a new instance of the <see cref="Usage" /> class.</summary>
        public Usage()
        {
        }

        /// <summary>Gets the used prompt tokens.</summary>
        /// <value>The prompt tokens.</value>
        [JsonPropertyName("prompt_tokens")]
        public int PromptTokens { get; set; }

        /// <summary>Gets the used completion tokens.</summary>
        /// <value>The completion tokens.</value>
        [JsonPropertyName("completion_tokens")]
        public int CompletionTokens { get; set; }

        /// <summary>Gets the total used tokens.</summary>
        /// <value>The total tokens.</value>
        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }

    }

}
