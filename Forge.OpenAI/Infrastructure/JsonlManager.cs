using System.Collections.Generic;
using System.IO;
using System.Text;
using Forge.OpenAI.Settings;

namespace Forge.OpenAI.Infrastructure
{

    /// <summary>
    /// Jsonp manager
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonlManager<T> where T : class, new()
    {

        private readonly List<T> _items = new List<T>();

        /// <summary>
        /// Gets the items.
        /// </summary>
        public IReadOnlyList<T> Items => _items.AsReadOnly();

        /// <summary>
        /// Loads items from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>JsonpManager</returns>
        public static JsonlManager<T> Load(Stream stream)
        {
            JsonlManager<T> result = new JsonlManager<T>();

            using (StreamReader reader = new StreamReader(stream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false), true, 1024, true))
            {
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(System.Text.Json.JsonSerializer.Deserialize<T>(line));
                }
            }

            return result;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(T item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public bool Remove(T item)
        {
            return _items.Remove(item);
        }

        /// <summary>
        /// Removes at.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }

        /// <summary>
        /// Saves items into the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public void Save(Stream stream)
        {
            using (StreamWriter writer = new StreamWriter(stream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false), 1024, leaveOpen: true))
            {
                foreach (T item in _items)
                {
                    writer.WriteLine(System.Text.Json.JsonSerializer.Serialize(item, OpenAIDefaultOptions.DefaultJsonSerializerOptions));
                }
                writer.Flush();
            }
        }

    }

}
