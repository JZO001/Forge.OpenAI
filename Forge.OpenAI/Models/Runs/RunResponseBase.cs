using Forge.OpenAI.Models.Common;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Forge.OpenAI.Infrastructure.Serialization;
using Forge.OpenAI.Models.Shared;

namespace Forge.OpenAI.Models.Runs
{

    public abstract class RunResponseBase : ResponseBase
    {

        public const string RUN_STATUS_QUEUED = "queued";
        public const string RUN_STATUS_IN_PROGRESS = "in_progress";
        public const string RUN_STATUS_REQUIRES_ACTION = "requires_action";
        public const string RUN_STATUS_CANCELLING = "cancelling";
        public const string RUN_STATUS_CANCELLED = "cancelled";
        public const string RUN_STATUS_FAILED = "failed";
        public const string RUN_STATUS_COMPLETED = "completed";
        public const string RUN_STATUS_EXPIRED = "expired";

        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The time when the result was generated in unix epoch format
        /// </summary>
        [JsonPropertyName("created_at")]
        public long? CreatedAtUnixTime { get; set; }

        /// <summary>
        /// The time when the result was generated.
        /// </summary>
        [JsonIgnore]
        public DateTime? CreatedAt => CreatedAtUnixTime.HasValue ? (DateTime?)DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTime.Value).DateTime : null;

        /// <summary>
        /// The thread ID that this run belongs to.
        /// </summary>
        [JsonPropertyName("thread_id")]
        public string ThreadId { get; set; }

        /// <summary>
        /// The ID of the assistant used for execution of this run.
        /// </summary>
        [JsonPropertyName("assistant_id")]
        public string AssistantId { get; set; }

        /// <summary>
        /// The status of the run. For the bpossible values, <see cref="RunData"/>
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// Details on the action required to continue the run.
        /// Will be null if no action is required.
        /// </summary>
        [JsonPropertyName("required_action")]
        public RequiredAction RequiredAction { get; set; }

        /// <summary>
        /// The Last error Associated with this run.
        /// Will be null if there are no errors.
        /// </summary>
        [JsonPropertyName("last_error")]
        public Error LastError { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run will expire.
        /// </summary>
        [JsonPropertyName("expires_at")]
        public long? ExpiresAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run will expire.
        /// </summary>
        [JsonIgnore]
        public DateTime? ExpiresAt => ExpiresAtUnixTimeSeconds.HasValue ? (DateTime?)DateTimeOffset.FromUnixTimeSeconds(ExpiresAtUnixTimeSeconds.Value).DateTime : null;

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was started.
        /// </summary>
        [JsonPropertyName("started_at")]
        public int? StartedAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was started.
        /// </summary>
        [JsonIgnore]
        public DateTime? StartedAt => StartedAtUnixTimeSeconds.HasValue ? (DateTime?)DateTimeOffset.FromUnixTimeSeconds(StartedAtUnixTimeSeconds.Value).DateTime : null;

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was cancelled.
        /// </summary>
        [JsonPropertyName("cancelled_at")]
        public int? CancelledAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was cancelled.
        /// </summary>
        [JsonIgnore]
        public DateTime? CancelledAt => CancelledAtUnixTimeSeconds.HasValue ? (DateTime?)DateTimeOffset.FromUnixTimeSeconds(CancelledAtUnixTimeSeconds.Value).DateTime : null;

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run failed.
        /// </summary>
        [JsonPropertyName("failed_at")]
        public int? FailedAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run failed.
        /// </summary>
        [JsonIgnore]
        public DateTime? FailedAt => FailedAtUnixTimeSeconds.HasValue ? (DateTime?)DateTimeOffset.FromUnixTimeSeconds(FailedAtUnixTimeSeconds.Value).DateTime : null;

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was completed.
        /// </summary>
        [JsonPropertyName("completed_at")]
        public int? CompletedAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was completed.
        /// </summary>
        [JsonIgnore]
        public DateTime? CompletedAt => CompletedAtUnixTimeSeconds.HasValue ? (DateTime?)DateTimeOffset.FromUnixTimeSeconds(CompletedAtUnixTimeSeconds.Value).DateTime : null;

        /// <summary>
        /// The instructions that the assistant used for this run.
        /// </summary>
        [JsonPropertyName("instructions")]
        public string Instructions { get; set; }

        /// <summary>
        /// The list of tools that the assistant used for this run.
        /// </summary>
        [JsonPropertyName("tools")]
        public IReadOnlyList<Tool> Tools { get; set; }

        /// <summary>
        /// The list of File IDs the assistant used for this run.
        /// </summary>
        [Obsolete]
        [JsonPropertyName("file_ids")]
        public IReadOnlyList<string> FileIds { get; set; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        [JsonPropertyName("metadata")]
        public IReadOnlyDictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// Usage statistics related to the run. This value will be `null` if the run is not in a terminal state (i.e. `in_progress`, `queued`, etc.).
        /// </summary>
        [JsonPropertyName("usage")]
        public Usage Usage { get; set; }

        /// <summary>
        /// The sampling temperature used for this run. If not set, defaults to 1.
        /// <see href="https://platform.openai.com/docs/api-reference/runs/object#runs/object-temperature" />
        /// </summary>
        /// <value>The temperature.</value>
        [JsonPropertyName("temperature")]
        public double? Temperature { get; set; }

        /// <summary>
        /// The nucleus sampling value used for this run. If not set, defaults to 1.
        /// <see href="https://platform.openai.com/docs/api-reference/runs/object#runs/object-top_p" />
        /// </summary>
        [JsonPropertyName("top_p")]
        public double? TopP { get; set; }

        /// <summary>
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/object#runs/object-max_prompt_tokens">https://platform.openai.com/docs/api-reference/runs/object#runs/object-max_prompt_tokens</a>
        /// </summary>
        /// <value>The maximum prompt tokens.</value>
        [JsonPropertyName("max_prompt_tokens")]
        public int? MaxPromptTokens { get; set; }

        /// <summary>
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/object#runs/object-max_completion_tokens">https://platform.openai.com/docs/api-reference/runs/object#runs/object-max_completion_tokens</a>
        /// </summary>
        /// <value>The maximum prompt tokens.</value>
        [JsonPropertyName("max_completion_tokens")]
        public int? MaxCompletionTokens { get; set; }

        /// <summary>
        /// Controls for how a thread will be truncated prior to the run. Use this to control the intial context window of the run.
        ///   <para>
        ///     <a href="https://platform.openai.com/docs/api-reference/runs/object#runs/object-truncation_strategy">https://platform.openai.com/docs/api-reference/runs/object#runs/object-truncation_strategy</a>
        ///   </para>
        /// </summary>
        /// <value>The truncation strategy.</value>
        [JsonPropertyName("truncation_strategy")]
        public TruncationStrategy TruncationStrategy { get; set; }

        /// <summary>
        ///   <para>
        /// Controls which (if any) tool is called by the model. none means the model will not call any tools and instead generates a message. auto is the default value and means the model can pick between generating a message or calling a tool. Specifying a particular tool like {"type": "file_search"} or {"type": "function", "function": {"name": "my_function"}} forces the model to call that tool.</para>
        ///   <para>
        ///     <a href="https://platform.openai.com/docs/api-reference/runs/object#runs/object-tool_choice">https://platform.openai.com/docs/api-reference/runs/object#runs/object-tool_choice</a>
        ///   </para>
        /// </summary>
        /// <value>The tool choice.</value>
        [JsonPropertyName("tool_choice")]
        [JsonConverter(typeof(ToolChoiceConverter))]
        public object ToolChoice { get; set; }

    }

}
