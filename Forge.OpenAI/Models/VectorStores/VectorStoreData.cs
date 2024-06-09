using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.VectorStores
{

    /// <summary>
    ///   <a href="https://platform.openai.com/docs/api-reference/vector-stores/object">https://platform.openai.com/docs/api-reference/vector-stores/object</a>
    /// </summary>
    public class VectorStoreData
    {

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
        /// The name of the assistant.
        /// The maximum length is 256 characters.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>The total number of bytes used by the files in the vector store.</summary>
        /// <value>inteeger</value>
        [JsonPropertyName("usage_bytes")]
        public int UsageBytes { get; set; }

        /// <summary>
        /// https://platform.openai.com/docs/api-reference/vector-stores/object#vector-stores/object-file_counts
        /// </summary>
        [JsonPropertyName("file_counts")]
        public FileCount FileCounts { get; set; }

        /// <summary>The status of the vector store, which can be either expired, in_progress, or completed. A status of completed indicates that the vector store is ready for use.</summary>
        /// <value>The status.</value>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>The expiration policy for a vector store.</summary>
        /// <value>The expires after.</value>
        [JsonPropertyName("expires_after")]
        public ExpiresAfter ExpiresAfter { get; set; }

        /// <summary>The Unix timestamp (in seconds) for when the vector store will expire.</summary>
        /// <value>The expires at.</value>
        [JsonPropertyName("expires_at")]
        public int? ExpiresAt { get; set; }

        /// <summary>The Unix timestamp (in seconds) for when the vector store was last active.</summary>
        /// <value>The last active at.</value>
        [JsonPropertyName("last_active_at")]
        public int? LastActiveAt { get; set; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        [JsonPropertyName("metadata")]
        public IReadOnlyDictionary<string, string> Metadata { get; set; }

    }

}
