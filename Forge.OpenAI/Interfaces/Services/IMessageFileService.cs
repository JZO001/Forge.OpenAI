using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Messages;
using System.Threading.Tasks;
using System.Threading;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>
    /// Create messages within threads
    /// https://platform.openai.com/docs/api-reference/messages
    /// </summary>
    public interface IMessageFileService
    {

        /// <summary>Gets a message files asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   AssistantFileResponse
        /// </returns>
        Task<HttpOperationResult<MessageFileListResponse>> GetAsync(MessageFileListRequest request, CancellationToken cancellationToken = default);

        /// <summary>Gets the list of assistant files asynchronously.</summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   AssistantFileListResponse
        /// </returns>
        Task<HttpOperationResult<MessageFileResponse>> GetAsync(string threadId, string messageId, string fileId, CancellationToken cancellationToken = default);

    }

}
