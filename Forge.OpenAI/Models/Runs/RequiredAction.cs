using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Runs
{

    public class RequiredAction
    {

        /// <summary>For now, this is always submit_tool_outputs</summary>
        /// <value>The type.</value>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Details on the tool outputs needed for this run to continue.
        /// </summary>
        [JsonPropertyName("submit_tool_outputs")]
        public SubmitToolOutputs SubmitToolOutputs { get; set; }

    }

}
