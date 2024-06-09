using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Batch
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/batch/object#batch/object-request_counts
    /// </summary>
    public class BatchRequestCount
    {

        /// <summary>
        /// Total number of requests in the batch.
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }

        /// <summary>
        /// Number of requests that have been completed successfully.
        /// </summary>
        [JsonPropertyName("completed")]
        public int Completed { get; set; }

        /// <summary>
        /// Number of requests that have failed.
        /// </summary>
        [JsonPropertyName("failed")]
        public int Failed { get; set; }

    }

}
