﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Forge.OpenAI.Infrastructure.Serialization;
using Forge.OpenAI.Models.Shared;

namespace Forge.OpenAI.Models.Assistants
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/assistants/listAssistants
    /// </summary>
    public class AssistantData
    {

        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The object type, which is always assistant.
        /// </summary>
        [JsonPropertyName("object")]
        public string Object { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the assistant was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public int CreatedAtUnixTime { get; set; }

        /// <summary>Gets the created at.</summary>
        /// <value>The created at.</value>
        [JsonIgnore]
        public DateTime CreatedAt => DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTime).DateTime;

        /// <summary>
        /// The name of the assistant.
        /// The maximum length is 256 characters.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Name { get; set; }

        /// <summary>
        /// The description of the assistant.
        /// The maximum length is 512 characters.
        /// </summary>
        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Description { get; set; }

        /// <summary>
        /// ID of the model to use.
        /// You can use the List models API to see all of your available models,
        /// or see our Model overview for descriptions of them.
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; }

        /// <summary>
        /// The system instructions that the assistant uses.
        /// The maximum length is 32768 characters.
        /// </summary>
        [JsonPropertyName("instructions")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Instructions { get; set; }

        /// <summary>
        /// A list of tool enabled on the assistant.
        /// There can be a maximum of 128 tools per assistant.
        /// Tools can be of types 'code_interpreter', 'retrieval', or 'function'.
        /// </summary>
        [JsonPropertyName("tools")]
        public IReadOnlyList<Tool> Tools { get; set; }

        /// <summary>
        /// A set of resources that are used by the assistant's tools. The resources are specific to the type of tool. For example, the code_interpreter tool requires a list of file IDs, while the file_search tool requires a list of vector store IDs.
        /// <a href="https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-tool_resources">https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-tool_resources</a>
        /// </summary>
        /// <value>The tool resources.</value>
        [JsonPropertyName("tool_resources")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ToolResource ToolResources { get; set; }

        /// <summary>
        /// A list of file IDs attached to this assistant.
        /// There can be a maximum of 20 files attached to the assistant.
        /// Files are ordered by their creation date in ascending order.
        /// </summary>
        [Obsolete]
        [JsonPropertyName("file_ids")]
        public IReadOnlyList<string> FileIds { get; set; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        [JsonPropertyName("metadata")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IReadOnlyDictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// What sampling temperature to use, between 0 and 2. Higher values like 0.8 will make the output more random, while lower values like 0.2 will make it more focused and deterministic.
        /// <see href="https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-temperature" />
        /// </summary>
        /// <value>The temperature.</value>
        [JsonPropertyName("temperature")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? Temperature { get; set; }

        /// <summary>
        /// An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. So 0.1 means only the tokens comprising the top 10% probability mass are considered.
        /// We generally recommend altering this or temperature but not both. <br/>
        /// <see href="https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-top_p" />
        /// </summary>
        [JsonPropertyName("top_p")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? TopP { get; set; }

        /// <summary>Gets or sets the response format.</summary>
        /// <value>
        ///   <a href="https://platform.openai.com/docs/api-reference/assistants/createAssistant#assistants-createassistant-response_format"></a>
        /// </value>
        [JsonPropertyName("response_format")]
        [JsonConverter(typeof(ResponseFormatConverter))]
        public object ResponseFormat { get; set; }

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => JsonSerializer.Serialize(this, GetType());

        /// <summary>Performs an implicit conversion from <see cref="AssistantData" /> to <see cref="System.String" />.</summary>
        /// <param name="data">The data.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(AssistantData data) => data?.ToString();

    }

}
