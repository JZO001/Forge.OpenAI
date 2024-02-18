using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.FineTuningJob
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/fine-tuning/list-events
    /// </summary>
    public class FineTuningJobListEventsRequest : FineTuningJobListRequest
    {

        /// <summary>
        /// The ID of the fine-tuning job to get events for.
        /// </summary>
        [Required]
        [JsonIgnore]
        public string FineTuningJobId { get; set; }

    }

}
