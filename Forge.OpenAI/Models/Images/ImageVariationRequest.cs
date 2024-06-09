using Forge.OpenAI.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Images
{

    /// <summary>Represents the request for a variation image operation</summary>
    public class ImageVariationRequest : RequestBase
    {

        /// <summary>Initializes a new instance of the <see cref="ImageVariationRequest" /> class.</summary>
        public ImageVariationRequest()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ImageVariationRequest" /> class.</summary>
        /// <param name="image">The image.</param>
        /// <exception cref="ArgumentNullException">image</exception>
        public ImageVariationRequest(BinaryContentData image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            Image = image;
        }

        /// <summary>Initializes a new instance of the <see cref="ImageVariationRequest" /> class.</summary>
        /// <param name="image">The image.</param>
        /// <param name="numberOfVariationImages">The image output number.</param>
        /// <param name="imageSize">The image size.</param>
        /// <param name="responseFormat">The response format.</param>
        /// <param name="user">The user.</param>
        public ImageVariationRequest(BinaryContentData image, int numberOfVariationImages = 1,
            ImageSizeEnum imageSize = ImageSizeEnum.Size_1024_x_1024,
            ImageResponseFormatEnum responseFormat = ImageResponseFormatEnum.Url,
            string user = null)
            : this(image)
        {
            NumberOfVariationImages = numberOfVariationImages;
            Size = ImageSize.ConvertImageSizeEnumToString(imageSize);
            ResponseFormat = ImageResponseFormat.ConvertImageResponseFormatEnumToString(responseFormat);
            User = user;
        }

        /// <summary>
        /// The image to use as the basis for the variation(s). Must be a valid PNG file, less than 4MB, and square.
        /// https://platform.openai.com/docs/api-reference/images/createVariation#images-createvariation-image
        /// </summary>
        /// <value>The image.</value>
        [JsonPropertyName("image")]
        [Required]
        public BinaryContentData Image { get; set; }

        /// <summary>The model to use for image generation.</summary>
        /// <value>The model.</value>
        [JsonPropertyName("model")]
        [Required]
        public string Model { get; set; } = KnownModelTypes.Dall_E_2;

        /// <summary>
        /// The number of images to generate. Must be between 1 and 10.
        /// If not set, the default is 1.
        /// </summary>
        [JsonPropertyName("n")]
        public int NumberOfVariationImages { get; set; } = 1;

        /// <summary>
        /// The format in which the generated images are returned. Must be one of url or b64_json. <br/>
        /// If not set, the default is 'url'
        /// </summary>
        [JsonPropertyName("response_format")]
        public string ResponseFormat { get; set; }

        /// <summary>
        /// The size of the generated images. It can be one of 256x256, 512x512, or 1024x1024.
        /// If not set, the default is 1024x1024.
        /// https://platform.openai.com/docs/api-reference/images/create
        /// </summary>
        [JsonPropertyName("size")]
        public string Size { get; set; }

        /// <summary>
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.
        /// </summary>
        [JsonPropertyName("user")]
        public string User { get; set; }

    }

}
