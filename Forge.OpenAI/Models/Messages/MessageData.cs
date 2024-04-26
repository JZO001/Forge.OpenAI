using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Forge.OpenAI.Models.Shared;

namespace Forge.OpenAI.Models.Messages
{

    public class MessageData
    {

        public const string STATUS_IN_PROGRESS = "in_progress";
        public const string STATUS_COMPLETED = "completed";
        public const string STATUS_INCOMPLETE = "incomplete";

        public const string ROLE_USER = "user";
        public const string ROLE_ASSISTANT = "assistant";
        public const string ROLE_SYSTEM = "system";

        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Object type, ie: text_completion, file, fine-tune, list, etc
        /// </summary>
        [JsonPropertyName("object")]
        public string Object { get; set; }

        /// <summary>
        /// The time when the result was generated in unix epoch format
        /// </summary>
        [JsonPropertyName("created_at")]
        public long? CreatedAtUnixTime { get; set; }

        /// <summary>
        /// The time when the result was generated.
        /// </summary>
        [JsonIgnore]
        public DateTime? CreatedAt => CreatedAtUnixTime.HasValue ? (DateTime?)(DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTime.Value).DateTime) : null;

        /// <summary>
        /// The thread ID that this message belongs to.
        /// </summary>
        [JsonPropertyName("thread_id")]
        public string ThreadId { get; set; }

        /// <summary>
        /// The thread ID that this message belongs to.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        ///   <para>
        /// On an incomplete message, details about why the message is incomplete.</para>
        ///   <para>
        ///     <a href="https://platform.openai.com/docs/api-reference/messages/object#messages/object-incomplete_details">https://platform.openai.com/docs/api-reference/messages/object#messages/object-incomplete_details</a>
        ///   </para>
        /// </summary>
        /// <value>The incomplete details.</value>
        [JsonPropertyName("incomplete_details")]
        public IncompleteDetails IncompleteDetails { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was completed.
        /// </summary>
        [JsonPropertyName("completed_at")]
        public int? CompletedAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was completed.
        /// </summary>
        [JsonIgnore]
        public DateTime? CompletedAt => CompletedAtUnixTimeSeconds.HasValue ? (DateTime?)DateTimeOffset.FromUnixTimeSeconds(CompletedAtUnixTimeSeconds.Value).DateTime : null;

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was completed.
        /// </summary>
        [JsonPropertyName("incomplete_at")]
        public int? IncompletedAtUnixTimeSeconds { get; set; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was completed.
        /// </summary>
        [JsonIgnore]
        public DateTime? IncompletedAt => IncompletedAtUnixTimeSeconds.HasValue ? (DateTime?)DateTimeOffset.FromUnixTimeSeconds(IncompletedAtUnixTimeSeconds.Value).DateTime : null;

        /// <summary>
        /// The entity that produced the message. One of user or assistant.
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; }

        /// <summary>
        /// The content of the message in array of text and/or images.
        /// </summary>
        [JsonPropertyName("content")]
        public IReadOnlyList<Content> Content { get; set; }

        /// <summary>
        /// If applicable, the ID of the assistant that authored this message.
        /// </summary>
        [JsonPropertyName("assistant_id")]
        public string AssistantId { get; set; }

        /// <summary>
        /// If applicable, the ID of the run associated with the authoring of this message.
        /// </summary>
        [JsonPropertyName("run_id")]
        public string RunId { get; set; }

        /// <summary>
        /// A list of file IDs that the assistant should use.
        /// Useful for tools like 'retrieval' and 'code_interpreter' that can access files.
        /// A maximum of 10 files can be attached to a message.
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
        public IReadOnlyDictionary<string, string> Metadata { get; set; }

        /// <summary>
        ///   <para>
        /// A list of files attached to the message, and the tools they were added to.</para>
        ///   <para>
        ///     <a href="https://platform.openai.com/docs/api-reference/messages/object#messages/object-attachments">https://platform.openai.com/docs/api-reference/messages/object#messages/object-attachments</a>
        ///   </para>
        /// </summary>
        /// <value>The attachments.</value>
        [JsonPropertyName("attachments")]
        public IReadOnlyList<Attachment> Attachments { get; set; }

    }

}
