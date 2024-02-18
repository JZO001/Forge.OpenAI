using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Assistants;
using Forge.OpenAI.Models.Threads;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Forge.OpenAI.Models.Runs
{

    /// <summary>Create a thread and run it in one request.</summary>
    public class CreateThreadAndRunRequest : RequestBase
    {

        /// <summary>
        /// The ID of the assistant to use to execute this run.
        /// </summary>
        [Required]
        [JsonPropertyName("assistant_id")]
        public string AssistantId { get; set; }

        /// <summary>Gets or sets the thread data for the creation</summary>
        /// <value>The thread.</value>
        [JsonPropertyName("thread")]
        public CreateThreadRequest Thread { get; set; }

        /// <summary>
        /// The ID of the Model to be used to execute this run.
        /// If a value is provided here, it will override the model associated with the assistant.
        /// If not, the model associated with the assistant will be used.
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; }

        /// <summary>
        /// Override the default system message of the assistant.
        /// This is useful for modifying the behavior on a per-run basis.
        /// </summary>
        [JsonPropertyName("instructions")]
        public string Instructions { get; set; }

        /// <summary>
        /// Override the tools the assistant can use for this run.
        /// This is useful for modifying the behavior on a per-run basis.
        /// </summary>
        [JsonPropertyName("tools")]
        public IReadOnlyList<Tool> Tools { get; set; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        [JsonPropertyName("metadata")]
        public IReadOnlyDictionary<string, string> Metadata { get; set; }

    }

}
