using System.Text.Json.Serialization;
using Forge.OpenAI.Models.Messages;

namespace Forge.OpenAI.Models.Shared
{

    public class CodeInterpreterOutputs
    {

        public const string CODE_INTERPRETER_OUTPUT_TYPE_LOGS = "logs";
        public const string CODE_INTERPRETER_OUTPUT_TYPE_IMAGE = "image";

        /// <summary>
        /// Output type. Can be either 'logs' or 'image'.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Text output from the Code Interpreter tool call as part of a run step.
        /// </summary>
        [JsonPropertyName("logs")]
        public string Logs { get; set; }

        /// <summary>
        /// Code interpreter image output.
        /// </summary>
        [JsonPropertyName("image")]
        public ImageFile Image { get; set; }

    }

}
