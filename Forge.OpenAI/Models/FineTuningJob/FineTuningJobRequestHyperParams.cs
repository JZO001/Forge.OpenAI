using System.Text.Json;
using System.Text.Json.Serialization;
using Forge.OpenAI.Infrastructure.Serialization;

namespace Forge.OpenAI.Models.FineTuningJob
{

    /// <summary>Represents the fune tuning job hyper parameters for a request</summary>
    public class FineTuningJobRequestHyperParams
    {

        /// <summary>Gets or sets the size of the batch.</summary>
        /// <value>The size of the batch.</value>
        [JsonPropertyName("batch_size")]
        [JsonConverter(typeof(IntegerAutoStringJsonConverter))]
        public int? BatchSize { get; set; }

        /// <summary>Gets or sets the learning rate multiplier.</summary>
        /// <value>The learning rate multiplier.</value>
        [JsonPropertyName("learning_rate_multiplier")]
        [JsonConverter(typeof(IntegerAutoStringJsonConverter))]
        public double? LearningRateMultiplier { get; set; }

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
