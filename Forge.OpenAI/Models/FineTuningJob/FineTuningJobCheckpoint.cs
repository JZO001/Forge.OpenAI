using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.FineTuningJob
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/fine-tuning/checkpoint-object
    /// </summary>
    public class FineTuningJobCheckpoint
    {

        /// <summary>
        /// The object identifier, which can be referenced in the API endpoints.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the fine-tuning job was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }

        /// <summary>
        /// The name of the fine-tuned model that is being created.
        /// </summary>
        [JsonPropertyName("fine_tuned_model_checkpoint")]
        public string FineTunedModelCheckpoint { get; set; }

        /// <summary>
        /// The step number that the checkpoint was created at.
        /// </summary>
        [JsonPropertyName("step_number")]
        public int StepNumber { get; set; }

        /// <summary>
        /// Metrics at the step number during the fine-tuning job.
        /// </summary>
        public FineTuningJobCheckpointMetrics Metrics { get; set; }

        /// <summary>
        /// The name of the fine-tuning job that this checkpoint was created from.
        /// </summary>
        [JsonPropertyName("fine_tuning_job_id")]
        public string FineTuningJobId { get; set; }

        /// <summary>
        /// The object type, which is always "fine_tuning.job.checkpoint".
        /// </summary>
        [JsonPropertyName("object")]
        public string Object { get; set; }

    }

}
