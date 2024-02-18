using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Runs
{

    /// <summary>Details on the tool outputs needed for this run to continue.</summary>
    public class SubmitToolOutputs
    {

        /// <summary>
        /// A list of the relevant tool calls.
        /// </summary>
        [JsonPropertyName("tool_calls")]
        public IReadOnlyList<ToolCall> ToolCalls { get; set; }

    }

}
