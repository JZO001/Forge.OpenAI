using Forge.OpenAI.Models.Common;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.VectorStoreFiles
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/vector-stores-files/deleteFile
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Models.Common.ResponseBase" />
    public class DeleteVectorStoreFileResponse : ResponseBase
    {

        /// <summary>
        /// Deletion status
        /// </summary>
        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }

    }

}
