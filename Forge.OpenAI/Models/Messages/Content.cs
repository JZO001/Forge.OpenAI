using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Messages
{

    /// <summary>The content of the message in array of text and/or images.</summary>
    public class Content
    {

        public const string CONTENT_TYPE_TEXT = "text";
        public const string CONTENT_TYPE_IMAGE_URL = "image_url";
        public const string CONTENT_TYPE_IMAGE_FILE = "image_file";

        /// <summary>See the constants above</summary>
        /// <value>The type.</value>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>The text content that is part of a message.</summary>
        /// <value>The text.</value>
        [JsonPropertyName("text")]
        public TextContent Text { get; set; }

        /// <summary>References an image File in the content of a message.</summary>
        /// <value>The image file.</value>
        [JsonPropertyName("image_file")]
        public ImageFile ImageFile { get; set; }

    }

}
