using Forge.OpenAI.Models.Common;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.VectorStores
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/vector-stores/delete
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Models.Common.ResponseBase" />
    public class DeleteVectorStoreResponse : ResponseBase
    {

        /// <summary>
        /// Deletion status
        /// </summary>
        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }

    }

}
