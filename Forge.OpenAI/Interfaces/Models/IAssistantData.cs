using Forge.OpenAI.Models.Shared;
using System;
using System.Collections.Generic;

namespace Forge.OpenAI.Interfaces.Models
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/assistants/listAssistants
    /// </summary>
    public interface IAssistantData
    {

        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The object type, which is always assistant.
        /// </summary>
        string Object { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the assistant was created.
        /// </summary>
        int CreatedAtUnixTime { get; }

        /// <summary>Gets the created at.</summary>
        /// <value>The created at.</value>
        DateTime CreatedAt { get; }

        /// <summary>
        /// The name of the assistant.
        /// The maximum length is 256 characters.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The description of the assistant.
        /// The maximum length is 512 characters.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// ID of the model to use.
        /// You can use the List models API to see all of your available models,
        /// or see our Model overview for descriptions of them.
        /// </summary>
        string Model { get; }

        /// <summary>
        /// The system instructions that the assistant uses.
        /// The maximum length is 32768 characters.
        /// </summary>
        string Instructions { get; }

        /// <summary>
        /// A list of tool enabled on the assistant.
        /// There can be a maximum of 128 tools per assistant.
        /// Tools can be of types 'code_interpreter', 'retrieval', or 'function'.
        /// </summary>
        IReadOnlyList<Tool> Tools { get; }

        /// <summary>
        /// A set of resources that are used by the assistant's tools. The resources are specific to the type of tool. For example, the code_interpreter tool requires a list of file IDs, while the file_search tool requires a list of vector store IDs.
        /// <a href="https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-tool_resources">https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-tool_resources</a>
        /// </summary>
        /// <value>The tool resources.</value>
        ToolResource ToolResources { get; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        IReadOnlyDictionary<string, string> Metadata { get; }

        /// <summary>
        /// What sampling temperature to use, between 0 and 2. Higher values like 0.8 will make the output more random, while lower values like 0.2 will make it more focused and deterministic.
        /// <see href="https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-temperature" />
        /// </summary>
        /// <value>The temperature.</value>
        double? Temperature { get; }

        /// <summary>
        /// An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. So 0.1 means only the tokens comprising the top 10% probability mass are considered.
        /// We generally recommend altering this or temperature but not both. <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-top_p" />
        /// </summary>
        double? TopP { get; }

        /// <summary>Gets or sets the response format.</summary>
        /// <value>
        ///   <a href="https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-response_format"></a>
        /// </value>
        object ResponseFormat { get; }

    }

}
