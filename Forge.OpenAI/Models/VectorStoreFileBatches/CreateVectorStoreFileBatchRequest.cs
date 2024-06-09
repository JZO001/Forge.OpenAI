using System.Collections.Generic;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.VectorStores;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.VectorStoreFileBatches
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/vector-stores-file-batches/createBatch
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Models.Common.RequestBase" />
    public class CreateVectorStoreFileBatchRequest : RequestBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVectorStoreFileBatchRequest"/> class.
        /// </summary>
        public CreateVectorStoreFileBatchRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVectorStoreFileBatchRequest"/> class.
        /// </summary>
        /// <param name="vectorStoreId">The vector store identifier.</param>
        public CreateVectorStoreFileBatchRequest(string vectorStoreId)
        {
            VectorStoreId = vectorStoreId;
        }

        /// <summary>
        /// The ID of the vector store for which to create a File Batch.
        /// </summary>
        [JsonIgnore]
        public string VectorStoreId { get; set; }

        /// <summary>
        /// A list of File IDs that the vector store should use. Useful for tools like file_search that can access files.
        /// </summary>
        [JsonPropertyName("file_ids")]
        public IList<string> FileIds { get; set; }

        /// <summary>
        /// The chunking strategy used to chunk the file(s). If not set, will use the auto strategy. Only applicable if file_ids is non-empty.
        /// </summary>
        [JsonPropertyName("chunking_strategy")]
        public ChunkingStrategy ChunkingStrategy { get; set; }

    }

}
