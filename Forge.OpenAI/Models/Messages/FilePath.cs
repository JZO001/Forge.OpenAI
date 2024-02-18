using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Messages
{

    public class FilePath
    {

        /// <summary>
        /// The ID of the file that was generated.
        /// </summary>
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }

    }

}
