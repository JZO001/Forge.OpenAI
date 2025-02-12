using Forge.OpenAI.Models.VectorStores;
using System;
using System.Collections.Generic;

namespace Forge.OpenAI.Interfaces.Models
{

    public interface IVectorStoreData
    {

        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The object type, which is always assistant.
        /// </summary>
        string Object { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the assistant was created.
        /// </summary>
        int CreatedAtUnixTime { get; }

        /// <summary>Gets the created at.</summary>
        /// <value>The created at.</value>
        DateTime CreatedAt { get; }

        /// <summary>
        /// The name of the assistant.
        /// The maximum length is 256 characters.
        /// </summary>
        string Name { get; }

        /// <summary>The total number of bytes used by the files in the vector store.</summary>
        /// <value>inteeger</value>
        int UsageBytes { get; }

        /// <summary>
        /// https://platform.openai.com/docs/api-reference/vector-stores/object#vector-stores/object-file_counts
        /// </summary>
        FileCount FileCounts { get; }

        /// <summary>The status of the vector store, which can be either expired, in_progress, or completed. A status of completed indicates that the vector store is ready for use.</summary>
        /// <value>The status.</value>
        string Status
        {
            get;
        }

        /// <summary>The expiration policy for a vector store.</summary>
        /// <value>The expires after.</value>
        ExpiresAfter ExpiresAfter
        {
            get;
        }

        /// <summary>The Unix timestamp (in seconds) for when the vector store will expire.</summary>
        /// <value>The expires at.</value>
        int? ExpiresAt
        {
            get;
        }

        /// <summary>The Unix timestamp (in seconds) for when the vector store was last active.</summary>
        /// <value>The last active at.</value>
        int? LastActiveAt
        {
            get;
        }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        IReadOnlyDictionary<string, string> Metadata
        {
            get;
        }

    }

}
