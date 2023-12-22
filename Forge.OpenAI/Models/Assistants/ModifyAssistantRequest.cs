using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Assistants
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/assistants/createAssistant
    /// </summary>
    public class ModifyAssistantRequest : AssistantRequestBase
    {

        /// <summary>The ID of the assistant to modify.</summary>
        /// <value>The assistant identifier.</value>
        [JsonIgnore]
        [Required]
        public string AssistantId { get; set; }

    }

}
