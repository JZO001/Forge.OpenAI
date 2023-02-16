using System.Text.Json;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Common
{

    /// <summary>Represents an error type response from OpenAI remote API</summary>
    public class ErrorResponse
    {

        /// <summary>Initializes a new instance of the <see cref="ErrorResponse" /> class.</summary>
        public ErrorResponse()
        {
        }

        /// <summary>Gets the error.</summary>
        /// <value>The error.</value>
        [JsonPropertyName("error")]
        public Error Error { get; set; }

        /// <summary>Performs an implicit conversion from <see cref="ErrorResponse" /> to <see cref="System.String" />.</summary>
        /// <param name="data">The data.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(ErrorResponse data) => data?.ToString();

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => JsonSerializer.Serialize(this, GetType());

    }

    /// <summary>Describe the nature of the issue</summary>
    public class Error
    {

        /// <summary>Initializes a new instance of the <see cref="Error" /> class.</summary>
        public Error()
        {
        }

        /// <summary>Gets the human readable error message.</summary>
        /// <value>The message.</value>
        [JsonPropertyName("message")]
        public string Message { get; set; }

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
