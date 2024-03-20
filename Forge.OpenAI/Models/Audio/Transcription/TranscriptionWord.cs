using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Audio
{

    /// <summary>
    /// Extracted word and their corresponding timestamps.
    /// </summary>
    public class TranscriptionWord
    {

        /// <summary>
        /// The text content of the word.
        /// </summary>
        [JsonPropertyName("word")]
        public string Word { get; set; }

        /// <summary>
        /// Start time of the word in seconds.
        /// </summary>
        [JsonPropertyName("start")]
        public double Start { get; set; }

        /// <summary>
        /// End time of the word in seconds.
        /// </summary>
        [JsonPropertyName("end")]
        public double End { get; set; }

    }

}
