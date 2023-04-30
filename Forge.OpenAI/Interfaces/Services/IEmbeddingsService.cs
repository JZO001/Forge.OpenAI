using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Embeddings;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>Represents the embeddings service</summary>
    public interface IEmbeddingsService
    {

        /// <summary>Acquire embeddings asynchronously</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   EmbeddingsResponse
        /// </returns>
        Task<HttpOperationResult<EmbeddingsResponse>> GetAsync(EmbeddingsRequest request, CancellationToken cancellationToken = default);

    }

}
