using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.FineTunes
{

    /// <summary>Represents the fune tune hyper parameters</summary>
    public class FineTuneHyperParams
    {

        /// <summary>Initializes a new instance of the <see cref="FineTuneHyperParams" /> class.</summary>
        public FineTuneHyperParams()
        {
        }

        /// <summary>Gets or sets the size of the batch.</summary>
        /// <value>The size of the batch.</value>
        [JsonPropertyName("batch_size")]
        public int? BatchSize { get; set; }

        /// <summary>Gets or sets the learning rate multiplier.</summary>
        /// <value>The learning rate multiplier.</value>
        [JsonPropertyName("learning_rate_multiplier")]
        public double? LearningRateMultiplier { get; set; }

        /// <summary>Gets or sets the epochs.</summary>
        /// <value>The epochs.</value>
        [JsonPropertyName("n_epochs")]
        public int Epochs { get; set; }

        /// <summary>Gets or sets the prompt loss weight.</summary>
        /// <value>The prompt loss weight.</value>
        [JsonPropertyName("prompt_loss_weight")]
        public double PromptLossWeight { get; set; }

        /// <summary>Performs an implicit conversion from <see cref="FineTuneHyperParams" /> to <see cref="System.String" />.</summary>
        /// <param name="data">The data.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(FineTuneHyperParams data) => data?.ToString();

    }

}
