using Forge.OpenAI.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Assistants
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/assistants/createAssistantFile
    /// </summary>
    public class CreateAssistantFileRequest : RequestBase
    {

        /// <summary>The ID of the assistant for which to create a File.</summary>
        /// <value>The assistant identifier.</value>
        [JsonIgnore]
        [Required]
        public string AssistantId { get; set; }

        /// <summary>
        /// A File ID (with purpose="assistants") that the assistant should use. Useful for tools like retrieval and code_interpreter that can access files.
        /// https://platform.openai.com/docs/api-reference/assistants/createAssistantFile#assistants-createassistantfile-file_id
        /// </summary>
        /// <value>The file identifier.</value>
        [JsonPropertyName("file_id")]
        [Required]
        public string FileId { get; set; }

    }

}
