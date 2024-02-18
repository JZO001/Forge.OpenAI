using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Images;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>Represents the image service</summary>
    public interface IImageService
    {

        /// <summary>Creates new image(s) asynchronously</summary>
        /// <param name="request">The image create request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ImageCreateResponse
        /// </returns>
        Task<HttpOperationResult<ImageCreateResponse>> CreateImageAsync(ImageCreateRequest request, CancellationToken cancellationToken = default);

        /// <summary>Edits an image asynchronously</summary>
        /// <param name="request">The image edit request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ImageEditResponse
        /// </returns>
        Task<HttpOperationResult<ImageEditResponse>> EditImageAsync(ImageEditRequest request, CancellationToken cancellationToken = default);

        /// <summary>Variates an image asynchronously</summary>
        /// <param name="request">The image variation request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ImageVariationResponse
        /// </returns>
        Task<HttpOperationResult<ImageVariationResponse>> VariateImageAsync(ImageVariationRequest request, CancellationToken cancellationToken = default);

    }

}
