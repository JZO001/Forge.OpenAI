using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Moderations;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>Moderation service</summary>
    public interface IModerationService
    {

        /// <summary>Acquire moderation</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ModerationResponse
        /// </returns>
        Task<HttpOperationResult<ModerationResponse>> GetAsync(ModerationRequest request, CancellationToken cancellationToken = default);

    }

}
