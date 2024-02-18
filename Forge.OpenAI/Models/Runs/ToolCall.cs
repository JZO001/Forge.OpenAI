using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Runs
{

    public class ToolCall
    {

        /// <summary>
        /// The ID of the tool call.
        /// This ID must be referenced when you submit the tool outputs in using the Submit tool outputs to run endpoint.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The type of tool call the output is required for.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// The definition of the function that was called.
        /// </summary>
        [JsonPropertyName("function")]
        public FunctionCall FunctionCall { get; set; }

        /// <summary>
        /// The Code Interpreter tool call definition.
        /// </summary>
        [JsonPropertyName("code_interpreter")]
        public CodeInterpreter CodeInterpreter { get; set; }

        /// <summary>
        /// For now, this is always going to be an empty object.
        /// </summary>
        [JsonPropertyName("retrieval")]
        public object Retrieval { get; set; }

    }

}
