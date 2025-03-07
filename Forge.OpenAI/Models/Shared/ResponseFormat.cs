using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Shared
{

    /// <summary>
    ///   <a href="https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-response_format"></a>
    ///   <a href="https://platform.openai.com/docs/api-reference/chat/create#chat-create-response_format">https://platform.openai.com/docs/api-reference/chat/create#chat-create-response_format</a>
    /// </summary>
    public class ResponseFormat
    {

        public const string RESPONSE_FORMAT_AUTO = "auto";
        public const string RESPONSE_FORMAT_JSON_SCHEMA = "json_schema";
        public const string RESPONSE_FORMAT_JSON = "json_object";
        public const string RESPONSE_FORMAT_TEXT = "text";

        /// <summary>Gets or sets the type.</summary>
        /// <value>"json_object" or "text"</value>
        [Required]
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>Gets or sets the json schema.</summary>
        /// <value>The json schema.</value>
        [JsonPropertyName("json_schema")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonSchema JsonSchema { get; set; }

    }

}
