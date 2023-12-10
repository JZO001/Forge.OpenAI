using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using Forge.OpenAI.Infrastructure.Serialization;

namespace Forge.OpenAI.Models
{

    /// <summary>Describe the nature of the issue</summary>
    public class Error
    {

        /// <summary>Gets the human readable error message.</summary>
        /// <value>The message.</value>
        [JsonIgnore] 
        public string Message { get; private set; }

        /// <summary>Gets the human readable error messages.</summary>
        /// <value>The messages.</value>
        [JsonIgnore] 
        public IReadOnlyList<string> Messages { get; private set; }

        /// <summary>For internal use</summary>
        /// <value>The message object.</value>
        [JsonPropertyName("message")]
        [JsonConverter(typeof(MessageJsonConverter))]
        public object InternalUseMessages
        {
            set
            {
                switch (value)
                {
                    case string s:
                        Message = s;
                        Messages = new List<string> { s };
                        break;

                    case List<object> list when list.TrueForAll(i => i is JsonElement):
                        Messages = list.Cast<JsonElement>().Select(e => e.GetString()).ToList();
                        Message = string.Join(Environment.NewLine, Messages);
                        break;
                }
            }
        }

        /// <summary>Gets the type of the message.</summary>
        /// <value>The type.</value>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>Gets the parameter.</summary>
        /// <value>The parameter.</value>
        [JsonPropertyName("param")]
        public object Param { get; set; }

        /// <summary>Gets the code.</summary>
        /// <value>The code.</value>
        [JsonPropertyName("code")]
        public object Code { get; set; }

        /// <summary>Performs an implicit conversion from <see cref="Error" /> to <see cref="System.String" />.</summary>
        /// <param name="data">The data.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(Error data) => data?.ToString();

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => JsonSerializer.Serialize(this, GetType());

    }

}
