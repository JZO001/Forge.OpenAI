using Forge.OpenAI.Interfaces.Models;
using Forge.OpenAI.Models.Common;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Messages
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/messages/getMessageFile
    /// </summary>
    public class MessageFileResponse : ResponseBase, IMessageFileData
    {

        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

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
