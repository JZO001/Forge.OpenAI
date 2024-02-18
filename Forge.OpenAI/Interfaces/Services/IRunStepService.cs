using Forge.OpenAI.Models.Runs;
using Forge.OpenAI.Models.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>
    /// Represents an execution run on a thread.
    /// https://platform.openai.com/docs/api-reference/runs
    /// </summary>
    public interface IRunStepService
    {

        /// <summary>Gets a run data asynchronously.</summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="runId">The run identifier.</param>
        /// <param name="stepId">The step identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   RunStepResponse
        /// </returns>
        Task<HttpOperationResult<RunStepResponse>> GetAsync(string threadId, string runId, string stepId, CancellationToken cancellationToken = default);

        /// <summary>Gets the list of run steps asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   RunStepsListResponse
        /// </returns>
        Task<HttpOperationResult<RunStepsListResponse>> GetAsync(RunStepsListRequest request, CancellationToken cancellationToken = default);

    }

}
