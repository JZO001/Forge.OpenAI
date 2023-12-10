using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.TextCompletions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>
    /// Given a prompt, the model will return one or more predicted completions, <br />
    /// and can also return the probabilities of alternative tokens at each position.
    /// </summary>
    [Obsolete]
    public interface ITextCompletionService
    {

        /// <summary>Request a text completion asynchronously</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   HttpOperationResult
        /// </returns>
        Task<HttpOperationResult<TextCompletionResponse>> GetAsync(TextCompletionRequest request, CancellationToken cancellationToken = default);

        /// <summary>Request a text completion asynchronously in streamed mode.</summary>
        /// <param name="request">The request.</param>
        /// <param name="resultCallback">The result callback which handles the incoming data fragments.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   HttpOperationResult
        /// </returns>
        Task<HttpOperationResult> GetStreamAsync(TextCompletionRequest request, Action<HttpOperationResult<TextCompletionResponse>> resultCallback, CancellationToken cancellationToken = default);

#if NETCOREAPP3_1_OR_GREATER
        /// <summary>Request a text completion asynchronously in streamed mode. This method is only available in .NET Core applications.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   IAsyncEnumerable
        /// </returns>
        IAsyncEnumerable<HttpOperationResult<TextCompletionResponse>> GetStreamAsync(TextCompletionRequest request, CancellationToken cancellationToken = default);
#endif

    }

}
