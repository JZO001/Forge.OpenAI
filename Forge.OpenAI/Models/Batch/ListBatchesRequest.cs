using System.Text.Json.Serialization;
using Forge.OpenAI.Models.Common;

namespace Forge.OpenAI.Models.Batch
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/batch/list
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Models.Common.RequestBase" />
    public class ListBatchesRequest : RequestBase
    {

        /// <summary>
        /// Identifier for the last job from the previous pagination request.
        /// </summary>
        [JsonPropertyName("after")]
        public string After { get; set; }

        /// <summary>
        /// Number of fine-tuning jobs to retrieve.
        /// Defaults to 20.
        /// </summary>
        [JsonPropertyName("limit")]
        public int? Limit { get; set; }

    }

}
