using Forge.OpenAI.Models.Common;
using System.Text.Json.Serialization;
using Forge.OpenAI.Models.VectorStores;
using System;
using System.Collections.Generic;
using Forge.OpenAI.Interfaces.Models;

namespace Forge.OpenAI.Models.VectorStoreFileBatches
{

    /// <summary>
    /// Vector Store File Batch Data as a response
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Models.Common.ResponseBase" />
    public abstract class VectorStoreFileBatchResponseBase : ResponseBase, IVectorStoreFileBatchData
    {

        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

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
