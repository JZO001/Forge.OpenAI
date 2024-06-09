using System.Collections.Generic;
using System.Text.Json.Serialization;
using Forge.OpenAI.Models.Common;

namespace Forge.OpenAI.Models.Batch
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/batch/list
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Models.Common.ResponseBase" />
    public class ListBatchesResponse : ResponseBase
    {

        [JsonPropertyName("data")]
        public IReadOnlyList<BatchData> Batches { get; set; }

    }

}
