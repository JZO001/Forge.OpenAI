using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Runs
{

    public class ToolOutput
    {

        /// <summary>
        /// The ID of the tool call in the <see cref="RequiredAction"/> within the <see cref="RunResponse"/> the output is being submitted for.
        /// </summary>
        [JsonPropertyName("tool_call_id")]
        public string ToolCallId { get; set; }

        /// <summary>
        /// The output of the tool call to be submitted to continue the run.
        /// </summary>
        [JsonPropertyName("output")]
        public string Output { get; set; }

    }

}
