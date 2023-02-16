using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.FineTunes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>Represents the fine tune service</summary>
    public interface IFineTuneService
    {

        /// <summary>Creates a fine tune job asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuneCreateResponse
        /// </returns>
        Task<HttpOperationResult<FineTuneCreateResponse>> CreateAsync(FineTuneCreateRequest request, CancellationToken cancellationToken);

        /// <summary>Gets the list of fine tune jobs asynchronously.</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuneListResponse
        /// </returns>
        Task<HttpOperationResult<FineTuneListResponse>> GetAsync(CancellationToken cancellationToken);

        /// <summary>Gets a fine tune job data asynchronously.</summary>
        /// <param name="fineTuneId">The fine tune identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuneJobDataResponse
        /// </returns>
        Task<HttpOperationResult<FineTuneJobDataResponse>> GetAsync(string fineTuneId, CancellationToken cancellationToken);

        /// <summary>Cancels a fine tune job asynchronously.</summary>
        /// <param name="fineTuneId">The fine tune identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuneCancelResponse
        /// </returns>
        Task<HttpOperationResult<FineTuneCancelResponse>> CancelAsync(string fineTuneId, CancellationToken cancellationToken);

        /// <summary>Deletes a fine tune model asynchronously, owned by your organization.</summary>
        /// <param name="model">The model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuneDeleteModelResponse
        /// </returns>
        Task<HttpOperationResult<FineTuneDeleteModelResponse>> DeleteAsync(string model, CancellationToken cancellationToken);

        /// <summary>Gets the events of a fine tune job asynchronously.</summary>
        /// <param name="fineTuneId">The fine tune identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuneEventsResponse
        /// </returns>
        Task<HttpOperationResult<FineTuneJobEventsResponse>> GetEventsAsync(string fineTuneId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the events as stream asynchronous.
        /// WARNING: method will block until cancelling with CancellationToken,
        /// or the Fine Tune Job cancel called on the API.
        /// </summary>
        /// <param name="fineTuneId">The fine tune identifier.</param>
        /// <param name="resultCallback">The result callback.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   HttpOperationResult
        /// </returns>
        Task<HttpOperationResult> GetEventsAsStreamAsync(string fineTuneId, Action<HttpOperationResult<FineTuneJobEvent>> resultCallback, CancellationToken cancellationToken);

#if NETCOREAPP3_1_OR_GREATER
        /// <summary>
        /// Gets the events as stream asynchronous.
        /// WARNING: method will block until cancelling with CancellationToken,
        /// or the Fine Tune Job cancel called on the API.
        /// </summary>
        /// <param name="fineTuneId">The fine tune identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuneJobEvent
        /// </returns>
        IAsyncEnumerable<HttpOperationResult<FineTuneJobEvent>> GetEventsAsStreamAsync(string fineTuneId, CancellationToken cancellationToken);
#endif

    }

}
