using System;
using System.Text.Json.Serialization;
using Forge.OpenAI.Interfaces.Models;
using Forge.OpenAI.Models.Common;

namespace Forge.OpenAI.Models.Assistants
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/assistants/file-object
    /// </summary>
    public class AssistantFileResponse : ResponseBase, IAssistantFileData
    {

        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the assistant file was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public int CreatedAtUnixTime { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt => DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTime).DateTime;

        /// <summary>
        /// The assistant ID that the file is attached to.
        /// </summary>
        [JsonPropertyName("assistant_id")]
        public string AssistantId { get; set; }

    }

}
