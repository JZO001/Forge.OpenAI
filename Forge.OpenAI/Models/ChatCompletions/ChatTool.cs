using System.Text.Json.Serialization;
using Forge.OpenAI.Factories;

namespace Forge.OpenAI.Models.ChatCompletions
{

    public class ChatTool
    {

        /// <summary>Gets or sets the type.</summary>
        /// <value></value>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("function")]
        public FunctionDescriptor Function { get; set; }

    }

}
