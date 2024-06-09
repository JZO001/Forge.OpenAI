using Forge.OpenAI.Models.Common;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Forge.OpenAI.Models.VectorStores
{

    /// <summary>
    /// Modifies a vector store.
    /// https://platform.openai.com/docs/api-reference/vector-stores/modify
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Models.Common.RequestBase" />
    public class ModifyVectorStoreRequest : RequestBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifyVectorStoreRequest"/> class.
        /// </summary>
        public ModifyVectorStoreRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifyVectorStoreRequest"/> class.
        /// </summary>
        /// <param name="vectorStoreId">The vector store identifier.</param>
        public ModifyVectorStoreRequest(string vectorStoreId)
        {
            VectorStoreId = vectorStoreId;
        }

        /// <summary>
        /// The ID of the vector store to modify.
        /// </summary>
        [JsonIgnore]
        public string VectorStoreId { get; set; }

        /// <summary>
        /// The name of the vector store.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The expiration policy for a vector store.
        /// </summary>
        [JsonPropertyName("expires_after")]
        public ExpiresAfter ExpiresAfter { get; set; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        [JsonPropertyName("metadata")]
        public IDictionary<string, string> Metadata { get; set; }

    }

}
