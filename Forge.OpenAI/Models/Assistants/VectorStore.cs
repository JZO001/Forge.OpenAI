using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Assistants
{

    /// <summary>A helper to create a vector store with file_ids and attach it to this assistant. There can be a maximum of 1 vector store attached to the assistant.</summary>
    public class VectorStore
    {

        /// <summary>A list of file IDs to add to the vector store. There can be a maximum of 10000 files in a vector store.</summary>
        /// <value>The file ids.</value>
        [JsonPropertyName("file_ids")]
        public IReadOnlyList<string> FileIds { get; set; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        [JsonPropertyName("metadata")]
        public IReadOnlyDictionary<string, string> Metadata { get; set; }

    }

}
