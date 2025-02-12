using Forge.OpenAI.Interfaces.Models;
using Forge.OpenAI.Models.Common;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Batch
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/batch/create
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Models.Common.ResponseBase" />
    public abstract class BatchResponseBase : ResponseBase, IBatchData
    {

        /// <summary>
        /// Id of the batch.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The OpenAI API endpoint used by the batch.
        /// </summary>
        [JsonPropertyName("endpoint")]
        public string Endpoint { get; set; }

        /// <summary>
        /// https://platform.openai.com/docs/api-reference/batch/object#batch/object-errors
        /// </summary>
        [JsonPropertyName("errors")]
        public IReadOnlyList<BatchError> Errors { get; set; }

        /// <summary>
        /// The ID of the input file for the batch.
        /// </summary>
        [JsonPropertyName("input_file_id")]
        public string InputFileId { get; set; }

        /// <summary>
        /// The time frame within which the batch should be processed.
        /// </summary>
        [JsonPropertyName("completion_window")]
        public string CompletionWindow { get; set; }

        /// <summary>
        /// The current status of the batch.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// The ID of the file containing the outputs of successfully executed requests.
        /// </summary>
        [JsonPropertyName("output_file_id")]
        public string OutputFileId { get; set; }

        /// <summary>
        /// The ID of the file containing the outputs of requests with errors.
        /// </summary>
        [JsonPropertyName("error_file_id")]
        public string ErrorFileId { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public int CreatedAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The time when the result was generated.
        /// </summary>
        [JsonIgnore]
        public DateTime CreatedAt => DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTimeSeconds).DateTime;

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch started processing.
        /// </summary>
        [JsonPropertyName("in_progress_at")]
        public int? InProgressAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch started processing.
        /// </summary>
        [JsonIgnore]
        public DateTime? InProgressAt => InProgressAtUnixTimeSeconds.HasValue ? (DateTime?)(DateTimeOffset.FromUnixTimeSeconds(InProgressAtUnixTimeSeconds.Value).DateTime) : null;

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch will expire.
        /// </summary>
        [JsonPropertyName("expires_at")]
        public int? ExpiresAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch will expire.
        /// </summary>
        [JsonIgnore]
        public DateTime? ExpiresAt => ExpiresAtUnixTimeSeconds.HasValue ? (DateTime?)(DateTimeOffset.FromUnixTimeSeconds(ExpiresAtUnixTimeSeconds.Value).DateTime) : null;

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch started finalizing.
        /// </summary>
        [JsonPropertyName("finalizing_at")]
        public int? FinalizingAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch started finalizing.
        /// </summary>
        [JsonIgnore]
        public DateTime? FinalizingAt => FinalizingAtUnixTimeSeconds.HasValue ? (DateTime?)(DateTimeOffset.FromUnixTimeSeconds(FinalizingAtUnixTimeSeconds.Value).DateTime) : null;

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch was completed.
        /// </summary>
        [JsonPropertyName("completed_at")]
        public int? CompletedAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch was completed.
        /// </summary>
        [JsonIgnore]
        public DateTime? CompletedAt => CompletedAtUnixTimeSeconds.HasValue ? (DateTime?)(DateTimeOffset.FromUnixTimeSeconds(CompletedAtUnixTimeSeconds.Value).DateTime) : null;

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch failed.
        /// </summary>
        [JsonPropertyName("failed_at")]
        public int? FailedAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch failed.
        /// </summary>
        [JsonIgnore]
        public DateTime? FailedAt => FailedAtUnixTimeSeconds.HasValue ? (DateTime?)(DateTimeOffset.FromUnixTimeSeconds(FailedAtUnixTimeSeconds.Value).DateTime) : null;

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch expired.
        /// </summary>
        [JsonPropertyName("expired_at")]
        public int? ExpiredAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch expired.
        /// </summary>
        [JsonIgnore]
        public DateTime? ExpiredAt => ExpiredAtUnixTimeSeconds.HasValue ? (DateTime?)(DateTimeOffset.FromUnixTimeSeconds(ExpiredAtUnixTimeSeconds.Value).DateTime) : null;

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch started cancelling.
        /// </summary>
        [JsonPropertyName("cancelling_at")]
        public int? CancellingAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch started cancelling.
        /// </summary>
        [JsonIgnore]
        public DateTime? CancellingAt => CancellingAtUnixTimeSeconds.HasValue ? (DateTime?)(DateTimeOffset.FromUnixTimeSeconds(CancellingAtUnixTimeSeconds.Value).DateTime) : null;

        /// <summary>
        /// The request counts for different statuses within the batch.
        /// </summary>
        [JsonPropertyName("request_counts")]
        public BatchRequestCount RequestCounts { get; set; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object. 
        /// This can be useful for storing additional information about the object in a structured format. 
        /// Keys can be a maximum of 64 characters long and values can be a maxium of 512 characters long.
        /// </summary>
        [JsonPropertyName("metadata")]
        public IReadOnlyDictionary<string, string> Metadata { get; set; }

    }

}
