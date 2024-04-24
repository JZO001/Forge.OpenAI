using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Shared
{

    public class AttachmentTool
    {

        /// <summary>The tools to add this file to.</summary>
        /// <value>The type.</value>
        [JsonPropertyName("type")]
        public string Type { get; set; }

    }

}
