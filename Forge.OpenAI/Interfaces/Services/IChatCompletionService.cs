using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Models.ChatCompletions;
using Forge.OpenAI.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>Given a chat conversation, the model will return a chat completion response.</summary>
    public interface IChatCompletionService
    {

        /// <summary>Request a chat completion asynchronously</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   HttpOperationResult
        /// </returns>
        Task<HttpOperationResult<ChatCompletionResponse>> GetAsync(ChatCompletionRequest request, CancellationToken cancellationToken = default);

        /// <summary>Request a chat completion asynchronously in streamed mode.</summary>
        /// <param name="request">The request.</param>
        /// <param name="resultCallback">The result callback which handles the incoming data fragments.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   HttpOperationResult
        /// </returns>
        Task<HttpOperationResult> GetStreamAsync(ChatCompletionRequest request, Action<HttpOperationResult<IAsyncEventInfo<ChatCompletionStreamedResponse>>> resultCallback, CancellationToken cancellationToken = default);

#if NETCOREAPP3_1_OR_GREATER
        /// <summary>Request a chat completion asynchronously in streamed mode. This method is only available in .NET Core applications.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   IAsyncEnumerable
        /// </returns>
        IAsyncEnumerable<HttpOperationResult<IAsyncEventInfo<ChatCompletionStreamedResponse>>> GetStreamAsync(ChatCompletionRequest request, CancellationToken cancellationToken = default);
#endif

    }

}
