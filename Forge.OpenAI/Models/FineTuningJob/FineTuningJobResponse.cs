using Forge.OpenAI.Models.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

namespace Forge.OpenAI.Models.FineTuningJob
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/fine-tuning/object
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Models.Common.ResponseBase" />
    public class FineTuningJobResponse : ResponseBase
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
        /// The time when the result was generated.
        /// </summary>
        [JsonIgnore]
        public DateTime Created_At => DateTimeOffset.FromUnixTimeSeconds(CreatedAt).DateTime;

        /// <summary>
        /// The name of the fine-tuned model that is being created. The value will be null if the fine-tuning job is still running.
        /// </summary>
        [JsonPropertyName("fine_tuned_model")]
        public string FineTunedModel { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the fine-tuning job was finished. The value will be null if the
        /// fine-tuning job is still running.
        /// </summary>
        [JsonPropertyName("finished_at")]
        public int? FinishedAt { get; set; }

        /// <summary>
        /// The hyperparameters used for the fine-tuning job. See the fine-tuning guide for more details.
        /// </summary>
        [JsonPropertyName("hyperparameters")]
        public FineTuningJobResponseHyperParams HyperParameters { get; set; }

        /// <summary>
        /// The organization that owns the fine-tuning job.
        /// </summary>
        [JsonPropertyName("organization_id")]
        public string OrganizationId { get; set; }

        /// <summary>
        /// The compiled results file ID(s) for the fine-tuning job. You can retrieve the results with the Files API.
        /// </summary>
        [JsonPropertyName("result_files")]
        public IReadOnlyList<string> ResultFiles { get; set; }

        /// <summary>
        /// The current status of the fine-tuning job, which can be either created, pending, running, succeeded, failed, or
        /// cancelled.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// The total number of billable tokens processed by this fine-tuning job. The value will be null if the fine-tuning job is still running.
        /// </summary>
        [JsonPropertyName("trained_tokens")]
        public int? TrainedTokens { get; set; }

        /// <summary>
        /// The file ID used for training. You can retrieve the training data with the Files API.
        /// </summary>
        [JsonPropertyName("training_file")]
        public string TrainingFileId { get; set; }

        /// <summary>
        /// The file ID used for validation. You can retrieve the validation results with the Files API.
        /// </summary>
        [JsonPropertyName("validation_file")]
        public string ValidationFileId { get; set; }

        /// <summary>
        /// A list of integrations to enable for this fine-tuning job.
        /// https://platform.openai.com/docs/api-reference/fine-tuning/object#fine-tuning/object-integrations
        /// </summary>
        /// <value>
        /// Array or null
        /// </value>
        [JsonPropertyName("integrations")]
        public IReadOnlyList<FineTuningJobIntegration> Integrations { get; set; }

        /// <summary>
        /// The seed used for the fine-tuning job.
        /// </summary>
        [JsonPropertyName("seed")]
        public int Seed { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the fine-tuning job is estimated to finish. The value will be null if the fine-tuning job is not running.
        /// </summary>
        [JsonPropertyName("estimated_finish")]
        public int EstimatedFinish { get; set; }

    }

}
