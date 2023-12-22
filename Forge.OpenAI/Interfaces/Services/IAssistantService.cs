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
    public interface IAssistantService
    {

        /// <summary>Creates a assistant asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   AssistantResponse
        /// </returns>
        Task<HttpOperationResult<AssistantResponse>> CreateAsync(CreateAssistantRequest request, CancellationToken cancellationToken = default);

        /// <summary>Gets a assistant data asynchronously.</summary>
        /// <param name="assistantId">The assistant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   AssistantResponse
        /// </returns>
        Task<HttpOperationResult<AssistantResponse>> GetAsync(string assistantId, CancellationToken cancellationToken = default);

        /// <summary>Gets the list of assistants asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   AssistantListResponse
        /// </returns>
        Task<HttpOperationResult<AssistantListResponse>> GetAsync(AssistantListRequest request, CancellationToken cancellationToken = default);

        /// <summary>Deletes a assistant asynchronously</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   AssistantResponse
        /// </returns>
        Task<HttpOperationResult<AssistantResponse>> ModifyAsync(ModifyAssistantRequest request, CancellationToken cancellationToken = default);

        /// <summary>Deletes an assistant asynchronously.</summary>
        /// <param name="assistantId">The assistant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   DeleteStateResponse
        /// </returns>
        Task<HttpOperationResult<DeleteStateResponse>> DeleteAsync(string assistantId, CancellationToken cancellationToken = default);

    }

}
