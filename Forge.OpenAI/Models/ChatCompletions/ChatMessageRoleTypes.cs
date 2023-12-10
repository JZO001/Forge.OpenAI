namespace Forge.OpenAI.Models.ChatCompletions
{

    /// <summary>Represents the set of available role types</summary>
    public static class ChatMessageRoleTypes
    {

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public const string USER = "user";
        public const string SYSTEM = "system";
        public const string ASSISTANT = "assistant";
        public const string FUNCTION = "function";

        public static readonly string[] ValidRoleTypes = new string[] { USER, SYSTEM, ASSISTANT, FUNCTION };
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    }

}
