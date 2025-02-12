using System;
using System.Text.Json.Serialization;
using Forge.OpenAI.Interfaces.Models;
using Forge.OpenAI.Models.VectorStores;

namespace Forge.OpenAI.Models.VectorStoreFiles
{

    /// <summary>
    /// A list of files attached to a vector store.
    /// </summary>
    public class VectorStoreFileData : IVectorStoreFileData
    {

        public const string STATUS_IN_PROGRESS = "in_progress";
        public const string STATUS_COMPLETED = "completed";
        public const string STATUS_CANCELLED = "cancelled";
        public const string STATUS_FAILED = "failed";

        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The object type, which is always assistant.
        /// </summary>
        [JsonPropertyName("object")]
        public string Object { get; set; }

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
