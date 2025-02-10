using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Assistants
{

    /// <summary>Overrides for the file search tool.</summary>
    public class AssistantFileSearch
    {

        /// <summary>
        /// The maximum number of results the file search tool should output. 
        /// The default is 20 for gpt-4* models and 5 for gpt-3.5-turbo. This number should be between 1 and 50 inclusive.
        /// Note that the file search tool may output fewer than max_num_results results.See the file search tool documentation for more information.
        /// </summary>
        /// <value>The maximum number results.</value>
        [JsonPropertyName("max_num_results")]
        public int? MaxNumResults { get; set; }

        /// <summary>
        /// The ranking options for the file search. If not specified, the file search tool will use the auto ranker and a score_threshold of 0.
        /// </summary>
        /// <value>The ranking options.</value>
        [JsonPropertyName("ranking_options")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AssistantRankingOptions RankingOptions { get; set; }

    }

}
