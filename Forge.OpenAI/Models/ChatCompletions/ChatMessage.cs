using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.ChatCompletions
{

    /// <summary>Represents a chat message</summary>
    public class ChatMessage
    {

        /// <summary>Initializes a new instance of the <see cref="ChatMessage" /> class.</summary>
        /// <param name="role">The role.</param>
        /// <param name="content">The content.</param>
        [JsonConstructor]
        public ChatMessage(string role, string content)
        {
            Role = role;
            Content = content;
        }

        /// <summary>
        /// Valid values are "system", "assistant" or "user".
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; }

        /// <summary>
        /// Chat message content
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }

        /// <summary>Creates the chat message with role and content specified</summary>
        /// <param name="role">The role.</param>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   ChatMessage
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// Invalid role type provided. Please specify a valid value from the following items ${string.Join(", ", ChatMessageRoleTypes.ValidRoleTypes)} - role
        /// </exception>
        public static ChatMessage Create(string role, string content)
        {
            if (!ChatMessageRoleTypes.ValidRoleTypes.Contains(role))
            {
                throw new ArgumentException($"Invalid role type provided. Please specify a valid value from the following items ${string.Join(", ", ChatMessageRoleTypes.ValidRoleTypes)}", nameof(role));
            }

            return new ChatMessage(role, content);
        }

        /// <summary>Creates the chat message with the given content as a System role.</summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   ChatMessage
        /// </returns>
        public static ChatMessage CreateFromSystem(string content)
        {
            return new ChatMessage(ChatMessageRoleTypes.SYSTEM, content);
        }

        /// <summary>Creates the chat message with the given content as an Assistant role.</summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   ChatMessage
        /// </returns>
        public static ChatMessage CreateFromAssistant(string content)
        {
            return new ChatMessage(ChatMessageRoleTypes.ASSISTANT, content);
        }

        /// <summary>Creates the chat message with the given content as a User role.</summary>
        /// <param name="content">The content.</param>
        /// <returns>
        ///   ChatMessage
        /// </returns>
        public static ChatMessage CreateFromUser(string content)
        {
            return new ChatMessage(ChatMessageRoleTypes.USER, content);
        }

    }

}
