﻿using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.ChatCompletions
{
    /// <summary>
    ///   The tool calls generated by the model, such as function calls.
    /// </summary>
    public class ChatAssistantToolCall
    {

        /// <summary>The ID of the tool call.</summary>
        /// <value>The identifier.</value>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>The type of the tool. Currently, only function is supported.</summary>
        /// <value>The type.</value>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>The function that the model called.</summary>
        /// <value>The function.</value>
        [JsonPropertyName("function")]
        public ChatFunctionCall Function { get; set; }

    }
}
