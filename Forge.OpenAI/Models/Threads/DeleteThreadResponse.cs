using System.Text.Json.Serialization;
using Forge.OpenAI.Models.Common;

namespace Forge.OpenAI.Models.Threads
{

    /// <summary>Deletion status</summary>
    public class DeleteThreadResponse : ResponseBase
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }

    }

}
