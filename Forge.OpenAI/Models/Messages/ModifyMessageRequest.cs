using Forge.OpenAI.Models.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Messages
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/messages/modifyMessage
    /// </summary>
    public class ModifyMessageRequest : RequestBase
    {

        /// <summary>The ID of the thread to modify a message for.</summary>
        /// <value>The thread identifier.</value>
        [JsonIgnore]
        [Required]
        public string ThreadId { get; set; }

        /// <summary>The ID of the thread to modify a message for.</summary>
        /// <value>The thread identifier.</value>
        [JsonIgnore]
        [Required]
        public string MessageId { get; set; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; set; }

    }

}
