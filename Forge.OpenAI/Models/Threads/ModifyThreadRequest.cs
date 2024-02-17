using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Forge.OpenAI.Models.Common;

namespace Forge.OpenAI.Models.Threads
{

    /// <summary>Modifies a thread.</summary>
    public class ModifyThreadRequest : RequestBase
    {

        /// <summary>The ID of the thread to modify.</summary>
        /// <value>The thread identifier.</value>
        [JsonIgnore]
        [Required]
        public string ThreadId { get; set; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        [JsonPropertyName("metadata")]
        public Dictionary<string, string> Metadata { get; set; }

    }

}
