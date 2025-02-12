using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Runs;
using Forge.OpenAI.Models.Shared;
using System;
using System.Collections.Generic;

namespace Forge.OpenAI.Interfaces.Models
{

    public interface IRunData
    {

        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Object type, ie: text_completion, file, fine-tune, list, etc
        /// </summary>
        string Object { get; }

        /// <summary>
        /// The time when the result was generated in unix epoch format
        /// </summary>
        int? CreatedAtUnixTime { get; }

        /// <summary>
        /// The time when the result was generated.
        /// </summary>
        DateTime? CreatedAt { get; }

        /// <summary>
        /// The thread ID that this run belongs to.
        /// </summary>
        string ThreadId { get; }

        /// <summary>
        /// The ID of the assistant used for execution of this run.
        /// </summary>
        string AssistantId { get; }

        /// <summary>
        /// The status of the run.
        /// </summary>
        string Status { get; }

        /// <summary>
        /// Details on the action required to continue the run.
        /// Will be null if no action is required.
        /// </summary>
        RequiredAction RequiredAction { get; }

        /// <summary>
        /// The Last error Associated with this run.
        /// Will be null if there are no errors.
        /// </summary>
        OpenAI.Models.Runs.Error LastError { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run will expire.
        /// </summary>
        int? ExpiresAtUnixTimeSeconds { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run will expire.
        /// </summary>
        DateTime? ExpiresAt { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was started.
        /// </summary>
        int? StartedAtUnixTimeSeconds { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was started.
        /// </summary>
        DateTime? StartedAt { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was cancelled.
        /// </summary>
        int? CancelledAtUnixTimeSeconds { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was cancelled.
        /// </summary>
        DateTime? CancelledAt { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run failed.
        /// </summary>
        int? FailedAtUnixTimeSeconds { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run failed.
        /// </summary>
        DateTime? FailedAt { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was completed.
        /// </summary>
        int? CompletedAtUnixTimeSeconds { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was completed.
        /// </summary>
        DateTime? CompletedAt { get; }

        /// <summary>
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/object#runs/object-incomplete_details">https://platform.openai.com/docs/api-reference/runs/object#runs/object-incomplete_details</a>
        /// </summary>
        /// <value>The incomplete details.</value>
        IncompleteDetails IncompleteDetails { get; }

        /// <summary>
        /// The model that the assistant used for this run.
        /// </summary>
        string Model
        {
            get;
        }

        /// <summary>
        /// The instructions that the assistant used for this run.
        /// </summary>
        string Instructions
        {
            get;
        }

        /// <summary>
        /// The list of tools that the assistant used for this run.
        /// </summary>
        IReadOnlyList<Tool> Tools
        {
            get;
        }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        IReadOnlyDictionary<string, string> Metadata
        {
            get;
        }

        /// <summary>
        /// Usage statistics related to the run. This value will be `null` if the run is not in a terminal state (i.e. `in_progress`, `queued`, etc.).
        /// </summary>
        Usage Usage
        {
            get;
        }

        /// <summary>
        /// What sampling temperature to use, between 0 and 2. Higher values like 0.8 will make the output more random, while lower values like 0.2 will make it more focused and deterministic.
        /// </summary>
        /// <value>The temperature.</value>
        double? Temperature
        {
            get;
        }

        /// <summary>
        /// An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. So 0.1 means only the tokens comprising the top 10% probability mass are considered.
        /// We generally recommend altering this or temperature but not both. <br/>
        /// </summary>
        double? TopP
        {
            get;
        }

        /// <summary>
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-max_prompt_tokens">https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-max_prompt_tokens</a>
        /// </summary>
        /// <value>The maximum prompt tokens.</value>
        int? MaxPromptTokens
        {
            get;
        }

        /// <summary>
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-max_prompt_tokens">https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-max_prompt_tokens</a>
        /// </summary>
        /// <value>The maximum prompt tokens.</value>
        int? MaxCompletionTokens
        {
            get;
        }

        /// <summary>
        ///   <para>
        /// Controls for how a thread will be truncated prior to the run. Use this to control the intial context window of the run.</para>
        ///   <para>
        ///     <a href="https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-truncation_strategy">https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-truncation_strategy</a>
        ///   </para>
        /// </summary>
        /// <value>The truncation strategy.</value>
        TruncationStrategy TruncationStrategy
        {
            get;
        }

        /// <summary>
        ///   <para>
        /// Controls which (if any) tool is called by the model. none means the model will not call any tools and instead generates a message. auto is the default value and means the model can pick between generating a message or calling a tool. Specifying a particular tool like {"type": "file_search"} or {"type": "function", "function": {"name": "my_function"}} forces the model to call that tool.</para>
        ///   <para>
        ///     <a href="https://platform.openai.com/docs/api-reference/runs/object#runs/object-tool_choice">https://platform.openai.com/docs/api-reference/runs/object#runs/object-tool_choice</a>
        ///   </para>
        /// </summary>
        /// <value>The tool choice.</value>
        object ToolChoice { get; }

        /// <summary>
        /// Whether to enable parallel function calling during tool use.
        /// https://platform.openai.com/docs/api-reference/runs/object#runs/object-parallel_tool_calls
        /// https://platform.openai.com/docs/guides/function-calling/parallel-function-calling
        /// </summary>
        bool ParallelToolCalls
        {
            get;
        }

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
        object ResponseFormat { get; }

    }

}
