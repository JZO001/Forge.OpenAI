using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Shared
{

    /// <summary>
    ///   <a href="https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-tool_resources">https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-tool_resources</a>
    /// </summary>
    public class ToolResource
    {

        [JsonPropertyName("code_interpreter")]
        public CodeInterpreter CodeInterpreter { get; set; }

        [JsonPropertyName("file_search")]
        public FileSearch FileSearch { get; set; }

    }

}
