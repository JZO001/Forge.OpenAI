using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Files;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.FineTunes
{

    /// <summary>Represents a standard response with fine tune job data</summary>
    [Obsolete]
    public abstract class FineTuneResponseBase : ResponseBase
    {

        /// <summary>Initializes a new instance of the <see cref="FineTuneResponseBase" /> class.</summary>
        protected FineTuneResponseBase()
        {
        }

        /// <summary>Gets the fine tune job identifier.</summary>
        /// <value>The identifier.</value>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>Gets the created at unix time.</summary>
        /// <value>The created at unix time.</value>
        [JsonPropertyName("created_at")]
        public long CreatedAtUnixTime { get; set; }

        /// <summary>Gets the created at.</summary>
        /// <value>The created at.</value>
        [JsonIgnore]
        public DateTime CreatedAt => DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTime).DateTime;

        /// <summary>Gets the events.</summary>
        /// <value>The events.</value>
        [JsonPropertyName("events")]
        public IReadOnlyList<FineTuneJobEvent> Events { get; set; }

        /// <summary>Gets the fine tuned model.</summary>
        /// <value>The fine tuned model.</value>
        [JsonPropertyName("fine_tuned_model")]
        public string FineTunedModel { get; set; }

        /// <summary>Gets the hyper parameters.</summary>
        /// <value>The hyper parameters.</value>
        [JsonPropertyName("hyperparams")]
        public FineTuneHyperParams HyperParams { get; set; }

        /// <summary>Gets the organization identifier.</summary>
        /// <value>The organization identifier.</value>
        [JsonPropertyName("organization_id")]
        public string OrganizationId { get; set; }

        /// <summary>Gets the result files.</summary>
        /// <value>The result files.</value>
        [JsonPropertyName("result_files")]
        public IReadOnlyList<FileData> ResultFiles { get; set; }

        /// <summary>Gets the status.</summary>
        /// <value>The status.</value>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>Gets the validation files.</summary>
        /// <value>The validation files.</value>
        [JsonPropertyName("validation_files")]
        public IReadOnlyList<FileData> ValidationFiles { get; set; }

        /// <summary>Gets the training files.</summary>
        /// <value>The training files.</value>
        [JsonPropertyName("training_files")]
        public IReadOnlyList<FileData> TrainingFiles { get; set; }

        /// <summary>Gets the updated at unix time.</summary>
        /// <value>The updated at unix time.</value>
        [JsonPropertyName("updated_at")]
        public int UpdatedAtUnixTime { get; set; }

        /// <summary>Gets the updated at.</summary>
        /// <value>The updated at.</value>
        [JsonIgnore]
        public DateTime UpdatedAt => DateTimeOffset.FromUnixTimeSeconds(UpdatedAtUnixTime).DateTime;

    }

}
