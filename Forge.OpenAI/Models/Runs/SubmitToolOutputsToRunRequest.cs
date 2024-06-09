using Forge.OpenAI.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Forge.OpenAI.Models.Runs
{

    /// <summary>
    /// When a run has the status: "requires_action" and required_action.type is submit_tool_outputs, 
    /// this endpoint can be used to submit the outputs from the tool calls once they're all completed. 
    /// All outputs must be submitted in a single request.
    /// </summary>
    public class SubmitToolOutputsToRunRequest : RequestBase
    {

        /// <summary>The ID of the thread that was run.</summary>
        /// <value>The thread identifier.</value>
        [Required]
        [JsonIgnore]
        public string ThreadId { get; set; }

        /// <summary>The ID of the run to modify.</summary>
        /// <value>The run identifier.</value>
        [Required]
        [JsonIgnore]
        public string RunId { get; set; }

        /// <summary>
        /// Details on the tool outputs needed for this run to continue.
        /// </summary>
        [Required]
        [JsonPropertyName("tool_outputs")]
        public IReadOnlyList<ToolOutput> ToolOutputs { get; set; }

        /// <summary>
        /// If true, returns a stream of events that happen during the Run as server-sent events, terminating when the Run enters a terminal state with a data: [DONE] message.
        /// </summary>
        [JsonPropertyName("stream")]
        public bool? Stream { get; set; }

    }

}
