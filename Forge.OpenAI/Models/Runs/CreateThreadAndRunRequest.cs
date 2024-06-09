using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Threads;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Forge.OpenAI.Models.Shared;

namespace Forge.OpenAI.Models.Runs
{

    /// <summary>Create a thread and run it in one request.</summary>
    public class CreateThreadAndRunRequest : RequestBase
    {

        /// <summary>
        /// The ID of the assistant to use to execute this run.
        /// </summary>
        [Required]
        [JsonPropertyName("assistant_id")]
        public string AssistantId { get; set; }

        /// <summary>Gets or sets the thread data for the creation</summary>
        /// <value>The thread.</value>
        [JsonPropertyName("thread")]
        public CreateThreadRequest Thread { get; set; }

        /// <summary>
        /// The ID of the Model to be used to execute this run.
        /// If a value is provided here, it will override the model associated with the assistant.
        /// If not, the model associated with the assistant will be used.
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; }

        /// <summary>
        /// Override the default system message of the assistant.
        /// This is useful for modifying the behavior on a per-run basis.
        /// </summary>
        [JsonPropertyName("instructions")]
        public string Instructions { get; set; }

        /// <summary>
        /// Override the tools the assistant can use for this run.
        /// This is useful for modifying the behavior on a per-run basis.
        /// </summary>
        [JsonPropertyName("tools")]
        public IReadOnlyList<Tool> Tools { get; set; }

        /// <summary>
        /// A set of resources that are used by the assistant's tools. The resources are specific to the type of tool. For example, the code_interpreter tool requires a list of file IDs, while the file_search tool requires a list of vector store IDs.
        /// <a href="https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-tool_resources</a>
        /// </summary>
        /// <value>The tool resources.</value>
        [JsonPropertyName("tool_resources")]
        public ToolResource ToolResources { get; set; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        [JsonPropertyName("metadata")]
        public IReadOnlyDictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// What sampling temperature to use, between 0 and 2. Higher values like 0.8 will make the output more random, while lower values like 0.2 will make it more focused and deterministic.
        /// <see href="https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-temperature" />
        /// </summary>
        /// <value>The temperature.</value>
        [JsonPropertyName("temperature")]
        public double? Temperature { get; set; }

        /// <summary>
        /// An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. So 0.1 means only the tokens comprising the top 10% probability mass are considered.
        /// We generally recommend altering this or temperature but not both. <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-top_p" />
        /// </summary>
        [JsonPropertyName("top_p")]
        public double? TopP { get; set; }

        /// <summary>
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-stream">https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-stream</a>
        /// </summary>
        /// <value>The stream.</value>
        [JsonPropertyName("stream")]
        public bool? Stream { get; set; }

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
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-tool_choice">https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-tool_choice</a>
        /// </summary>
        /// <value>The tool choice.</value>
        [JsonPropertyName("tool_choice")]
        public object ToolChoice { get; set; }

        /// <summary>
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-tool_choice">https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-tool_choice</a>
        /// </summary>
        /// <value>The tool choice.</value>
        [JsonIgnore]
        public ToolChoice ToolChoiceAsObject
        {
            get => ToolChoice as ToolChoice;
            set => ToolChoice = value;
        }

        /// <summary>
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-tool_choice">https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-tool_choice</a>
        /// </summary>
        /// <value>The tool choice.</value>
        [JsonIgnore]
        public string ToolChoiceAsString
        {
            get => ToolChoice as string;
            set => ToolChoice = value;
        }

        /// <summary>
        /// Whether to enable parallel function calling during tool use.
        /// https://platform.openai.com/docs/api-reference/runs/object#runs/object-parallel_tool_calls
        /// https://platform.openai.com/docs/guides/function-calling/parallel-function-calling
        /// </summary>
        [JsonPropertyName("parallel_tool_calls")]
        public bool ParallelToolCalls { get; set; }

        /// <summary>Gets or sets the response format.</summary>
        /// <value>
        ///   <a href="https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-response_format"></a>
        /// </value>
        [JsonPropertyName("response_format")]
        public object ResponseFormat { get; private set; }

        /// <summary>Gets or sets the response format as string.</summary>
        /// <value>The response format as string.</value>
        [JsonIgnore]
        public string ResponseFormatAsString { get => ResponseFormat as string; set => ResponseFormat = value; }

        /// <summary>Gets or sets the response format as object.</summary>
        /// <value>The response format as object.</value>
        [JsonIgnore]
        public ResponseFormat ResponseFormatAsObject { get => ResponseFormat as ResponseFormat; set => ResponseFormat = value; }

    }

}
