using Forge.OpenAI.Models.Common;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Forge.OpenAI.Models.Models
{

    /// <summary>Represents the list of the available OpenAI models</summary>
    public class ModelsResponse : ResponseBase
    {

        /// <summary>Initializes a new instance of the <see cref="ModelsResponse" /> class.</summary>
        public ModelsResponse()
        {
        }

        /// <summary>Gets the available models.</summary>
        /// <value>The models.</value>
        [JsonPropertyName("data")]
        public IReadOnlyList<Model> Models { get; set; }

    }

}
