using System;

namespace Forge.OpenAI.Interfaces.Models
{

    public interface IMessageFileData
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

        /// <summary>The ID of the message that the File is attached to.</summary>
        /// <value>The message identifier.</value>
        string MessageId { get; set; }

    }

}
