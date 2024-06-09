using Forge.OpenAI.Models.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.VectorStores
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/vector-stores/list
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Models.Common.ResponseBase" />
    public class VectorStoreListResponse : ResponseBase
    {

        /// <summary>
        /// List of the vector store data.
        /// </summary>
        [JsonPropertyName("data")]
        public IReadOnlyList<VectorStoreData> Data { get; set; }

        [JsonPropertyName("first_id")]
        public string FirstId { get; set; }

        [JsonPropertyName("last_id")]
        public string LastId { get; set; }

        [JsonPropertyName("has_more")]
        public bool HasMore { get; set; }

    }

}
