using Forge.OpenAI.Models.VectorStores;
using Forge.OpenAI.Models.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>
    /// Vectore Store Service interface
    /// </summary>
    public interface IVectorStoreService
    {

        /// <summary>Creates a vectore store asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   CreateVectorStoreResponse
        /// </returns>
        Task<HttpOperationResult<CreateVectorStoreResponse>> CreateAsync(CreateVectorStoreRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a list of vector stores.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<HttpOperationResult<VectorStoreListResponse>> GetAsync(VectorStoreListRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a vector store.
        /// </summary>
        /// <param name="vectorStoreId">The vector store identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>VectoreStoreResponse</returns>
        Task<HttpOperationResult<VectoreStoreResponse>> GetAsync(string vectorStoreId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Modifies a vector store.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>VectoreStoreResponse</returns>
        Task<HttpOperationResult<VectoreStoreResponse>> ModifyAsync(ModifyVectorStoreRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a vector store.
        /// </summary>
        /// <param name="vectorStoreId">The vector store identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>DeleteVectorStoreResponse</returns>
        Task<HttpOperationResult<DeleteVectorStoreResponse>> DeleteAsync(string vectorStoreId, CancellationToken cancellationToken = default);

    }

}
