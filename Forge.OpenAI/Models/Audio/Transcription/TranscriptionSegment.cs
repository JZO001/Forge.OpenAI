using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Audio
{

    /// <summary>
    /// Segment of the transcribed text and their corresponding details.
    /// </summary>
    public sealed class TranscriptionSegment
    {

        /// <summary>
        /// Unique identifier of the segment.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Seek offset of the segment.
        /// </summary>
        [JsonPropertyName("seek")]
        public int Seek { get; set; }

        /// <summary>
        /// Start time of the segment in seconds.
        /// </summary>
        [JsonPropertyName("start")]
        public double Start { get; set; }

        /// <summary>
        /// End time of the segment in seconds.
        /// </summary>
        [JsonPropertyName("end")]
        public double End { get; set; }

        /// <summary>
        /// Text content of the segment.
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// Array of token IDs for the text content.
        /// </summary>
        [JsonPropertyName("tokens")]
        public int[] Tokens { get; set; }

        /// <summary>
        /// Temperature parameter used for generating the segment.
        /// </summary>
        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        /// <summary>
        /// Average logprob of the segment.
        /// If the value is lower than -1, consider the logprobs failed.
        /// </summary>
        [JsonPropertyName("avg_logprob")]
        public double AverageLogProbability { get; set; }

        /// <summary>
        /// Compression ratio of the segment.
        /// If the value is greater than 2.4, consider the compression failed.
        /// </summary>
        [JsonPropertyName("compression_ratio")]
        public double CompressionRatio { get; set; }

        /// <summary>
        /// Probability of no speech in the segment.
        /// If the value is higher than 1.0 and the avg_logprob is below -1, consider this segment silent.
        /// </summary>
        [JsonPropertyName("no_speech_prob")]
        public double NoSpeechProbability { get; set; }

    }

}
