using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Messages
{

    public class Annotation
    {

        public const string ANNOTATION_TYPE_FILE_CITATION = "file_citation";
        public const string ANNOTATION_TYPE_FILE_PATH = "file_path";

        /// <summary>See the consts above</summary>
        /// <value>The type.</value>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// The text in the message content that needs to be replaced.
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// A citation within the message that points to a specific quote from a
        /// specific File associated with the assistant or the message.
        /// Generated when the assistant uses the 'retrieval' tool to search files.
        /// </summary>
        [JsonPropertyName("file_citation")]
        public FileCitation FileCitation { get; set; }

        /// <summary>
        /// A URL for the file that's generated when the assistant used the 'code_interpreter' tool to generate a file.
        /// </summary>
        [JsonPropertyName("file_path")]
        public FilePath FilePath { get; set; }

        [JsonPropertyName("start_index")]
        public int StartIndex { get; set; }

        [JsonPropertyName("end_index")]
        public int EndIndex { get; set; }

    }

}
