using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Shared
{

    /// <summary>
    ///   <a href="https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-tool_resources">https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-tool_resources</a>
    /// </summary>
    public class FileSearch
    {

        /// <summary>The vector store attached to this assistant. There can be a maximum of 1 vector store attached to the assistant.</summary>
        /// <value>The vector store ids.</value>
        [JsonPropertyName("vector_store_ids")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IReadOnlyList<string> VectorStoreIds { get; set; }

        /// <summary>A helper to create a vector store with file_ids and attach it to this assistant. There can be a maximum of 1 vector store attached to the assistant.</summary>
        /// <value>The vector stores.</value>
        [JsonPropertyName("vector_stores")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IReadOnlyList<VectorStore> VectorStores { get; set; }

    }

}
