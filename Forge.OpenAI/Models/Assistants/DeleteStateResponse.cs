using Forge.OpenAI.Models.Common;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Assistants
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/assistants/deleteAssistant
    /// </summary>
    public class DeleteStateResponse : ResponseBase
    {

        /// <summary>Deletion status</summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.</value>
        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }

    }

}
