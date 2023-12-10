using System;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.TextCompletions
{

    /// <summary>
    /// Represents a completion choice
    /// </summary>
    [Obsolete]
    public class Choice
    {

        /// <summary>Initializes a new instance of the <see cref="Choice" /> class.</summary>
        public Choice()
        {
        }

        /// <summary>
        /// The main text of the completion
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// If multiple completion choices we returned, this is the index withing the various choices
        /// </summary>
        [JsonPropertyName("index")]
        public int Index { get; set; }

        /// <summary>
        /// If the request specified LogProbabilities, this contains the list of the most likely tokens.
        /// </summary>
        [JsonPropertyName("logprobs")]
        public LogProbabilities LogProbabilities { get; set; }

        /// <summary>
        /// If this is the last segment of the completion result, this specifies why the completion has ended.
        /// </summary>
        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }

        /// <summary>
        /// Gets the main text of this completion
        /// </summary>
        public override string ToString() => Text;

        /// <summary>Performs an implicit conversion from <see cref="Choice" /> to <see cref="string" />.</summary>
        /// <param name="choice">The choice.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(Choice choice) => choice?.Text;

    }

}
