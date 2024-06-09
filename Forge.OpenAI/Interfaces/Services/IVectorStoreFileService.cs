using Forge.OpenAI.Models.VectorStoreFiles;
using Forge.OpenAI.Models.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>
    /// Vector store file service
    /// </summary>
    public interface IVectorStoreFileService
    {

        /// <summary>
        /// Create vector store file
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>CreateVectorStoreFilesResponse</returns>
        Task<HttpOperationResult<CreateVectorStoreFilesResponse>> CreateAsync(CreateVectorStoreFilesRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// List vector store files
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>VectorStoreFilesListResponse</returns>
        Task<HttpOperationResult<VectorStoreFilesListResponse>> GetAsync(VectorStoreFilesListRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve vector store file
        /// </summary>
        /// <param name="vectorStoreFileId">The vector store file identifier.</param>
        /// <param name="fileId">The ID of the file being retrieved.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>VectorStoreFileResponse</returns>
        Task<HttpOperationResult<VectorStoreFileResponse>> GetAsync(string vectorStoreFileId, string fileId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete vector store file
        /// </summary>
        /// <param name="vectorStoreFileId">The vector store file identifier.</param>
        /// <param name="fileId">The ID of the file being retrieved.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>DeleteVectorStoreFileResponse</returns>
        Task<HttpOperationResult<DeleteVectorStoreFileResponse>> DeleteAsync(string vectorStoreFileId, string fileId, CancellationToken cancellationToken = default);

    }

}
