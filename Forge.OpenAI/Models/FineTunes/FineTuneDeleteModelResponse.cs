using Forge.OpenAI.Models.Common;
using System.Text.Json.Serialization;
using System;

namespace Forge.OpenAI.Models.FineTunes
{

    /// <summary>
    /// Represents a response of a fine tune model deletion request
    /// https://platform.openai.com/docs/api-reference/fine-tunes/delete-model
    /// </summary>
    [Obsolete]
    public class FineTuneDeleteModelResponse : ResponseBase
    {

        /// <summary>Initializes a new instance of the <see cref="FineTuneDeleteModelResponse" /> class.</summary>
        public FineTuneDeleteModelResponse()
        {
        }

        /// <summary>Gets a value indicating whether the was deleted or not.</summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.</value>
        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }

    }

}
