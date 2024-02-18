using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Forge.OpenAI.Models.Common;

namespace Forge.OpenAI.Models.FineTuningJob
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/fine-tuning/cancel
    /// </summary>
    public class FineTuningJobCancelRequest : RequestBase
    {

        /// <summary>The ID of the fine-tuning job to cancel.</summary>
        /// <value>The fine tuning job identifier.</value>
        [Required]
        [JsonPropertyName("fine_tuning_job_id")]
        public string FineTuningJobId { get; set; }

    }

}
