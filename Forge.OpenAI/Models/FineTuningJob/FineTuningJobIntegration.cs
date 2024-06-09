using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.FineTuningJob
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/fine-tuning/object#fine-tuning/object-integrations
    /// </summary>
    public class FineTuningJobIntegration
    {

        /// <summary>
        /// The type of the integration being enabled for the fine-tuning job
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// The settings for your integration with Weights and Biases. 
        /// This payload specifies the project that metrics will be sent to. 
        /// Optionally, you can set an explicit display name for your run, add tags to your run, and set a default entity (team, username, etc) to be associated with your run.
        /// </summary>
        /// <value>
        /// The weights and biases.
        /// </value>
        [JsonPropertyName("wandb")]
        public FineTuningJobWeightAndBias WeightsAndBiases { get; set; }

    }

}
