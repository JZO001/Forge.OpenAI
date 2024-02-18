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
    public interface IMessageService
    {

        /// <summary>Creates a message asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   CreateMessageResponse
        /// </returns>
        Task<HttpOperationResult<CreateMessageResponse>> CreateAsync(CreateMessageRequest request, CancellationToken cancellationToken = default);

        /// <summary>Gets a message data asynchronously.</summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="messageId">The message identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   MessageResponse
        /// </returns>
        Task<HttpOperationResult<MessageResponse>> GetAsync(string threadId, string messageId, CancellationToken cancellationToken = default);

        /// <summary>Gets the list of messages asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   MessageListResponse
        /// </returns>
        Task<HttpOperationResult<MessageListResponse>> GetAsync(MessageListRequest request, CancellationToken cancellationToken = default);

        /// <summary>Modifies a message asynchronously</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   MessageResponse
        /// </returns>
        Task<HttpOperationResult<ModifyMessageResponse>> ModifyAsync(ModifyMessageRequest request, CancellationToken cancellationToken = default);

    }

}
