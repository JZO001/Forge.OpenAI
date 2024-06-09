using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.FineTuningJob
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/fine-tuning/checkpoint-object#fine-tuning/checkpoint-object-metrics
    /// </summary>
    public class FineTuningJobCheckpointMetrics
    {

        [JsonPropertyName("step")]
        public int Step { get; set; }

        [JsonPropertyName("train_loss")]
        public int TrainLoss { get; set; }

        [JsonPropertyName("train_mean_token_accuracy")]
        public int TrainMeanTokenAccuracy { get; set; }

        [JsonPropertyName("valid_loss")]
        public int ValidLoss { get; set; }

        [JsonPropertyName("valid_mean_token_accuracy")]
        public int ValidMeanTokenAccuracy { get; set; }

        [JsonPropertyName("full_valid_loss")]
        public int FullValidLoss { get; set; }

        [JsonPropertyName("full_valid_mean_token_accuracy")]
        public int FullValidMeanTokenAccuracy { get; set; }

    }

}
