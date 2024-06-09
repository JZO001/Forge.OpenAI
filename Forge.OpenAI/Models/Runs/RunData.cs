using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Forge.OpenAI.Infrastructure.Serialization;
using Forge.OpenAI.Models.Shared;

namespace Forge.OpenAI.Models.Runs
{

    /// <summary>Represents an execution run on a thread.</summary>
    public class RunData
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
        /// Object type, ie: text_completion, file, fine-tune, list, etc
        /// </summary>
        [JsonPropertyName("object")]
        public string Object { get; set; }

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
        /// The status of the run.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// Details on the action required to continue the run.
        /// Will be null if no action is required.
        /// </summary>
        [JsonPropertyName("required_action")]
        public RequiredAction RequiredAction { get;set ; }

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
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/object#runs/object-incomplete_details">https://platform.openai.com/docs/api-reference/runs/object#runs/object-incomplete_details</a>
        /// </summary>
        /// <value>The incomplete details.</value>
        [JsonPropertyName("incomplete_details")]
        public IncompleteDetails IncompleteDetails { get; set; }

        /// <summary>
        /// The model that the assistant used for this run.
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; }

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
        /// What sampling temperature to use, between 0 and 2. Higher values like 0.8 will make the output more random, while lower values like 0.2 will make it more focused and deterministic.
        /// </summary>
        /// <value>The temperature.</value>
        [JsonPropertyName("temperature")]
        public double? Temperature { get; set; }

        /// <summary>
        /// An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. So 0.1 means only the tokens comprising the top 10% probability mass are considered.
        /// We generally recommend altering this or temperature but not both. <br/>
        /// </summary>
        [JsonPropertyName("top_p")]
        public double? TopP { get; set; }

        /// <summary>
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-max_prompt_tokens">https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-max_prompt_tokens</a>
        /// </summary>
        /// <value>The maximum prompt tokens.</value>
        [JsonPropertyName("max_prompt_tokens")]
        public int? MaxPromptTokens { get; set; }

        /// <summary>
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-max_prompt_tokens">https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-max_prompt_tokens</a>
        /// </summary>
        /// <value>The maximum prompt tokens.</value>
        [JsonPropertyName("max_completion_tokens")]
        public int? MaxCompletionTokens { get; set; }

        /// <summary>
        ///   <para>
        /// Controls for how a thread will be truncated prior to the run. Use this to control the intial context window of the run.</para>
        ///   <para>
        ///     <a href="https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-truncation_strategy">https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-truncation_strategy</a>
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

        /// <summary>
        /// Whether to enable parallel function calling during tool use.
        /// https://platform.openai.com/docs/api-reference/runs/object#runs/object-parallel_tool_calls
        /// https://platform.openai.com/docs/guides/function-calling/parallel-function-calling
        /// </summary>
        [JsonPropertyName("parallel_tool_calls")]
        public bool ParallelToolCalls { get; set; }

        /// <summary>
        ///   <para>
        /// Specifies the format that the model must output. Compatible with GPT-4 Turbo and all GPT-3.5 Turbo models since gpt-3.5-turbo-1106.</para>
        ///   <para>Setting to { "type": "json_object" } enables JSON mode, which guarantees the message the model generates is valid JSON.</para>
        ///   <para>Important: when using JSON mode, you must also instruct the model to produce JSON yourself via a system or user message. Without this, the model may generate an unending stream of whitespace until the generation reaches the token limit, resulting in a long-running and seemingly "stuck" request. Also note that the message content may be partially cut off if finish_reason="length", which indicates the generation exceeded max_tokens or the conversation exceeded the max context length.</para>
        ///   <para>
        ///     <a href="https://platform.openai.com/docs/api-reference/runs/object#runs/object-response_format">https://platform.openai.com/docs/api-reference/runs/object#runs/object-response_format</a>
        ///   </para>
        /// </summary>
        /// <value>The response format.</value>
        [JsonPropertyName("response_format")]
        [JsonConverter(typeof(ResponseFormatConverter))]
        public object ResponseFormat { get; set; }

    }

}
