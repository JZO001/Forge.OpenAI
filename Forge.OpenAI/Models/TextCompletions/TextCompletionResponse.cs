using System.Collections.Generic;
using System.Text.Json.Serialization;
using Forge.OpenAI.Models.Common;

namespace Forge.OpenAI.Models.TextCompletions
{

    /// <summary>
    /// Represents a text completion response
    /// </summary>
    public class TextCompletionResponse : ResponseBase
    {

        /// <summary>
        /// The identifier of the result, which may be used during troubleshooting
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The completions returned by the API.  Depending on your request, there may be 1 or many choices.
        /// </summary>
        [JsonPropertyName("choices")]
        public List<Choice> Completions { get; set; }

    }

}
