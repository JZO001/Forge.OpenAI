using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Shared
{

    /// <summary>A message to start the thread</summary>
    public class Message
    {

        public const string ROLE_USER = "user";
        public const string ROLE_ASSISTANT = "assistant";

        /// <summary>Initializes a new instance of the <see cref="Message" /> class.</summary>
        /// <param name="content">The content.</param>
        /// <param name="fileIds">The file ids.</param>
        /// <param name="metadata">The metadata.</param>
        public Message(string content, IEnumerable<string> fileIds = null, IDictionary<string, string> metadata = null)
        {
            Content = content;
            if (fileIds != null) FileIds = new List<string>(fileIds);
            if (metadata != null) Metadata = new Dictionary<string, string>(metadata);
        }

        /// <summary>
        /// The role of the entity that is creating the message.
        /// Currently only user is supported.
        /// https://platform.openai.com/docs/api-reference/threads/createThread#threads-createthread-messages
        /// </summary>
        [Required]
        [JsonPropertyName("role")]
        public string Role { get; set; } = ROLE_USER;

        /// <summary>
        /// The content of the message.
        /// </summary>
        [Required]
        [JsonPropertyName("content")]
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

        /// <summary>
        /// A list of files attached to the message, and the tools they should be added to.
        /// </summary>
        /// <value>The attachments.</value>
        [JsonPropertyName("attachments")]
        public IList<Attachment> Attachments { get; set; }

    }

}
