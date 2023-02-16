using Forge.OpenAI.Models.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Moderations
{

    /// <summary>Represents the response of the moderation request</summary>
    public class ModerationResponse : ResponseBase
    {

        /// <summary>Initializes a new instance of the <see cref="ModerationResponse" /> class.</summary>
        public ModerationResponse()
        {
        }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>Gets or sets the results of the request inputs.</summary>
        /// <value>The results.</value>
        [JsonPropertyName("results")]
        public List<ModerationResult> Results { get; set; }

    }

}
