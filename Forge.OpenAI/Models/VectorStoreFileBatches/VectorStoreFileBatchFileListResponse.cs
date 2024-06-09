using Forge.OpenAI.Models.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.VectorStoreFileBatches
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/vector-stores-files/listFiles
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Models.Common.ResponseBase" />
    public class VectorStoreFileBatchFileListResponse : ResponseBase
    {

        /// <summary>
        /// Gets or sets the vector store file batch data.
        /// </summary>
        [JsonPropertyName("data")]
        public IReadOnlyList<VectorStoreFileBatchData> Data { get; set; }

        [JsonPropertyName("first_id")]
        public string FirstId { get; set; }

        [JsonPropertyName("last_id")]
        public string LastId { get; set; }

        [JsonPropertyName("has_more")]
        public bool HasMore { get; set; }

    }

}
