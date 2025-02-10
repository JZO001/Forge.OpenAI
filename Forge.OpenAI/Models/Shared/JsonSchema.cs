using Forge.OpenAI.Factories;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Shared
{

    /// <summary>Json schema</summary>
    public class JsonSchema
    {

        /// <summary>
        /// A description of what the response format is for, used by the model to determine how to respond in the format.
        /// </summary>
        /// <value>The description.</value>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The name of the response format. Must be a-z, A-Z, 0-9, or contain underscores and dashes, with a maximum length of 64.
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Whether to enable strict schema adherence when generating the output. 
        /// If set to true, the model will always follow the exact schema defined in the schema field. 
        /// Only a subset of JSON Schema is supported when strict is true. To learn more, read the Structured Outputs guide.
        /// </summary>
        /// <value>The strict.</value>
        [JsonPropertyName("strict")]
        public bool? Strict { get; set; }

        /// <summary>
        /// The schema for the response format, described as a JSON Schema object.
        /// </summary>
        /// <value>The schema.</value>
        [JsonPropertyName("schema")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PropertyDescriptor Schema { get; set; }

    }

}
