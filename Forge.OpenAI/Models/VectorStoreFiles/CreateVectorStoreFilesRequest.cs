using Forge.OpenAI.Models.Common;
using System.Text.Json.Serialization;
using Forge.OpenAI.Models.VectorStores;
using System.ComponentModel.DataAnnotations;

namespace Forge.OpenAI.Models.VectorStoreFiles
{

    /// <summary>
    /// Create a vector store files.
    /// https://platform.openai.com/docs/api-reference/vector-stores-files/createFile
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Models.Common.RequestBase" />
    public class CreateVectorStoreFilesRequest : RequestBase
    {

        /// <summary>
        /// The ID of the vector store for which to create a File.
        /// </summary>
        /// <value>The vector store identifier.</value>
        [Required]
        [JsonPropertyName("vector_store_id")]
        public string VectorStoreId { get; set; }

        /// <summary>
        /// A File ID that the vector store should use. Useful for tools like file_search that can access files.
        /// </summary>
        [Required]
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }

        /// <summary>
        /// The chunking strategy used to chunk the file(s). If not set, will use the auto strategy.
        /// </summary>
        [JsonPropertyName("chunking_strategy")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ChunkingStrategy ChunkingStrategy { get; set; }

    }

}
