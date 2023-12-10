using Forge.OpenAI.Models.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Embeddings
{

    /// <summary>Represents the embeddings response</summary>
    public class EmbeddingsResponse : ResponseBase
    {

        /// <summary>Initializes a new instance of the <see cref="EmbeddingsResponse" /> class.</summary>
        public EmbeddingsResponse()
        {
        }

        /// <summary>Gets the embedding response data.</summary>
        /// <value>The data.</value>
        [JsonPropertyName("data")]
        public IReadOnlyList<EmbeddingsResponseData> Data { get; set; }

        /// <summary>Gets the token usage numbers.</summary>
        /// <value>The usage.</value>
        [JsonPropertyName("usage")]
        public Usage Usage { get; set; }

    }

}
