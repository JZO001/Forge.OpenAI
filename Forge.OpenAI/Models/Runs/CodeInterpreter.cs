using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Runs
{

    /// <summary>
    ///   <a href="https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-tool_resources">https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-tool_resources</a>
    /// </summary>
    public class CodeInterpreter
    {

        /// <summary>
        /// The input to the Code Interpreter tool call.
        /// </summary>
        [JsonPropertyName("input")]
        public string Input { get; set; }

        /// <summary>
        /// The outputs from the Code Interpreter tool call.
        /// Code Interpreter can output one or more items, including text (logs) or images (image).
        /// Each of these are represented by a different object type.
        /// </summary>
        [JsonPropertyName("outputs")]
        public IReadOnlyList<CodeInterpreterOutputs> Outputs { get; set; }

        /// <summary>
        /// A list of file IDs that the assistant should use.
        /// Useful for tools like 'retrieval' and 'code_interpreter' that can access files.
        /// A maximum of 10 files can be attached to a message.
        /// </summary>
        [JsonPropertyName("file_ids")]
        public IReadOnlyList<string> FileIds { get; set; }

    }

}
