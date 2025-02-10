using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Shared
{

    /// <summary>
    ///   <a href="https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-tool_resources">https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-tool_resources</a>
    /// </summary>
    public class ToolResource
    {

        /// <summary>
        /// The code_interpreter tool requires a list of file IDs
        /// </summary>
        /// <value>The code interpreter.</value>
        [JsonPropertyName("code_interpreter")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CodeInterpreter CodeInterpreter { get; set; }

        /// <summary>
        /// The file_search tool requires a list of vector store IDs.
        /// </summary>
        /// <value>The file search.</value>
        [JsonPropertyName("file_search")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public FileSearch FileSearch { get; set; }

    }

}
