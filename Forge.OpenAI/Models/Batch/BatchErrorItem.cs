using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Batch
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/batch/object#batch/object-errors
    /// </summary>
    public class BatchErrorItem
    {

        /// <summary>
        /// An error code identifying the error type.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// A human-readable message providing more details about the error.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// The name of the parameter that caused the error, if applicable.
        /// </summary>
        [JsonPropertyName("param")]
        public string Param { get; set; }

        /// <summary>
        /// The line number of the input file where the error occurred, if applicable.
        /// </summary>
        [JsonPropertyName("line")]
        public string Line { get; set; }

    }

}
