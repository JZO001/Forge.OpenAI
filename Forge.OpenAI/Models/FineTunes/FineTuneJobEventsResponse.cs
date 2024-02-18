using Forge.OpenAI.Models.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

namespace Forge.OpenAI.Models.FineTunes
{

    /// <summary>Represents the response of the list of fine tune events</summary>
    [Obsolete]
    public class FineTuneJobEventsResponse : ResponseBase
    {

        /// <summary>Initializes a new instance of the <see cref="FineTuneJobEventsResponse" /> class.</summary>
        public FineTuneJobEventsResponse()
        {
        }

        /// <summary>Gets the events.</summary>
        /// <value>The events.</value>
        [JsonPropertyName("data")]
        public IReadOnlyList<FineTuneJobEvent> Events { get; set; }

    }

}
