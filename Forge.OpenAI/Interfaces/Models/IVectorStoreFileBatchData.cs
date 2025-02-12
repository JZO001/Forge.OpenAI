using Forge.OpenAI.Models.VectorStores;
using System;

namespace Forge.OpenAI.Interfaces.Models
{

    public interface IVectorStoreFileBatchData
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
        /// The ID of the vector store that the File is attached to.
        /// </summary>
        string VectorStoreId { get; }

        /// <summary>
        /// The status of the vector store files batch, which can be either in_progress, completed, cancelled or failed.
        /// </summary>
        string Status { get; }

        /// <summary>
        /// https://platform.openai.com/docs/api-reference/vector-stores-file-batches/batch-object#vector-stores-file-batches/batch-object-file_counts
        /// </summary>
        FileCount FileCounts { get; }

    }

}
