using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Batch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>
    /// Batch service
    /// </summary>
    public interface IBatchService
    {

        /// <summary>Creates and executes a batch from an uploaded file of requests</summary>
        /// <param name="request">The fine tune job checkpoint request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   CreateBatchResponse
        /// </returns>
        Task<HttpOperationResult<CreateBatchResponse>> CreateAsync(CreateBatchRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the list of batches asynchronously.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>ListBatchesResponse</returns>
        Task<HttpOperationResult<ListBatchesResponse>> GetAsync(ListBatchesRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the batch by id asynchronously.
        /// </summary>
        /// <param name="batchId">The batch identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>BatchResponse</returns>
        Task<HttpOperationResult<BatchResponse>> GetAsync(string batchId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Cancels a job the asynchronously.
        /// </summary>
        /// <param name="batchId">The batch identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>CancelBatchResponse</returns>
        Task<HttpOperationResult<CancelBatchResponse>> CancelAsync(string batchId, CancellationToken cancellationToken = default);

    }

}
