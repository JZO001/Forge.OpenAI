using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.TextCompletions
{

    /// <summary>Belongs to the Choice</summary>
    [Obsolete]
    public class LogProbabilities
    {

        /// <summary>Initializes a new instance of the <see cref="LogProbabilities" /> class.</summary>
        public LogProbabilities()
        {
        }

        /// <summary>Gets the tokens.</summary>
        /// <value>The tokens.</value>
        [JsonPropertyName("tokens")]
        public IReadOnlyList<string> Tokens { get; set; }

        /// <summary>Gets the token log probabilities.</summary>
        /// <value>The token log probabilities.</value>
        [JsonPropertyName("token_logprobs")]
        public IReadOnlyList<double> TokenLogProbabilities { get; set; }

        /// <summary>Gets the top log probabilities.</summary>
        /// <value>The top log probabilities.</value>
        [JsonPropertyName("top_logprobs")]
        public IReadOnlyList<IDictionary<string, double>> TopLogProbabilities { get; set; }

        /// <summary>Gets the text offsets.</summary>
        /// <value>The text offsets.</value>
        [JsonPropertyName("text_offset")]
        public IReadOnlyList<int> TextOffsets { get; set; }

    }

}
