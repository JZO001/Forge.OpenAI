using Forge.OpenAI.Models.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System;
using Forge.OpenAI.Models.Shared;

namespace Forge.OpenAI.Models.Runs
{

    /// <summary>Create a run.</summary>
    public class CreateRunRequest : RequestBase
    {

        /// <summary>Initializes a new instance of the <see cref="CreateRunRequest" /> class.</summary>
        public CreateRunRequest()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CreateRunRequest" /> class.</summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="assistantId">The assistant identifier.</param>
        public CreateRunRequest(string threadId, string assistantId)
        {
            if (string.IsNullOrWhiteSpace(threadId)) throw new ArgumentNullException(nameof(threadId));
            if (string.IsNullOrWhiteSpace(assistantId)) throw new ArgumentNullException(nameof(assistantId));

            ThreadId = threadId;
            AssistantId = assistantId;
        }

        /// <summary>The ID of the thread to create a message for.</summary>
        /// <value>The thread identifier.</value>
        [Required]
        [JsonIgnore]
        public string ThreadId { get; set; }

        /// <summary>
        /// The ID of the assistant used for execution of this run.
        /// </summary>
        [Required]
        [JsonPropertyName("assistant_id")]
        public string AssistantId { get; set; }

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
        /// Appends additional instructions at the end of the instructions for the run. 
        /// This is useful for modifying the behavior on a per-run basis without overriding other instructions.
        /// </summary>
        [JsonPropertyName("additional_instructions")]
        public string AdditionalInstructions { get; set; }

        /// <summary>
        /// A list of messages to start the thread with.
        /// </summary>
        [JsonPropertyName("additional_messages")]
        public IList<Message> AdditionalMessages { get; set; }

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
        /// What sampling temperature to use, between 0 and 2. Higher values like 0.8 will make the output more random, while lower values like 0.2 will make it more focused and deterministic.
        /// <see href="https://platform.openai.com/docs/api-reference/runs/createRun#runs-createrun-temperature" />
        /// </summary>
        /// <value>The temperature.</value>
        [JsonPropertyName("temperature")]
        public double? Temperature { get; set; }

        /// <summary>
        /// An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. So 0.1 means only the tokens comprising the top 10% probability mass are considered.
        /// We generally recommend altering this or temperature but not both. <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/runs/createRun#runs-createrun-top_p" />
        /// </summary>
        [JsonPropertyName("top_p")]
        public double? TopP { get; set; }

        /// <summary>If true, returns a stream of events that happen during the Run as server-sent events, terminating when the Run enters a terminal state with a data: [DONE] message.</summary>
        /// <value>The stream.</value>
        [JsonPropertyName("stream")]
        public bool? Stream { get; set; }

        /// <summary>
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/createRun#runs-createrun-max_prompt_tokens">https://platform.openai.com/docs/api-reference/runs/createRun#runs-createrun-max_prompt_tokens</a>
        /// </summary>
        /// <value>The maximum prompt tokens.</value>
        [JsonPropertyName("max_prompt_tokens")]
        public int? MaxPromptTokens { get; set; }

        /// <summary>
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/createRun#runs-createrun-max_completion_tokens">https://platform.openai.com/docs/api-reference/runs/createRun#runs-createrun-max_completion_tokens</a>
        /// </summary>
        /// <value>The maximum prompt tokens.</value>
        [JsonPropertyName("max_completion_tokens")]
        public int? MaxCompletionTokens { get; set; }

        /// <summary>Gets or sets the response format.</summary>
        /// <value>
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/createRun#runs-createrun-response_format"></a>
        /// </value>
        [JsonPropertyName("response_format")]
        public ResponseFormat ResponseFormat { get; set; }

        /// <summary>Sets the set response format with enum.</summary>
        /// <value>The set response format with enum.</value>
        [JsonIgnore]
        public ResponseFormats? SetResponseFormatWithEnum
        {
            set
            {
                if (value is null)
                {
                    ResponseFormat = null;
                    return;
                }

                ResponseFormat = new ResponseFormat
                {
                    Type = value == ResponseFormats.Json ? ResponseFormat.RESPONSE_FORMAT_JSON : ResponseFormat.RESPONSE_FORMAT_TEXT
                };
            }
        }

        /// <summary>
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/createRun#runs-createrun-tool_choice">https://platform.openai.com/docs/api-reference/runs/createRun#runs-createrun-tool_choice</a>
        /// </summary>
        /// <value>The tool choice.</value>
        [JsonPropertyName("tool_choice")]
        public object ToolChoice { get; set; }

        /// <summary>
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/createRun#runs-createrun-tool_choice">https://platform.openai.com/docs/api-reference/runs/createRun#runs-createrun-tool_choice</a>
        /// </summary>
        /// <value>The tool choice.</value>
        [JsonIgnore]
        public ToolChoice ToolChoiceAsObject
        {
            get => ToolChoice as ToolChoice;
            set => ToolChoice = value;
        }

        /// <summary>
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/createRun#runs-createrun-tool_choice">https://platform.openai.com/docs/api-reference/runs/createRun#runs-createrun-tool_choice</a>
        /// </summary>
        /// <value>The tool choice.</value>
        [JsonIgnore]
        public string ToolChoiceAsString
        {
            get => ToolChoice as string;
            set => ToolChoice = value;
        }

        /// <summary>
        ///   <para>
        /// Controls for how a thread will be truncated prior to the run. Use this to control the intial context window of the run.</para>
        ///   <para>
        ///     <a href="https://platform.openai.com/docs/api-reference/runs/createRun#runs-createrun-truncation_strategy">https://platform.openai.com/docs/api-reference/runs/createRun#runs-createrun-truncation_strategy</a>
        ///   </para>
        /// </summary>
        /// <value>The truncation strategy.</value>
        [JsonPropertyName("truncation_strategy")]
        public TruncationStrategy TruncationStrategy { get; set; }

    }

}
