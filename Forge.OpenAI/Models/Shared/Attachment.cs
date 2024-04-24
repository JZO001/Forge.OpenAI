using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Shared
{

    public class Attachment
    {

        /// <summary>The ID of the file to attach to the message.</summary>
        /// <value>The file identifier.</value>
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }

        /// <summary>The tools to add this file to.</summary>
        /// <value>The tools.</value>
        [JsonPropertyName("tools")]
        public IList<AttachmentTool> Tools { get; set; }

    }

}
