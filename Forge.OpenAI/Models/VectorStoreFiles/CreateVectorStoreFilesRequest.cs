using System.Collections.Generic;
using Forge.OpenAI.Models.Common;
using System.Text.Json.Serialization;
using Forge.OpenAI.Models.VectorStores;

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
        /// A File ID that the vector store should use. Useful for tools like file_search that can access files.
        /// </summary>
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }

        /// <summary>
        /// The chunking strategy used to chunk the file(s). If not set, will use the auto strategy.
        /// </summary>
        [JsonPropertyName("chunking_strategy")]
        public ChunkingStrategy ChunkingStrategy { get; set; }

    }

}
