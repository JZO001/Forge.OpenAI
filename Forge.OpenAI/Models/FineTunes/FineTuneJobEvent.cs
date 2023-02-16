using System;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.FineTunes
{

    /// <summary>Represents a fine tune job event</summary>
    public class FineTuneJobEvent
    {

        /// <summary>Initializes a new instance of the <see cref="FineTuneJobEvent" /> class.</summary>
        public FineTuneJobEvent()
        {
        }

        /// <summary>Gets the object type.</summary>
        /// <value>The object.</value>
        [JsonPropertyName("object")]
        public string Object { get; set; }

        /// <summary>Gets the created at unix time.</summary>
        /// <value>The created at unix time.</value>
        [JsonPropertyName("created_at")]
        public int CreatedAtUnixTime { get; set; }

        /// <summary>Gets the created at.</summary>
        /// <value>The created at.</value>
        [JsonIgnore]
        public DateTime CreatedAt => DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTime).DateTime;

        /// <summary>Gets the log level of the message.</summary>
        /// <value>The level.</value>
        [JsonPropertyName("level")]
        public string Level { get; set; }

        /// <summary>Gets the message.</summary>
        /// <value>The message.</value>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>Performs an implicit conversion from <see cref="FineTuneJobEvent" /> to <see cref="System.String" />.</summary>
        /// <param name="data">The data.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(FineTuneJobEvent data) => data?.ToString();

    }

}
