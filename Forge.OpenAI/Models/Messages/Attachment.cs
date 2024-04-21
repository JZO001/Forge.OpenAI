using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Messages
{

    /// <summary>
    ///   <a href="https://platform.openai.com/docs/api-reference/messages/createMessage#messages-createmessage-attachments">https://platform.openai.com/docs/api-reference/messages/createMessage#messages-createmessage-attachments</a>
    /// </summary>
    public class Attachment
    {

        /// <summary>A list of files attached to the message, and the tools they should be added to.</summary>
        /// <value>The file ids.</value>
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }

        /// <summary>A list of files attached to the message, and the tools they should be added to.</summary>
        /// <value>The attachments.</value>
        [JsonPropertyName("attachments")]
        public IReadOnlyList<AttachmentTool> Attachments { get; set; }

    }

}
