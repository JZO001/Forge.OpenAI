using System;
using System.Reflection;

namespace Forge.OpenAI.Infrastructure
{

    /// <summary>Make a copy about the source instance into the destination one</summary>
    public static class MappingHelper
    {

        public static void Map<TSource, TDestination>(TSource source, TDestination destination, bool throwErrorOnMissingTargetProperty = false)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            Type sourceType = source.GetType();
            Type destinationType = destination.GetType();

            foreach (PropertyInfo sourceProperty in sourceType.GetProperties())
            {
                PropertyInfo destinationProperty = destinationType.GetProperty(sourceProperty.Name);

                if (destinationProperty != null)
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
                }
                else if (throwErrorOnMissingTargetProperty)
                {
                    throw new InvalidOperationException($"Property {sourceProperty.Name} not found in destination object");
                }
            }
        }

    }

}
