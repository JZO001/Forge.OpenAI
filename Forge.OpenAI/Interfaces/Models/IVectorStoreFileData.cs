using Forge.OpenAI.Models;
using Forge.OpenAI.Models.VectorStores;
using System;

namespace Forge.OpenAI.Interfaces.Models
{

    public interface IVectorStoreFileData
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
        /// The total vector store usage in bytes. Note that this may be different from the original file size.
        /// </summary>
        int UsageBytes { get; }

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
        /// The status of the vector store file, which can be either in_progress, completed, cancelled, or failed. The status completed indicates that the vector store file is ready for use.
        /// </summary>
        string Status { get; }

        /// <summary>
        /// The last error associated with this vector store file. Will be null if there are no errors.
        /// </summary>
        Error LastError { get; }

        /// <summary>
        /// The chunking strategy used to chunk the file(s).
        /// </summary>
        ChunkingStrategy ChunkingStrategy { get; }

    }

}
