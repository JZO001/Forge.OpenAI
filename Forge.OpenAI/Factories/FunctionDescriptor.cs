using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Factories
{

    /// <summary>Definition of a function call./// </summary>
    public class FunctionDescriptor
    {

        /// <summary>
        /// Required. The name of the function to be called. Must be a-z, A-Z, 0-9,
        /// or contain underscores and dashes, with a maximum length of 64.
        /// </summary>
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Optional. The description of what the function does.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Optional. The parameters the functions accepts, described as a JSON Schema object.
        /// See the guide (https://platform.openai.com/docs/guides/gpt/function-calling) for examples,
        /// and the JSON Schema reference (https://json-schema.org/understanding-json-schema/)
        /// for documentation about the format.
        /// </summary>
        [JsonPropertyName("parameters")]
        public PropertyDescriptor Parameters { get; set; }

        /// <summary>
        /// Whether to enable strict schema adherence when generating the function call. 
        /// If set to true, the model will follow the exact schema defined in the parameters field. 
        /// Only a subset of JSON Schema is supported when strict is true. 
        /// Learn more about Structured Outputs in the function calling guide.
        /// </summary>
        /// <value>The strict.</value>
        [JsonPropertyName("strict")]
        public bool? Strict { get; set; }

    }

}
