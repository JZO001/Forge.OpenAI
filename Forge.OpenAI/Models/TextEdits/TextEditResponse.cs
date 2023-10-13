using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.TextCompletions;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.TextEdits
{

    /// <summary>Represents a text edit response</summary>
    public class TextEditResponse : ResponseBase
    {

        /// <summary>Initializes a new instance of the <see cref="TextEditResponse" /> class.</summary>
        public TextEditResponse()
        {
        }

        /// <summary>Gets the choices based on the requested text edit number in the request</summary>
        /// <value>The choices.</value>
        [JsonPropertyName("choices")]
        public List<Choice> Choices { get; set; }

        /// <summary>Gets the token usage numbers.</summary>
        /// <value>The usage.</value>
        [JsonPropertyName("usage")]
        public Usage Usage { get; set; }

    }

}
