using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Forge.OpenAI.Models.Common;

namespace Forge.OpenAI.Models.FineTuningJob
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/fine-tuning/list-checkpoints
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Models.Common.RequestBase" />
    public class FineTuningJobCheckpointListRequest : RequestBase
    {

        /// <summary>
        /// The ID of the fine-tuning job to get checkpoints for.
        /// https://platform.openai.com/docs/api-reference/fine-tuning/list-checkpoints#fine-tuning-list-checkpoints-fine_tuning_job_id
        /// </summary>
        [Required]
        [JsonIgnore]
        public string FineTuningJobId { get; set; }

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
