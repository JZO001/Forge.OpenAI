using Forge.OpenAI.Models.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Messages
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/messages/listMessageFiles
    /// </summary>
    public class MessageFileListResponse : ResponseBase
    {

        [JsonPropertyName("data")]
        public IReadOnlyList<MessageFileData> Data { get; set; }

        [JsonPropertyName("has_more")]
        public bool HasMore { get; set; }

        [JsonPropertyName("first_id")]
        public string FirstId { get; set; }

        [JsonPropertyName("last_id")]
        public string LastId { get; set; }

    }

}
