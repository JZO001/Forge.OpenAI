using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Runs
{

    /// <summary>Represents the message delta for thread.message.delta</summary>
    public class MessageDelta
    {

        /// <summary>Gets content of the message delta.</summary>
        /// <value>The content of the message delta.</value>
        [JsonPropertyName("content")]
        public IReadOnlyList<MessageDeltaContent> Contents { get; set; }

    }

}
