using Forge.OpenAI.Models.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.FineTuningJob
{

    public class FineTuningJobListResponse : ResponseBase
    {

        [JsonPropertyName("data")]
        public IReadOnlyList<FineTuningJobResponse> Data { get; set; }

        [JsonPropertyName("has_more")]
        public bool HasMore { get; set; }

    }

}
