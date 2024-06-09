using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.VectorStoreFileBatches
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/vector-stores-files/deleteFile
    /// </summary>
    public class DeleteVectorStoreFileBatchResponse
    {

        /// <summary>
        /// Deletion status
        /// </summary>
        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }

    }

}
