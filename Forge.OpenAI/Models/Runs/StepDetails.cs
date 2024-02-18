using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Runs
{

    /// <summary>The details of the run step.</summary>
    public class StepDetails
    {

        /// <summary>
        /// Always message_creation.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Details of the message creation by the run step.
        /// </summary>
        [JsonPropertyName("message_creation")]
        public RunStepMessageCreation MessageCreation { get; set; }

        /// <summary>
        /// An array of tool calls the run step was involved in.
        /// These can be associated with one of three types of tools: 'code_interpreter', 'retrieval', or 'function'.
        /// </summary>
        [JsonPropertyName("tool_calls")]
        public IReadOnlyList<ToolCall> ToolCalls { get; set; }

    }

}
