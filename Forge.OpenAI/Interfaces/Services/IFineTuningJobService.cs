using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.FineTuningJob;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>Represents the fine tuning job service</summary>
    public interface IFineTuningJobService
    {

        /// <summary>Creates a fine tune job asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuningJobResponse
        /// </returns>
        Task<HttpOperationResult<FineTuningJobResponse>> CreateAsync(FineTuningJobCreateRequest request, CancellationToken cancellationToken = default);

        /// <summary>Gets the list of fine tune jobs asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuningJobListResponse
        /// </returns>
        Task<HttpOperationResult<FineTuningJobListResponse>> GetAsync(FineTuningJobListRequest request = null, CancellationToken cancellationToken = default);

        /// <summary>Gets a fine tune job data asynchronously.</summary>
        /// <param name="fineTuneJobId">The fine tune identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuningJobResponse
        /// </returns>
        Task<HttpOperationResult<FineTuningJobResponse>> GetAsync(string fineTuneJobId, CancellationToken cancellationToken = default);

        /// <summary>Cancels a fine tune job asynchronously.</summary>
        /// <param name="fineTuneJobId">The fine tune identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuningJobResponse
        /// </returns>
        Task<HttpOperationResult<FineTuningJobResponse>> CancelAsync(string fineTuneJobId, CancellationToken cancellationToken = default);

        /// <summary>Gets the events of a fine tune job asynchronously.</summary>
        /// <param name="fineTuneJobId">The fine tune identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuningJobEvent
        /// </returns>
        Task<HttpOperationResult<FineTuningJobEvent>> GetEventsAsync(string fineTuneJobId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the events as stream asynchronous.
        /// WARNING: method will block until cancelling with CancellationToken,
        /// or the Fine Tune Job cancel called on the API.
        /// </summary>
        /// <param name="fineTuningJobId">The fine tune identifier.</param>
        /// <param name="resultCallback">The result callback.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   HttpOperationResult
        /// </returns>
        Task<HttpOperationResult> GetEventsAsStreamAsync(string fineTuningJobId, Action<HttpOperationResult<IAsyncEventInfo<FineTuningJobEvent>>> resultCallback, CancellationToken cancellationToken = default);

#if NETCOREAPP3_1_OR_GREATER
        /// <summary>
        /// Gets the events as stream asynchronous.
        /// WARNING: method will block until cancelling with CancellationToken,
        /// or the Fine Tuning Job cancel called on the API.
        /// </summary>
        /// <param name="fineTuningJobId">The fine tuning job identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuningJobEvent
        /// </returns>
        IAsyncEnumerable<HttpOperationResult<IAsyncEventInfo<FineTuningJobEvent>>> GetEventsAsStreamAsync(string fineTuningJobId, CancellationToken cancellationToken = default);
#endif

        /// <summary>Gets the checkpoints of a fine tune job asynchronously.</summary>
        /// <param name="request">The fine tune job checkpoint request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuningJobCheckpointListResponse
        /// </returns>
        Task<HttpOperationResult<FineTuningJobCheckpointListResponse>> GetCheckPointsAsync(FineTuningJobCheckpointListRequest request, CancellationToken cancellationToken = default);

    }

}
