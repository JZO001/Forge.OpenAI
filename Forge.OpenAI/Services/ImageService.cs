using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Images;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Services
{

    /// <summary>Represents the image service</summary>
    public class ImageService : IImageService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;
        private readonly IProviderEndpointService _providerEndpointService;

        /// <summary>Initializes a new instance of the <see cref="ImageService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API communication service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiCommunicationService</exception>
        public ImageService(OpenAIOptions options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (apiHttpService == null) throw new ArgumentNullException(nameof(apiHttpService));

            _options = options;
            _apiHttpService = apiHttpService;
            _providerEndpointService = providerEndpointService;
        }

        /// <summary>Initializes a new instance of the <see cref="ImageService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API communication service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        public ImageService(IOptions<OpenAIOptions> options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
            : this(options?.Value, apiHttpService, providerEndpointService)
        {
        }

        /// <summary>Creates new image(s) asynchronously</summary>
        /// <param name="imageCreateRequest">The image create request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ImageCreateResponse
        /// </returns>
        public async Task<HttpOperationResult<ImageCreateResponse>> CreateImageAsync(ImageCreateRequest imageCreateRequest, CancellationToken cancellationToken = default)
        {
            var validationResult = imageCreateRequest.Validate<ImageCreateResponse>();
            if (validationResult != null) return validationResult;
            
            return await _apiHttpService.PostAsync<ImageCreateRequest, ImageCreateResponse>(GetCreateUri(), imageCreateRequest, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Edits an image asynchronously</summary>
        /// <param name="imageEditRequest">The image edit request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ImageEditResponse
        /// </returns>
        public async Task<HttpOperationResult<ImageEditResponse>> EditImageAsync(ImageEditRequest imageEditRequest, CancellationToken cancellationToken = default)
        {
            var validationResult = imageEditRequest.Validate<ImageEditResponse>();
            if (validationResult != null) return validationResult;
            
            return await _apiHttpService.PostAsync<ImageEditRequest, ImageEditResponse>(GetEditUri(), imageEditRequest, ImageEditHttpContentFactoryAsync, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Variates an image asynchronously</summary>
        /// <param name="imageVariationRequest">The image variation request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ImageVariationResponse
        /// </returns>
        public async Task<HttpOperationResult<ImageVariationResponse>> VariateImageAsync(ImageVariationRequest imageVariationRequest, CancellationToken cancellationToken = default)
        {
            var validationResult = imageVariationRequest.Validate<ImageVariationResponse>();
            if (validationResult != null) return validationResult;
            
            return await _apiHttpService.PostAsync<ImageVariationRequest, ImageVariationResponse>(GetVariationUri(), imageVariationRequest, ImageVariationHttpContentFactoryAsync, cancellationToken).ConfigureAwait(false);
        }

        private string GetCreateUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.ImageCreateUri);
        }

        private string GetEditUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.ImageEditUri);
        }

        private string GetVariationUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.ImageVariationUri);
        }

        private async Task<HttpContent> ImageEditHttpContentFactoryAsync(ImageEditRequest imageEditRequest, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(imageEditRequest.Prompt)) throw new InvalidOperationException("Missing prompt data");
            if (imageEditRequest.Image == null) throw new InvalidOperationException("Missing image content data");
            if (imageEditRequest.Image.SourceContent == null && imageEditRequest.Image.SourceStream == null) throw new InvalidOperationException("No image content nor image stream defined in image content data.");
            if (string.IsNullOrWhiteSpace(imageEditRequest.Image.ContentName)) throw new InvalidOperationException("Missing image name in image content data");

            if (imageEditRequest.Mask != null)
            {
                if (imageEditRequest.Mask.SourceContent == null && imageEditRequest.Mask.SourceStream == null) throw new InvalidOperationException("No image content nor image stream defined in mask content data.");
                if (string.IsNullOrWhiteSpace(imageEditRequest.Mask.ContentName)) throw new InvalidOperationException("Missing image name in mask content data");
            }

            MultipartFormDataContent content = new MultipartFormDataContent();
            
            // add image content
            if (imageEditRequest.Image.SourceContent != null)
            {
                content.Add(new ByteArrayContent(imageEditRequest.Image.SourceContent), "image", imageEditRequest.Image.ContentName);
            }
            else
            {
                using (MemoryStream imageData = new MemoryStream())
                {
                    await imageEditRequest.Image.SourceStream.CopyToAsync(imageData, 81920, cancellationToken).ConfigureAwait(false);
                    content.Add(new ByteArrayContent(imageData.ToArray()), "image", imageEditRequest.Image.ContentName);
                    imageData.SetLength(0);
                }
            }

            if (imageEditRequest.Mask != null)
            {
                // add mask content
                if (imageEditRequest.Mask.SourceContent != null)
                {
                    content.Add(new ByteArrayContent(imageEditRequest.Mask.SourceContent), "mask", imageEditRequest.Mask.ContentName);
                }
                else
                {
                    using (MemoryStream maskImageData = new MemoryStream())
                    {
                        await imageEditRequest.Mask.SourceStream.CopyToAsync(maskImageData, 81920, cancellationToken).ConfigureAwait(false);
                        content.Add(new ByteArrayContent(maskImageData.ToArray()), "mask", imageEditRequest.Mask.ContentName);
                        maskImageData.SetLength(0);
                    }
                }
            }

            content.Add(new StringContent(imageEditRequest.Prompt), "prompt");
            content.Add(new StringContent(imageEditRequest.NumberOfEditedImages.ToString()), "n");

            if (!string.IsNullOrWhiteSpace(imageEditRequest.Size)) content.Add(new StringContent(imageEditRequest.Size), "size");
            if (!string.IsNullOrWhiteSpace(imageEditRequest.ResponseFormat)) content.Add(new StringContent(imageEditRequest.ResponseFormat), "response_format");
            if (!string.IsNullOrWhiteSpace(imageEditRequest.User)) content.Add(new StringContent(imageEditRequest.User), "user");

            return content;
        }

        private async Task<HttpContent> ImageVariationHttpContentFactoryAsync(ImageVariationRequest imageVariationRequest, CancellationToken cancellationToken)
        {
            if (imageVariationRequest.Image == null) throw new InvalidOperationException("Missing image content data");
            if (imageVariationRequest.Image.SourceContent == null && imageVariationRequest.Image.SourceStream == null) throw new InvalidOperationException("No image content nor image stream defined in image content data.");
            if (string.IsNullOrWhiteSpace(imageVariationRequest.Image.ContentName)) throw new InvalidOperationException("Missing image name in image content data");

            MultipartFormDataContent content = new MultipartFormDataContent();

            // add image content
            if (imageVariationRequest.Image.SourceContent != null)
            {
                content.Add(new ByteArrayContent(imageVariationRequest.Image.SourceContent), "image", imageVariationRequest.Image.ContentName);
            }
            else
            {
                using (MemoryStream imageData = new MemoryStream())
                {
                    await imageVariationRequest.Image.SourceStream.CopyToAsync(imageData, 81920, cancellationToken).ConfigureAwait(false);
                    content.Add(new ByteArrayContent(imageData.ToArray()), "image", imageVariationRequest.Image.ContentName);
                    imageData.SetLength(0);
                }
            }

            content.Add(new StringContent(imageVariationRequest.NumberOfVariationImages.ToString()), "n");

            if (!string.IsNullOrWhiteSpace(imageVariationRequest.Size)) content.Add(new StringContent(imageVariationRequest.Size), "size");
            if (!string.IsNullOrWhiteSpace(imageVariationRequest.ResponseFormat)) content.Add(new StringContent(imageVariationRequest.Size), "response_format");
            if (!string.IsNullOrWhiteSpace(imageVariationRequest.User)) content.Add(new StringContent(imageVariationRequest.User), "user");

            return content;
        }

    }

}
