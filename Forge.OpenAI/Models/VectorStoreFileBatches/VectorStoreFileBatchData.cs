using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Forge.OpenAI.Interfaces.Models;
using Forge.OpenAI.Models.VectorStores;

namespace Forge.OpenAI.Models.VectorStoreFileBatches
{

    /// <summary>
    /// Vector Store File Batch Data
    /// </summary>
    public class VectorStoreFileBatchData : IVectorStoreFileBatchData
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
        /// The status of the vector store files batch, which can be either in_progress, completed, cancelled or failed.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// https://platform.openai.com/docs/api-reference/vector-stores-file-batches/batch-object#vector-stores-file-batches/batch-object-file_counts
        /// </summary>
        [JsonPropertyName("file_counts")]
        public FileCount FileCounts { get; set; }

    }

}
