using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>Represents the model service</summary>
    public interface IModelService
    {

        /// <summary>Gets the all available models</summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>ModelsResponse</returns>
        Task<HttpOperationResult<ModelsResponse>> GetAsync(CancellationToken cancellationToken = default);

        /// <summary>Gets the specified model</summary>
        /// <param name="modelId">The model identifier.</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>
        ///   Model
        /// </returns>
        Task<HttpOperationResult<Model>> GetAsync(string modelId, CancellationToken cancellationToken = default);

    }

}
