using System.Text.Json;
using System.Text.Json.Serialization;
using Forge.OpenAI.Infrastructure.Serialization;

namespace Forge.OpenAI.Models.FineTuningJob
{

    /// <summary>Represents the fune tuning job hyper parameters for a response</summary>
    public class FineTuningJobResponseHyperParams
    {

        /// <summary>Gets or sets the epochs.</summary>
        /// <value>The epochs.</value>
        [JsonPropertyName("n_epochs")]
        [JsonConverter(typeof(IntegerAutoStringJsonConverter))]
        public int? Epochs { get; set; }

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => JsonSerializer.Serialize(this, GetType());

    }

}
