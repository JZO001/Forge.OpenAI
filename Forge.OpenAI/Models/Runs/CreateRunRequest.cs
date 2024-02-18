using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Assistants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System;

namespace Forge.OpenAI.Models.Runs
{

    /// <summary>Create a run.</summary>
    public class CreateRunRequest : RequestBase
    {

        /// <summary>Initializes a new instance of the <see cref="CreateRunRequest" /> class.</summary>
        public CreateRunRequest()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CreateRunRequest" /> class.</summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="assistantId">The assistant identifier.</param>
        public CreateRunRequest(string threadId, string assistantId)
        {
            if (string.IsNullOrWhiteSpace(threadId)) throw new ArgumentNullException(nameof(threadId));
            if (string.IsNullOrWhiteSpace(assistantId)) throw new ArgumentNullException(nameof(assistantId));

            ThreadId = threadId;
            AssistantId = assistantId;
        }

        /// <summary>The ID of the thread to create a message for.</summary>
        /// <value>The thread identifier.</value>
        [Required]
        [JsonIgnore]
        public string ThreadId { get; set; }

        /// <summary>
        /// The ID of the assistant used for execution of this run.
        /// </summary>
        [Required]
        [JsonPropertyName("assistant_id")]
        public string AssistantId { get; set; }

        /// <summary>
        /// The model that the assistant used for this run.
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; }

        /// <summary>
        /// The instructions that the assistant used for this run.
        /// </summary>
        [JsonPropertyName("instructions")]
        public string Instructions { get; set; }

        /// <summary>
        /// Appends additional instructions at the end of the instructions for the run. 
        /// This is useful for modifying the behavior on a per-run basis without overriding other instructions.
        /// </summary>
        [JsonPropertyName("additional_instructions")]
        public string AdditionalInstructions { get; set; }

        /// <summary>
        /// The list of tools that the assistant used for this run.
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
