using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Messages
{

    /// <summary>The text content that is part of a message.</summary>
    public class TextContent
    {

        /// <summary>
        /// The data that makes up the text.
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <summary>
        /// Annotations
        /// </summary>
        [JsonPropertyName("annotations")]
        public IReadOnlyList<Annotation> Annotations { get; set; }

    }

}
