using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Messages
{

    /// <summary>
    ///   <a href="https://platform.openai.com/docs/api-reference/messages/createMessage#messages-createmessage-attachments">https://platform.openai.com/docs/api-reference/messages/createMessage#messages-createmessage-attachments</a>
    /// </summary>
    public class AttachmentTool
    {

        public const string TYPE_CODE_INTERPRETER = "code_interpreter";
        public const string TYPE_FILE_SEARCH = "file_search";

        /// <summary>Initializes a new instance of the <see cref="AttachmentTool" /> class.</summary>
        /// <param name="type">The type.</param>
        /// <exception cref="System.ArgumentNullException">type</exception>
        public AttachmentTool(string type)
        {
            if (string.IsNullOrWhiteSpace(type)) throw new ArgumentNullException(nameof(type));

            Type = type;
        }

        /// <summary>The type of tool being defined: code_interpreter or file_search</summary>
        /// <value>The type.</value>
        [Required]
        [JsonPropertyName("type")]
        public string Type { get; set; }

    }

}
