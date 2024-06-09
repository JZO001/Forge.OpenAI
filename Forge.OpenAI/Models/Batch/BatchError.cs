using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Batch
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/batch/object#batch/object-errors
    /// </summary>
    public class BatchError
    {

        /// <summary>
        /// The object type, which is always list.
        /// </summary>
        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("data")]
        public IReadOnlyList<BatchErrorItem> ErrorItems { get; set; }

    }

}
