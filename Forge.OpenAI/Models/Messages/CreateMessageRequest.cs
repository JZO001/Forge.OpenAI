using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Forge.OpenAI.Models.Common;

namespace Forge.OpenAI.Models.Messages
{

    /// <summary>Create a message.</summary>
    public class CreateMessageRequest : RequestBase
    {

        /// <summary>Initializes a new instance of the <see cref="CreateMessageRequest" /> class.</summary>
        public CreateMessageRequest()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CreateMessageRequest" /> class.</summary>
        /// <param name="threadId">The threadId.</param>
        /// <param name="content">The content.</param>
        /// <param name="fileIds">The file ids.</param>
        /// <param name="metadata">The metadata.</param>
        public CreateMessageRequest(string threadId, string content, IEnumerable<string> fileIds = null, IDictionary<string, string> metadata = null)
        {
            ThreadId = threadId;
            Content = content;
            if (fileIds != null) FileIds = new List<string>(fileIds);
            if (metadata != null) Metadata = new Dictionary<string, string>(metadata);
        }

        /// <summary>The ID of the thread to create a message for.</summary>
        /// <value>The thread identifier.</value>
        [Required]
        [JsonIgnore]
        public string ThreadId { get; set; }

        /// <summary>
        /// The role of the entity that is creating the message.
        /// Currently only user is supported.
        /// https://platform.openai.com/docs/api-reference/threads/createThread#threads-createthread-messages
        /// </summary>
        [JsonPropertyName("role")]
        [Required]
        public string Role { get; set; } = "user";

        /// <summary>
        /// The content of the message.
        /// </summary>
        [JsonPropertyName("content")]
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// A list of File IDs that the message should use.
        /// There can be a maximum of 10 files attached to a message.
        /// Useful for tools like 'retrieval' and 'code_interpreter' that can access and use files.
        /// </summary>
        [JsonPropertyName("file_ids")]
        public IList<string> FileIds { get; set; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        [JsonPropertyName("metadata")]
        public IDictionary<string, string> Metadata { get; set; }

    }

}
