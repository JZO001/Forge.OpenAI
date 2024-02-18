using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Messages
{

    /// <summary>
    /// A citation within the message that points to a specific quote from a specific File associated with the assistant or the message. Generated when the assistant uses the "retrieval" tool to search files.
    /// </summary>
    public class FileCitation
    {

        /// <summary>
        /// The ID of the specific File the citation is from.
        /// </summary>
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }

        /// <summary>
        /// The specific quote in the file.
        /// </summary>
        [JsonPropertyName("quote")]
        public string Quote { get; set; }

    }

}
