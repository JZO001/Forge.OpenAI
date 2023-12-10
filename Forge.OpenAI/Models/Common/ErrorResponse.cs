using System.Text.Json;

namespace Forge.OpenAI.Models.Common
{

    /// <summary>Represents an error type response from OpenAI remote API</summary>
    public class ErrorResponse : ResponseBase
    {

        /// <summary>Performs an implicit conversion from <see cref="ErrorResponse" /> to <see cref="System.String" />.</summary>
        /// <param name="data">The data.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(ErrorResponse data) => data?.ToString();

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => JsonSerializer.Serialize(this, GetType());

    }

}
