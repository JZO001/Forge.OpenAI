using Forge.OpenAI.Interfaces.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Assistants
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/assistants/file-object
    /// </summary>
    public class AssistantFileData : IAssistantFileData
    {

        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The object type, which is always assistant.file.
        /// </summary>
        [JsonPropertyName("object")]
        public string Object { get; set; }

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

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => JsonSerializer.Serialize(this, GetType());

        /// <summary>Performs an implicit conversion from <see cref="AssistantFileData" /> to <see cref="System.String" />.</summary>
        /// <param name="data">The data.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(AssistantFileData data) => data?.ToString();

    }

}
