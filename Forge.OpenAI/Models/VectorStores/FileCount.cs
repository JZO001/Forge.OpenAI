using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.VectorStores
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/vector-stores/object#vector-stores/object-file_counts
    /// </summary>
    public class FileCount
    {

        /// <summary>
        /// The number of files that are currently being processed.
        /// </summary>
        [JsonPropertyName("in_progress")]
        public int InProgress { get; set; }

        /// <summary>
        /// The number of files that have been successfully processed.
        /// </summary>
        [JsonPropertyName("completed")]
        public int Completed { get; set; }

        /// <summary>
        /// The number of files that have failed to process.
        /// </summary>
        [JsonPropertyName("failed")]
        public int Failed { get; set; }

        /// <summary>
        /// The number of files that were cancelled.
        /// </summary>
        [JsonPropertyName("cancelled")]
        public int Cancelled { get; set; }

        /// <summary>
        /// The total number of files.
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }

    }

}
