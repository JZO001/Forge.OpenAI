using System;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Shared
{

    /// <summary>
    ///   <a href="https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-tool_choice">https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-tool_choice</a>
    /// </summary>
    public class ToolChoiceFunction
    {

        /// <summary>Initializes a new instance of the <see cref="ToolChoiceFunction" /> class.</summary>
        /// <param name="functionName">Name of the function.</param>
        /// <exception cref="ArgumentNullException">functionName</exception>
        public ToolChoiceFunction(string functionName)
        {
            if (string.IsNullOrWhiteSpace(functionName)) throw new ArgumentNullException(nameof(functionName));

            FunctionName = functionName;
        }

        /// <summary>Gets the name of the function.</summary>
        /// <value>The name of the function.</value>
        [JsonPropertyName("name")]
        public string FunctionName { get; private set; }

    }

}
