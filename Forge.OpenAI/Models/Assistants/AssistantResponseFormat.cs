using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Assistants
{

    /// <summary>
    ///   <a href="https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-response_format"></a>
    /// </summary>
    public class AssistantResponseFormat
    {

        /// <summary>Gets or sets the type.</summary>
        /// <value>"json_object" or "text"</value>
        [JsonPropertyName("type")]
        public string Type { get; set; }

    }

}
