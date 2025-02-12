using Forge.OpenAI.Models.Common;
using System;
using System.Text.Json.Serialization;
using Forge.OpenAI.Models.VectorStores;
using Forge.OpenAI.Interfaces.Models;

namespace Forge.OpenAI.Models.VectorStoreFiles
{

    /// <summary>
    /// Response base with common properties for vector store files.
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Models.Common.ResponseBase" />
    public abstract class VectorStoreFileResponseBase : ResponseBase, IVectorStoreFileData
    {

        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The total vector store usage in bytes. Note that this may be different from the original file size.
        /// </summary>
        [JsonPropertyName("usage_bytes")]
        public int UsageBytes { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the assistant was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public int CreatedAtUnixTime { get; set; }

        /// <summary>Gets the created at.</summary>
        /// <value>The created at.</value>
        [JsonIgnore]
        public DateTime CreatedAt => DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTime).DateTime;

        /// <summary>
        /// The ID of the vector store that the File is attached to.
        /// </summary>
        [JsonPropertyName("vector_store_id")]
        public string VectorStoreId { get; set; }

        /// <summary>
        /// The status of the vector store file, which can be either in_progress, completed, cancelled, or failed. The status completed indicates that the vector store file is ready for use.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// The last error associated with this vector store file. Will be null if there are no errors.
        /// </summary>
        [JsonPropertyName("last_error")]
        public Error LastError { get; set; }

        /// <summary>
        /// The chunking strategy used to chunk the file(s).
        /// </summary>
        [JsonPropertyName("chunking_strategy")]
        public ChunkingStrategy ChunkingStrategy { get; set; }

    }

}
