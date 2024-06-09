using System.Collections.Generic;
using System.Text.Json.Serialization;
using Forge.OpenAI.Models.Common;

namespace Forge.OpenAI.Models.FineTuningJob
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/fine-tuning/list-checkpoints
    /// </summary>
    /// <seealso cref="Forge.OpenAI.Models.Common.ResponseBase" />
    public class FineTuningJobCheckpointListResponse : ResponseBase
    {

        [JsonPropertyName("data")]
        public IReadOnlyList<FineTuningJobCheckpoint> Checkpoints { get; set; }

    }

}
