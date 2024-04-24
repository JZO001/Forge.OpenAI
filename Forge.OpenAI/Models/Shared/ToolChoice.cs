using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Shared
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-tool_choice
    /// </summary>
    public class ToolChoice
    {

        public const string TYPE_FILE_SEARCH = "file_search";
        public const string TYPE_FUNCTION = "function";

        /// <summary>Initializes a new instance of the <see cref="ToolChoice" /> class.</summary>
        public ToolChoice()
        {
            Type = TYPE_FILE_SEARCH;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolChoice"/> class.
        /// </summary>
        /// <param name="toolChoiceFunction">The tool choice function.</param>
        /// <exception cref="ArgumentNullException">nameof(toolChoiceFunction)</exception>
        public ToolChoice(ToolChoiceFunction toolChoiceFunction)
        {
            if (toolChoiceFunction == null) throw new ArgumentNullException(nameof(toolChoiceFunction));

            Function = toolChoiceFunction;
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [Required]
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the function.
        /// </summary>
        /// <value>
        /// The function.
        /// </value>
        [JsonPropertyName("function")]
        public ToolChoiceFunction Function { get; set; }

    }

}
