using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Threads;
using System.Threading.Tasks;
using System.Threading;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>
    /// Create threads that assistants can interact with.
    /// https://platform.openai.com/docs/api-reference/threads
    /// </summary>
    public interface IThreadsService
    {

        /// <summary>Creates a thread asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   CreateThreadResponse
        /// </returns>
        Task<HttpOperationResult<CreateThreadResponse>> CreateAsync(CreateThreadRequest request, CancellationToken cancellationToken = default);

        /// <summary>Gets a thread data asynchronously.</summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   RetrieveThreadResponse
        /// </returns>
        Task<HttpOperationResult<RetrieveThreadResponse>> GetAsync(string threadId, CancellationToken cancellationToken = default);

        /// <summary>Modifies a thread asynchronously</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ModifyThreadResponse
        /// </returns>
        Task<HttpOperationResult<ModifyThreadResponse>> ModifyAsync(ModifyThreadRequest request, CancellationToken cancellationToken = default);

        /// <summary>Deletes a thread asynchronously.</summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   DeleteThreadResponse
        /// </returns>
        Task<HttpOperationResult<DeleteThreadResponse>> DeleteAsync(string threadId, CancellationToken cancellationToken = default);

    }

}
