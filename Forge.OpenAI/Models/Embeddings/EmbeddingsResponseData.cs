using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Embeddings
{

    /// <summary>Represents the response data for each inputs</summary>
    public class EmbeddingsResponseData
    {

        /// <summary>Initializes a new instance of the <see cref="EmbeddingsResponseData" /> class.</summary>
        public EmbeddingsResponseData()
        {
        }

        /// <summary>Gets or sets the embedding.</summary>
        /// <value>The embedding.</value>
        [JsonPropertyName("embedding")]
        public IReadOnlyList<double> Embedding { get; set; }

        /// <summary>Gets or sets the index.</summary>
        /// <value>The index.</value>
        [JsonPropertyName("index")]
        public int Index { get; set; }

        /// <summary>Gets the response object type.</summary>
        /// <value>The object.</value>
        [JsonPropertyName("object")]
        public string Object { get; set; }

    }

}
