using Forge.OpenAI.Interfaces.Models;
using System;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Messages
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/messages/file-object">https://platform.openai.com/docs/api-reference/messages/file-object
    /// </summary>
    public class MessageFileData : IMessageFileData
    {

        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The object type, which is always assistant.
        /// </summary>
        [JsonPropertyName("object")]
        public string Object { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the assistant was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public int CreatedAtUnixTime { get; set; }

        /// <summary>Gets the created at.</summary>
        /// <value>The created at.</value>
        [JsonIgnore]
        public DateTime CreatedAt => DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTime).DateTime;

        /// <summary>The ID of the message that the File is attached to.</summary>
        /// <value>The message identifier.</value>
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }

    }

}
