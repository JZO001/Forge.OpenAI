using Forge.OpenAI.Models.Common;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Forge.OpenAI.Models.Audio.Transcription
{

    /// <summary>Represents a transcription response</summary>
    public class TranscriptionResponse : ResponseBase
    {

        /// <summary>
        /// The language of the input audio.
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; }

        /// <summary>
        /// The duration of the input audio.
        /// </summary>
        [JsonPropertyName("duration")]
        public double? Duration { get; set; }

        /// <summary>Gets the text.</summary>
        /// <value>The text.</value>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// Extracted words and their corresponding timestamps.
        /// </summary>
        [JsonPropertyName("words")]
        public IReadOnlyList<TranscriptionWord> Words { get; set; }

        /// <summary>
        /// Segments of the transcribed text and their corresponding details.
        /// </summary>
        [JsonPropertyName("segments")]
        public IReadOnlyList<TranscriptionSegment> Segments { get; set; }

    }

}
