using System;

namespace Forge.OpenAI.Interfaces.Models
{

    public interface IAssistantFileData
    {

        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The object type, which is always assistant.file.
        /// </summary>
        string Object { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the assistant file was created.
        /// </summary>
        int CreatedAtUnixTime { get; }

        DateTime CreatedAt { get; }

        /// <summary>
        /// The assistant ID that the file is attached to.
        /// </summary>
        string AssistantId { get; }

    }

}
