using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Forge.OpenAI.Factories;

namespace Forge.OpenAI.Models.Shared
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/assistants/object#assistants/object-tools
    /// https://platform.openai.com/docs/api-reference/runs/createRun#runs-createrun-tools
    /// </summary>
    public class Tool
    {

        /// <summary>
        ///   <a href="https://platform.openai.com/docs/api-reference/assistants-v1/createAssistant#assistants-v1-createassistant-tools">https://platform.openai.com/docs/api-reference/assistants-v1/createAssistant#assistants-v1-createassistant-tools</a>
        /// </summary>
        [Obsolete]
        public const string RETRIEVAL = "retrieval";

        /// <summary>
        ///   <a href="https://platform.openai.com/docs/api-reference/assistants-v1/createAssistant#assistants-v1-createassistant-tools">https://platform.openai.com/docs/api-reference/assistants-v1/createAssistant#assistants-v1-createassistant-tools</a>
        ///   <a href="https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-tools">https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-tools</a>
        ///   <a href="https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-tools">https://platform.openai.com/docs/api-reference/runs/createThreadAndRun#runs-createthreadandrun-tools</a>
        /// </summary>
        public const string FUNCTION = "function";

        /// <summary>
        /// https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-tools
        /// </summary>
        public const string CODE_INTERPRETER = "code_interpreter";
        /// <summary>
        /// https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-tools
        /// </summary>
        public const string FILE_SEARCH = "file_search";

        /// <summary>Initializes a new instance of the <see cref="Tool" /> class.</summary>
        public Tool()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Tool" /> class.</summary>
        /// <param name="function">The function.</param>
        /// <exception cref="ArgumentNullException">function</exception>
        public Tool(FunctionDescriptor function)
        {
            if (function == null) throw new ArgumentNullException(nameof(function));

            Type = CODE_INTERPRETER;
            Function = function;
        }

        [JsonPropertyName("id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Id { get; set; }

        [JsonPropertyName("index")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Index { get; set; }

        /// <summary>The type of tool being defined</summary>
        /// <value>The type.</value>
        [JsonPropertyName("type")]
        public string Type { get; set; } = CODE_INTERPRETER;

        /// <summary>Function tool</summary>
        /// <value>The function.</value>
        [JsonPropertyName("function")]
        public FunctionDescriptor Function { get; set; }

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString() => JsonSerializer.Serialize(this, GetType());

        /// <summary>Performs an implicit conversion from <see cref="Tool" /> to <see cref="string" />.</summary>
        /// <param name="data">The data.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(Tool data) => data?.ToString();

    }

}
