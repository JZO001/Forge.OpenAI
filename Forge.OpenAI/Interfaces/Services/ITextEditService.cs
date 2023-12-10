using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.TextEdits;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>Represents a text edit service</summary>
    [Obsolete]
    public interface ITextEditService
    {

        /// <summary>Request a text edit operation</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   TextEditResponse
        /// </returns>
        Task<HttpOperationResult<TextEditResponse>> GetAsync(TextEditRequest request, CancellationToken cancellationToken = default);

    }

}
