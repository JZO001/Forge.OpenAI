using System;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.FineTunes
{

    /// <summary>Represents a fine tune job event</summary>
    [Obsolete]
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

    }

}
