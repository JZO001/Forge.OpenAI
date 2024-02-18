using System.Collections.Generic;
using System.Text.Json.Serialization;
using Forge.OpenAI.Models.Common;

namespace Forge.OpenAI.Models.Threads
{

    /// <summary>Create a thread</summary>
    public class CreateThreadRequest : RequestBase
    {

        /// <summary>
        /// A list of messages to start the thread with.
        /// </summary>
        [JsonPropertyName("messages")]
        public IList<Message> Messages { get; set; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        [JsonPropertyName("metadata")]
        public IDictionary<string, string> Metadata { get; set; }

    }

}
