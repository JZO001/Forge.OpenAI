using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Assistants
{

    /// <summary>
    /// The ranking options for the file search. If not specified, the file search tool will use the auto ranker and a score_threshold of 0.
    /// </summary>
    public class AssistantRankingOptions
    {

        /// <summary>
        /// The ranker to use for the file search. If not specified will use the auto ranker.
        /// </summary>
        /// <value>The ranker.</value>
        [JsonPropertyName("ranker")]
        public string Ranker { get; set; }

        /// <summary>
        /// The score threshold for the file search. All values must be a floating point number between 0 and 1.
        /// </summary>
        /// <value>The score threshold.</value>
        [Required]
        [JsonPropertyName("score_threshold")]
        public float ScoreThreshold { get; set; }

    }

}
