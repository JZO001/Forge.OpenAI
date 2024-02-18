using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Factories
{

    /// <summary>Describes a property</summary>
    public class PropertyDescriptor
    {

        /// <summary>
        ///     Required. Function parameter object type. Default value is "object".
        /// </summary>
        [Required]
        [JsonPropertyName("type")]
        public string Type { get; set; } = "object";

        /// <summary>
        ///     Optional. List of "function arguments", as a dictionary that maps from argument name
        ///     to an object that describes the type, maybe possible enum values, and so on.
        /// </summary>
        [JsonPropertyName("properties")]
        public IDictionary<string, PropertyDescriptor> Properties { get; set; }

        /// <summary>
        ///     Optional. List of "function arguments" which are required.
        /// </summary>
        [JsonPropertyName("required")]
        public ICollection<string> Requireds { get; set; }

        /// <summary>
        ///     Optional. Whether additional properties are allowed. Default value is true.
        /// </summary>
        [JsonPropertyName("additionalProperties")]
        public bool? AdditionalProperties { get; set; }

        /// <summary>
        ///     Optional. Argument description.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Optional. List of allowed values for this argument.
        /// </summary>
        [JsonPropertyName("enum")]
        public ICollection<string> Enum { get; set; }

        /// <summary>
        ///     The number of properties on an object can be restricted using the minProperties and maxProperties keywords. Each of
        ///     these must be a non-negative integer.
        /// </summary>
        [JsonPropertyName("minProperties")]
        public int? MinProperties { get; set; }

        /// <summary>
        ///     The number of properties on an object can be restricted using the minProperties and maxProperties keywords. Each of
        ///     these must be a non-negative integer.
        /// </summary>
        [JsonPropertyName("maxProperties")]
        public int? MaxProperties { get; set; }

        /// <summary>
        ///     If type is "array", this specifies the element type for all items in the array.
        ///     If type is not "array", this should be null.
        ///     For more details, see https://json-schema.org/understanding-json-schema/reference/array.html
        /// </summary>
        [JsonPropertyName("items")]
        public PropertyDescriptor Items { get; set; }

    }

}
