using Forge.OpenAI.Models.VectorStoreFileBatches;
using Forge.OpenAI.Models.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>
    /// Vector Store File Batches service
    /// </summary>
    public interface IVectorStoreFileBatchService
    {

        /// <summary>
        /// Create vector store batch file
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>CreateVectorStoreFileBatchResponse</returns>
        Task<HttpOperationResult<CreateVectorStoreFileBatchResponse>> CreateAsync(CreateVectorStoreFileBatchRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// List vector store batch files
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>VectorStoreFileBatchFileListResponse</returns>
        Task<HttpOperationResult<VectorStoreFileBatchFileListResponse>> GetAsync(VectorStoreFileBatchFileListRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieve vector store batch file
        /// </summary>
        /// <param name="vectorStoreId">The vector store identifier.</param>
        /// <param name="batchId">The batch identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>VectorStoreFileBatchResponse</returns>
        Task<HttpOperationResult<VectorStoreFileBatchResponse>> GetAsync(string vectorStoreId, string batchId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Cancel vector store batch file
        /// </summary>
        /// <param name="vectorStoreId">The vector store identifier.</param>
        /// <param name="batchId">The batch identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>DeleteVectorStoreFileBatchResponse</returns>
        Task<HttpOperationResult<DeleteVectorStoreFileBatchResponse>> CancelAsync(string vectorStoreId, string batchId, CancellationToken cancellationToken = default);

    }

}
