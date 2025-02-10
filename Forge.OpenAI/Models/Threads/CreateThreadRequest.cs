using System.Collections.Generic;
using System.Text.Json.Serialization;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Shared;

namespace Forge.OpenAI.Models.Threads
{

    /// <summary>Create a thread</summary>
    public class CreateThreadRequest : RequestBase
    {

        /// <summary>
        /// A list of messages to start the thread with.
        /// </summary>
        [JsonPropertyName("messages")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IList<Message> Messages { get; set; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        [JsonPropertyName("metadata")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// A set of resources that are used by the assistant's tools. The resources are specific to the type of tool. For example, the code_interpreter tool requires a list of file IDs, while the file_search tool requires a list of vector store IDs.
        /// <a href="https://platform.openai.com/docs/api-reference/threads/createThread#threads-createthread-tool_resources">https://platform.openai.com/docs/api-reference/threads/createThread#threads-createthread-tool_resources</a>
        /// </summary>
        /// <value>The tool resources.</value>
        [JsonPropertyName("tool_resources")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ToolResource ToolResources { get; set; }

    }

}
