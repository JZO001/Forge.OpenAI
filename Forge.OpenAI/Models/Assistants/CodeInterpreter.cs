using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Assistants
{

    /// <summary>
    ///   <a href="https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-tool_resources">https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-tool_resources</a>
    /// </summary>
    public class CodeInterpreter
    {

        /// <summary>
        /// A list of file IDs that the assistant should use.
        /// Useful for tools like 'retrieval' and 'code_interpreter' that can access files.
        /// A maximum of 10 files can be attached to a message.
        /// </summary>
        [JsonPropertyName("file_ids")]
        public IReadOnlyList<string> FileIds { get; set; }

    }

}
