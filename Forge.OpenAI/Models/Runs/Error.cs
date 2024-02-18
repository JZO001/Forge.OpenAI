using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Runs
{

    public class Error
    {

        /// <summary>
        /// One of server_error or rate_limit_exceeded.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// A human-readable description of the error.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

    }

}
