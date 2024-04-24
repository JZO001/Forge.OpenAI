using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Runs
{

    /// <summary>
    ///   <a href="https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-truncation_strategy">https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-truncation_strategy</a>
    /// </summary>
    public class TruncationStrategy
    {

        public const string TYPE_AUTO = "auto";
        public const string TYPE_LAST_MESSAGES = "last_messages";

        /// <summary>
        /// The truncation strategy to use for the thread. The default is auto. If set to last_messages, the thread will be truncated to the n most recent messages in the thread. When set to auto, messages in the middle of the thread will be dropped to fit the context length of the model, max_prompt_tokens.
        /// </summary>
        /// <value>The type.</value>
        [Required]
        [JsonPropertyName("type")]
        public string Type { get; set; } = TYPE_AUTO;

        /// <summary>The number of most recent messages from the thread when constructing the context for the run.</summary>
        /// <value>The last messages.</value>
        [JsonPropertyName("last_messages")]
        public int? LastMessages { get; set; }

    }

}
