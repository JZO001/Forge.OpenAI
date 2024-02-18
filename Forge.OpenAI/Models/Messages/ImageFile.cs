using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Messages
{

    public class ImageFile
    {

        /// <summary>
        /// The file ID of the image.
        /// </summary>
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }

    }

}
