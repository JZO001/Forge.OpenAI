using Forge.OpenAI.Interfaces.Models;
using Forge.OpenAI.Models.Common;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Runs
{

    /// <summary>
    ///   <a href="https://platform.openai.com/docs/api-reference/runs/step-object">https://platform.openai.com/docs/api-reference/runs/step-object</a>
    /// </summary>
    public class RunStepResponse : ResponseBase, IRunStepData
    {

        public const string RUN_STEP_TYPE_MESSAGE_CREATION = "message_creation";
        public const string RUN_STEP_TYPE_MESSAGE_TOOL_CALLS = "tool_calls";

        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The time when the result was generated in unix epoch format
        /// </summary>
        [JsonPropertyName("created_at")]
        public int? CreatedAtUnixTime { get; set; }

        /// <summary>
        /// The time when the result was generated.
        /// </summary>
        [JsonIgnore]
        public DateTime? CreatedAt => CreatedAtUnixTime.HasValue ? (DateTime?)DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTime.Value).DateTime : null;

        /// <summary>
        /// The ID of the assistant used for execution of this run.
        /// </summary>
        [JsonPropertyName("assistant_id")]
        public string AssistantId { get; set; }

        /// <summary>
        /// The thread ID that this run belongs to.
        /// </summary>
        [JsonPropertyName("thread_id")]
        public string ThreadId { get; set; }

        /// <summary>
        /// The ID of the run that this run step is a part of.
        /// </summary>
        [JsonPropertyName("run_id")]
        public string RunId { get; set; }

        /// <summary>
        /// The type of run step.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// The status of the run step. For possibel values, see consts in RunData.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// The details of the run step.
        /// </summary>
        [JsonPropertyName("step_details")]
        public StepDetails StepDetails { get; set; }

        /// <summary>
        /// The last error associated with this run step. Will be null if there are no errors.
        /// </summary>
        [JsonPropertyName("last_error")]
        public Error LastError { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step expired. A step is considered expired if the parent run is expired.
        /// </summary>
        [JsonPropertyName("expires_at")]
        public int? ExpiresAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step expired. A step is considered expired if the parent run is expired.
        /// </summary>
        [JsonIgnore]
        public DateTime? ExpiresAt => ExpiresAtUnixTimeSeconds.HasValue ? (DateTime?)DateTimeOffset.FromUnixTimeSeconds(ExpiresAtUnixTimeSeconds.Value).DateTime : null;

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step was cancelled.
        /// </summary>
        [JsonPropertyName("cancelled_at")]
        public int? CancelledAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step was cancelled.
        /// </summary>
        [JsonIgnore]
        public DateTime? CancelledAt => CancelledAtUnixTimeSeconds.HasValue ? (DateTime?)DateTimeOffset.FromUnixTimeSeconds(CancelledAtUnixTimeSeconds.Value).DateTime : null;

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step failed.
        /// </summary>
        [JsonPropertyName("failed_at")]
        public int? FailedAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step failed.
        /// </summary>
        [JsonIgnore]
        public DateTime? FailedAt => FailedAtUnixTimeSeconds.HasValue ? (DateTime?)DateTimeOffset.FromUnixTimeSeconds(FailedAtUnixTimeSeconds.Value).DateTime : null;

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step completed.
        /// </summary>
        [JsonPropertyName("completed_at")]
        public int? CompletedAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step completed.
        /// </summary>
        [JsonIgnore]
        public DateTime? CompletedAt => CompletedAtUnixTimeSeconds.HasValue ? (DateTime?)DateTimeOffset.FromUnixTimeSeconds(CompletedAtUnixTimeSeconds.Value).DateTime : null;

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        [JsonPropertyName("metadata")]
        public IReadOnlyDictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// Usage statistics related to the run step. This value will be `null` while the run step's status is `in_progress`.
        /// </summary>
        [JsonPropertyName("usage")]
        public Usage Usage { get; set; }

    }

}
