using Forge.OpenAI.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Images
{

    /// <summary>Represents an image creation request</summary>
    public class ImageCreateRequest : RequestBase
    {

        public const string QUALITY_STANDARD = "standard";
        public const string QUALITY_HD = "hd";

        public const string STYLE_VIVID = "vivid";
        public const string STYLE_NATURAL = "natural";

        /// <summary>Initializes a new instance of the <see cref="ImageCreateRequest" /> class.</summary>
        public ImageCreateRequest()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ImageCreateRequest" /> class.</summary>
        /// <param name="prompt">The prompt.</param>
        /// <exception cref="ArgumentNullException">prompt</exception>
        /// <exception cref="ArgumentOutOfRangeException">prompt - The maximum character length for the prompt is 1000 characters.</exception>
        public ImageCreateRequest(string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt)) throw new ArgumentNullException(nameof(prompt));
            if (prompt.Length > 1000)
            {
                throw new ArgumentOutOfRangeException(nameof(prompt), "The maximum character length for the prompt is 1000 characters.");
            }
            Prompt = prompt;
        }

        /// <summary>Initializes a new instance of the <see cref="ImageCreateRequest" /> class.</summary>
        /// <param name="prompt">The prompt.</param>
        /// <param name="numberOfCreatedImages">The number of created images.</param>
        /// <param name="imageSize">Size of the image.</param>
        /// <param name="responseFormat">The image response format.</param>
        /// <param name="user">The user.</param>
        /// <exception cref="ArgumentOutOfRangeException">number - The number of results must be between 1 and 10</exception>
        public ImageCreateRequest(string prompt, 
            int numberOfCreatedImages = 1,
            ImageSizeEnum imageSize = ImageSizeEnum.Size_1024_x_1024,
            ImageResponseFormatEnum responseFormat = ImageResponseFormatEnum.Url,
#if NETCOREAPP3_1_OR_GREATER
            string? user = null
#else
            string user = null
#endif
            )
            : this(prompt)
        {
            if (numberOfCreatedImages > 10 || numberOfCreatedImages < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfCreatedImages), "The number of results must be between 1 and 10");
            }

            NumberOfCreatedImages = numberOfCreatedImages;
            Size = ImageSize.ConvertImageSizeEnumToString(imageSize);
            ResponseFormat = ImageResponseFormat.ConvertImageResponseFormatEnumToString(responseFormat);
            User = user;
        }

        /// <summary>
        /// A text description of the desired image(s). The maximum length is 1000 characters.
        /// </summary>
        [Required]
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }

        /// <summary>The model to use for image generation.</summary>
        /// <value>The model.</value>
        [JsonPropertyName("model")]
        [Required]
        public string Model { get; set; } = KnownModelTypes.Dall_E_3;

        /// <summary>
        /// The number of images to generate. Must be between 1 and 10.
        /// If not set, the default is 1.
        /// </summary>
        [JsonPropertyName("n")]
        public int NumberOfCreatedImages { get; set; } = 1;

        /// <summary>
        /// The quality of the image that will be generated. 
        /// hd creates images with finer details and greater consistency across the image. 
        /// This param is only supported for dall-e-3.
        /// </summary>
        /// <value>The quality.</value>
        [JsonPropertyName("quality")]
        [Required]
        public string Quality { get; set; } = QUALITY_STANDARD;

        /// <summary>
        /// The style of the generated images. 
        /// Must be one of vivid or natural. 
        /// Vivid causes the model to lean towards generating hyper-real and dramatic images. 
        /// Natural causes the model to produce more natural, less hyper-real looking images. 
        /// This param is only supported for dall-e-3.</summary>
        /// <value>The style.</value>
        [JsonPropertyName("style")]
        [Required]
        public string Style { get; set; } = STYLE_VIVID;

        /// <summary>
        /// The size of the generated images. It can be one of 256x256, 512x512, or 1024x1024.
        /// If not set, the default is 1024x1024.
        /// https://platform.openai.com/docs/api-reference/images/create#images-create-size
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
#if NETCOREAPP3_1_OR_GREATER
        public string? User { get; set; }
#else
        public string User { get; set; }
#endif

    }

}
