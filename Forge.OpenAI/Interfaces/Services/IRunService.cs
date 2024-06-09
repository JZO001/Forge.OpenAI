using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Models.Runs;
using Forge.OpenAI.Models.Common;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>
    /// Represents an execution run on a thread.
    /// https://platform.openai.com/docs/api-reference/runs
    /// </summary>
    public interface IRunService
    {

        /// <summary>Creates a run asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   CreateRunResponse
        /// </returns>
        Task<HttpOperationResult<CreateRunResponse>> CreateAsync(CreateRunRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a run as streamed and run asynchronously.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="resultCallback">The result callback.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>HttpOperationResult</returns>
        Task<HttpOperationResult> CreateAsStreamAsync(CreateRunRequest request, Action<HttpOperationResult<IAsyncEventInfo<CreateRunResponse>>> resultCallback, CancellationToken cancellationToken = default);

#if NETCOREAPP3_1_OR_GREATER
        /// <summary>Creates a run asynchronously in streamed mode. This method is only available in .NET Core applications.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   IAsyncEnumerable
        /// </returns>
        IAsyncEnumerable<HttpOperationResult<IAsyncEventInfo<CreateRunResponse>>> CreateAsStreamAsync(CreateRunRequest request, CancellationToken cancellationToken = default);
#endif

        /// <summary>Creates a thread and run in one request asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   CreateThreadAndRunResponse
        /// </returns>
        Task<HttpOperationResult<CreateThreadAndRunResponse>> CreateThreadAndRunAsync(CreateThreadAndRunRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a thread and run in one request asynchronously in streamed mode.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="resultCallback">The result callback.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<HttpOperationResult> CreateThreadAndRunAsStreamAsync(CreateThreadAndRunRequest request, Action<HttpOperationResult<IAsyncEventInfo<CreateThreadAndRunResponse>>> resultCallback, CancellationToken cancellationToken = default);

#if NETCOREAPP3_1_OR_GREATER
        /// <summary>Creates a thread and run in one request asynchronously in streamed mode. This method is only available in .NET Core applications.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   IAsyncEnumerable
        /// </returns>
        IAsyncEnumerable<HttpOperationResult<IAsyncEventInfo<CreateThreadAndRunResponse>>> CreateThreadAndRunStreamAsync(CreateThreadAndRunRequest request, CancellationToken cancellationToken = default);
#endif

        /// <summary>Gets a run data asynchronously.</summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="runId">The run identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   RunResponse
        /// </returns>
        Task<HttpOperationResult<RunResponse>> GetAsync(string threadId, string runId, CancellationToken cancellationToken = default);

        /// <summary>Gets the list of runs asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   RunListResponse
        /// </returns>
        Task<HttpOperationResult<RunListResponse>> GetAsync(RunListRequest request, CancellationToken cancellationToken = default);

        /// <summary>Modifies a run asynchronously</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ModifyRunResponse
        /// </returns>
        Task<HttpOperationResult<ModifyRunResponse>> ModifyAsync(ModifyRunRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// When a run has the status: "requires_action" and required_action.type is submit_tool_outputs, 
        /// this endpoint can be used to submit the outputs from the tool calls once they're all completed. 
        /// All outputs must be submitted in a single request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   SubmitToolOutputsToRunResponse
        /// </returns>
        Task<HttpOperationResult<SubmitToolOutputsToRunResponse>> SubmitToolOutputsToRunAsync(SubmitToolOutputsToRunRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// When a run has the status: "requires_action" and required_action.type is submit_tool_outputs, 
        /// this endpoint can be used to submit the outputs from the tool calls once they're all completed. 
        /// All outputs must be submitted in a single request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="resultCallback">The result callback.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>HttpOperationResult</returns>
        Task<HttpOperationResult> SubmitToolOutputsToRunAsStreamAsync(SubmitToolOutputsToRunRequest request, Action<HttpOperationResult<IAsyncEventInfo<SubmitToolOutputsToRunResponse>>> resultCallback, CancellationToken cancellationToken = default);

#if NETCOREAPP3_1_OR_GREATER
        /// <summary>
        /// When a run has the status: "requires_action" and required_action.type is submit_tool_outputs, 
        /// this endpoint can be used to submit the outputs from the tool calls once they're all completed. 
        /// All outputs must be submitted in a single request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   IAsyncEnumerable
        /// </returns>
        IAsyncEnumerable<HttpOperationResult<IAsyncEventInfo<SubmitToolOutputsToRunResponse>>> SubmitToolOutputsToRunStreamAsync(SubmitToolOutputsToRunRequest request, CancellationToken cancellationToken = default);
#endif

        /// <summary>Cancels a run that is in_progress.</summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="runId">The run identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   RunResponse
        /// </returns>
        Task<HttpOperationResult<RunResponse>> CancelAsync(string threadId, string runId, CancellationToken cancellationToken = default);

    }

}
