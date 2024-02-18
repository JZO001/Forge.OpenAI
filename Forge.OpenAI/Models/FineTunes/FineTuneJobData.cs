using Forge.OpenAI.Models.Files;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.FineTunes
{

    /// <summary>Represents a fine tune job</summary>
    [Obsolete]
    public class FineTuneJobData
    {

        /// <summary>Initializes a new instance of the <see cref="FineTuneJobData" /> class.</summary>
        public FineTuneJobData()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="FineTuneJobData" /> class.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="object">The object.</param>
        /// <param name="model">The model.</param>
        /// <param name="createdAtUnixTime">The created at unix time.</param>
        /// <param name="events">The events.</param>
        /// <param name="fineTunedModel">The fine tuned model.</param>
        /// <param name="hyperParams">The hyper parameters.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="resultFiles">The result files.</param>
        /// <param name="status">The status.</param>
        /// <param name="validationFiles">The validation files.</param>
        /// <param name="trainingFiles">The training files.</param>
        /// <param name="updatedAtUnixTime">The updated at unix time.</param>
        public FineTuneJobData(
            string id, 
            string @object, 
            string model, 
            long createdAtUnixTime, 
            IEnumerable<FineTuneJobEvent> events, 
            string fineTunedModel, 
            FineTuneHyperParams hyperParams, 
            string organizationId,
            IEnumerable<FileData> resultFiles, 
            string status,
            IEnumerable<FileData> validationFiles,
            IEnumerable<FileData> trainingFiles, 
            int updatedAtUnixTime)
        {
            Id = id;
            Object = @object;
            Model = model;
            CreatedAtUnixTime = createdAtUnixTime;
            Events = new List<FineTuneJobEvent>(events).AsReadOnly();
            FineTunedModel = fineTunedModel;
            HyperParams = hyperParams;
            OrganizationId = organizationId;
            ResultFiles = new List<FileData>(resultFiles).AsReadOnly();
            Status = status;
            ValidationFiles = new List<FileData>(validationFiles).AsReadOnly();
            TrainingFiles = new List<FileData>(trainingFiles).AsReadOnly();
            UpdatedAtUnixTime = updatedAtUnixTime;
        }

        /// <summary>Gets the fine tune job identifier.</summary>
        /// <value>The identifier.</value>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>Gets the object type.</summary>
        /// <value>The object.</value>
        [JsonPropertyName("object")]
        public string Object { get; set; }

        /// <summary>Gets the model.</summary>
        /// <value>The model.</value>
        [JsonPropertyName("model")]
        public string Model { get; set; }

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

        /// <summary>Performs an implicit conversion from <see cref="FineTuneJobData" /> to <see cref="System.String" />.</summary>
        /// <param name="data">The data.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(FineTuneJobData data) => data?.ToString();

    }

}
