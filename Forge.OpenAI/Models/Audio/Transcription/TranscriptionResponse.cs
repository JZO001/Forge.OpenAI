using Forge.OpenAI.Models.Common;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Audio.Transcription
{

    /// <summary>Represents a transcription response</summary>
    public class TranscriptionResponse : ResponseBase
    {

        /// <summary>Gets the text.</summary>
        /// <value>The text.</value>
        [JsonPropertyName("text")]
        public string Text { get; set; }

    }

}
