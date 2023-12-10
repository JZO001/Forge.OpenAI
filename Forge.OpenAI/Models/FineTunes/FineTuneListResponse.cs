using Forge.OpenAI.Models.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.FineTunes
{

    /// <summary>Represents the response of the fine tune jobs list</summary>
    public class FineTuneListResponse : ResponseBase
    {

        /// <summary>Initializes a new instance of the <see cref="FineTuneListResponse" /> class.</summary>
        public FineTuneListResponse()
        {
        }

        /// <summary>Gets the fine tune jobs.</summary>
        /// <value>The jobs.</value>
        [JsonPropertyName("data")]
        public IReadOnlyList<FineTuneJobData> Jobs { get; set; }

    }

}
