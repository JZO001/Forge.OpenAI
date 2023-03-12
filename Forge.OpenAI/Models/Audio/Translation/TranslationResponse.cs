using Forge.OpenAI.Models.Common;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Audio.Translation
{

    /// <summary>Represents a transcription response</summary>
    public class TranslationResponse : ResponseBase
    {

        /// <summary>Gets the text.</summary>
        /// <value>The text.</value>
        [JsonPropertyName("text")]
        public string Text { get; set; }

    }

}
