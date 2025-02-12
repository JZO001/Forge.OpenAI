using Forge.OpenAI.Models.Messages;
using Forge.OpenAI.Models.Shared;
using System;
using System.Collections.Generic;

namespace Forge.OpenAI.Interfaces.Models
{

    public interface IMessageData
    {

        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Object type, ie: text_completion, file, fine-tune, list, etc
        /// </summary>
        string Object { get; }

        /// <summary>
        /// The time when the result was generated in unix epoch format
        /// </summary>
        long? CreatedAtUnixTime { get; }

        /// <summary>
        /// The time when the result was generated.
        /// </summary>
        DateTime? CreatedAt { get; }

        /// <summary>
        /// The thread ID that this message belongs to.
        /// </summary>
        string ThreadId { get; }

        /// <summary>
        /// The thread ID that this message belongs to.
        /// </summary>
        string Status { get; }

        /// <summary>
        ///   <para>
        /// On an incomplete message, details about why the message is incomplete.</para>
        ///   <para>
        ///     <a href="https://platform.openai.com/docs/api-reference/messages/object#messages/object-incomplete_details">https://platform.openai.com/docs/api-reference/messages/object#messages/object-incomplete_details</a>
        ///   </para>
        /// </summary>
        /// <value>The incomplete details.</value>
        IncompleteDetails IncompleteDetails { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was completed.
        /// </summary>
        int? CompletedAtUnixTimeSeconds { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was completed.
        /// </summary>
        DateTime? CompletedAt { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was completed.
        /// </summary>
        int? IncompletedAtUnixTimeSeconds { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run was completed.
        /// </summary>
        DateTime? IncompletedAt { get; }

        /// <summary>
        /// The entity that produced the message. One of user or assistant.
        /// </summary>
        string Role { get; }

        /// <summary>
        /// The content of the message in array of text and/or images.
        /// </summary>
        IReadOnlyList<Content> Content { get; }

        /// <summary>
        /// If applicable, the ID of the assistant that authored this message.
        /// </summary>
        string AssistantId { get; }

        /// <summary>
        /// If applicable, the ID of the run associated with the authoring of this message.
        /// </summary>
        string RunId { get; }

        /// <summary>
        /// A list of files attached to the message, and the tools they were added to.
        /// https://platform.openai.com/docs/api-reference/messages/object#messages/object-attachments
        /// </summary>
        /// <value>The attachments.</value>
        IReadOnlyList<OpenAI.Models.Messages.Attachment> Attachments { get; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        IReadOnlyDictionary<string, string> Metadata { get; }

    }

}
