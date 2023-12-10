using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.FineTunes
{

    /// <summary>Represents the fune tune hyper parameters for a response</summary>
    public class FineTuneHyperParams
    {

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

    }

}
