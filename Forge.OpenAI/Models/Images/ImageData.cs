using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Images
{

    /// <summary>Represents the image data</summary>
    public class ImageData
    {

        /// <summary>Initializes a new instance of the <see cref="ImageData" /> class.</summary>
        public ImageData()
        {
        }

        /// <summary>Gets the image URL where it is downloadable</summary>
        /// <value>The image URL.</value>
        [JsonPropertyName("url")]
        public string ImageUrl { get; set; }

        /// <summary>Gets the image data in Base64 format</summary>
        /// <value>The base64 data.</value>
        [JsonPropertyName("b64_json")]
        public string Base64Data { get; set; }

        /// <summary>The prompt that was used to generate the image, if there was any revision to the prompt.</summary>
        /// <value>The revised prompt.</value>
        [JsonPropertyName("revised_prompt")]
        public string RevisedPrompt { get; private set; }

    }

}
