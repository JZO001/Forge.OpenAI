using System.Collections.Generic;

namespace Forge.OpenAI.Factories
{

    /// <summary>Set of builder/helper methods to construct property descriptors</summary>
    public static class PropertyDescriptorBuilders
    {

        public static PropertyDescriptor BuildArray(PropertyDescriptor arrayItems = null)
        {
            return new PropertyDescriptor
            {
                Items = arrayItems,
                Type = ConvertTypeToString(PropertyObjectTypes.Array)
            };
        }

        public static PropertyDescriptor BuildEnum(List<string> enumList, string description = null)
        {
            return new PropertyDescriptor
            {
                Description = description,
                Enum = enumList,
                Type = ConvertTypeToString(PropertyObjectTypes.String)
            };
        }

        public static PropertyDescriptor BuildInteger(string description = null)
        {
            return new PropertyDescriptor
            {
                Description = description,
                Type = ConvertTypeToString(PropertyObjectTypes.Integer)
            };
        }

        public static PropertyDescriptor BuildNumber(string description = null)
        {
            return new PropertyDescriptor
            {
                Description = description,
                Type = ConvertTypeToString(PropertyObjectTypes.Number)
            };
        }

        public static PropertyDescriptor BuildString(string description = null)
        {
            return new PropertyDescriptor
            {
                Description = description,
                Type = ConvertTypeToString(PropertyObjectTypes.String)
            };
        }

        public static PropertyDescriptor BuildBoolean(string description = null)
        {
            return new PropertyDescriptor
            {
                Description = description,
                Type = ConvertTypeToString(PropertyObjectTypes.Boolean)
            };
        }

        public static PropertyDescriptor BuildNull(string description = null)
        {
            return new PropertyDescriptor
            {
                Description = description,
                Type = ConvertTypeToString(PropertyObjectTypes.Null)
            };
        }

        public static PropertyDescriptor BuildObject(
            IDictionary<string, PropertyDescriptor> properties = null,
            ICollection<string> required = null,
            bool? additionalProperties = null,
            string description = null,
            ICollection<string> enums = null)
        {
            return new PropertyDescriptor
            {
                Properties = properties,
                Requireds = required,
                AdditionalProperties = additionalProperties,
                Description = description,
                Enum = enums,
                Type = ConvertTypeToString(PropertyObjectTypes.Object)
            };
        }

        /// <summary>
        ///     Converts a PropertyObjectTypes enumeration value to its corresponding string representation.
        /// </summary>
        /// <param name="type">The type to convert</param>
        /// <returns>The string representation of the given type</returns>
        public static string ConvertTypeToString(PropertyObjectTypes type)
        {
            return type.ToString().ToLowerInvariant();
        }

    }

}
