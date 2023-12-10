using Forge.OpenAI.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Images
{

    /// <summary>Represents an image edit request</summary>
    public class ImageEditRequest : RequestBase
    {

        /// <summary>Initializes a new instance of the <see cref="ImageEditRequest" /> class.</summary>
        public ImageEditRequest()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ImageEditRequest" /> class.</summary>
        /// <param name="prompt">The prompt.</param>
        /// <param name="imageContentData">The image content data.</param>
        /// <exception cref="ArgumentNullException">prompt
        /// or
        /// imageContentData</exception>
        /// <exception cref="ArgumentOutOfRangeException">prompt - The maximum character length for the prompt is 1000 characters.</exception>
        public ImageEditRequest(string prompt, BinaryContentData imageContentData)
        {
            if (string.IsNullOrWhiteSpace(prompt)) throw new ArgumentNullException(nameof(prompt));
            if (prompt.Length > 1000)
            {
                throw new ArgumentOutOfRangeException(nameof(prompt), "The maximum character length for the prompt is 1000 characters.");
            }
            if (imageContentData == null) throw new ArgumentNullException(nameof(imageContentData));
            Prompt = prompt;
            Image = imageContentData;
        }

        /// <summary>Initializes a new instance of the <see cref="ImageEditRequest" /> class.</summary>
        /// <param name="prompt">The prompt.</param>
        /// <param name="imageContentData">The image content data.</param>
        /// <param name="maskContentData">The mask content data.</param>
        /// <param name="numberOfEditedImages">The number.</param>
        /// <param name="imageSize">Size of the image.</param>
        /// <param name="responseFormat">The response format.</param>
        /// <param name="user">The user.</param>
        /// <exception cref="ArgumentOutOfRangeException">number - The number of results must be between 1 and 10</exception>
        public ImageEditRequest(string prompt, BinaryContentData imageContentData,
            BinaryContentData maskContentData,
            int numberOfEditedImages = 1, ImageSizeEnum imageSize = ImageSizeEnum.Size_1024_x_1024,
            ImageResponseFormatEnum responseFormat = ImageResponseFormatEnum.Url,
            string user = null)
            : this(prompt, imageContentData)
        {
            if (numberOfEditedImages > 10 || numberOfEditedImages < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfEditedImages), "The number of results must be between 1 and 10");
            }

            Mask = maskContentData;
            NumberOfEditedImages = numberOfEditedImages;
            Size = ImageSize.ConvertImageSizeEnumToString(imageSize);
            ResponseFormat = ImageResponseFormat.ConvertImageResponseFormatEnumToString(responseFormat);
            User = user;
        }

        /// <summary>Gets or sets the image to edit.</summary>
        /// <value>The image.</value>
        [Required]
        public BinaryContentData Image { get; set; }

        /// <summary>
        /// A text description of the desired image(s). The maximum length is 1000 characters.
        /// </summary>
        [Required]
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }

        /// <summary>Gets or sets the mask image.</summary>
        /// <value>The mask.</value>
        public BinaryContentData Mask { get; set; }

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
        public int NumberOfEditedImages { get; set; } = 1;

        /// <summary>
        /// The size of the generated images. It can be one of 256x256, 512x512, or 1024x1024.
        /// If not set, the default is 1024x1024.
        /// https://platform.openai.com/docs/api-reference/images/create
        /// </summary>
        [JsonPropertyName("size")]
        public string Size { get; set; }

        /// <summary>
        /// The format in which the generated images are returned. Must be one of url or b64_json. <br/>
        /// If not set, the default is 'url'
        /// </summary>
        [JsonPropertyName("response_format")]
        public string ResponseFormat { get; set; }

        /// <summary>
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.
        /// </summary>
        [JsonPropertyName("user")]
        public string User { get; set; }

    }

}
