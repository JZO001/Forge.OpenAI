using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Assistants;
using System.Threading.Tasks;
using System.Threading;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>
    /// Build assistants that can call models and use tools to perform tasks.
    /// https://platform.openai.com/docs/api-reference/assistants
    /// https://platform.openai.com/docs/assistants/overview
    /// </summary>
    public interface IAssistantFileService
    {

        /// <summary>Creates a assistant file asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   AssistantFileResponse
        /// </returns>
        Task<HttpOperationResult<AssistantFileResponse>> CreateAsync(CreateAssistantFileRequest request, CancellationToken cancellationToken = default);

        /// <summary>Gets a assistant file asynchronously.</summary>
        /// <param name="assistantId">The assistant identifier.</param>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   AssistantFileResponse
        /// </returns>
        Task<HttpOperationResult<AssistantFileResponse>> GetAsync(string assistantId, string fileId, CancellationToken cancellationToken = default);

        /// <summary>Gets the list of assistant files asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   AssistantFileListResponse
        /// </returns>
        Task<HttpOperationResult<AssistantFileListResponse>> GetAsync(AssistantFileListRequest request, CancellationToken cancellationToken = default);

        /// <summary>Deletes an assistant file asynchronously.</summary>
        /// <param name="assistantId">The assistant identifier.</param>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   DeleteStateResponse
        /// </returns>
        Task<HttpOperationResult<DeleteStateResponse>> DeleteAsync(string assistantId, string fileId, CancellationToken cancellationToken = default);

    }

}
