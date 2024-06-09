using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Forge.OpenAI.Models.Batch
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/batch/request-output
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BatchRequestOutput<T> where T : class
    {

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// A developer-provided per-request id that will be used to match outputs to inputs.
        /// </summary>
        [JsonPropertyName("custom_id")]
        public string CustomId { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        [JsonPropertyName("response")]
        public BatchRequestOutputResponse<T> Response { get; set; }

        /// <summary>
        /// For requests that failed with a non-HTTP error, this will contain more information on the cause of the failure.
        /// </summary>
        [JsonPropertyName("error")]
        public BatchRequestOutputError Error { get; set; }

    }

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/batch/request-output#batch/request-output-response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BatchRequestOutputResponse<T> where T : class
    {

        /// <summary>
        /// The HTTP status code of the response
        /// </summary>
        [JsonPropertyName("status_code")]
        public int StatusCode { get; set; }

        /// <summary>
        /// An unique identifier for the OpenAI API request. Please include this request ID when contacting support.
        /// </summary>
        [JsonPropertyName("request_id")]
        public string RequestId { get; set; }

        /// <summary>
        /// The JSON body of the response
        /// </summary>
        [JsonPropertyName("body")]
        public T Body { get; set; }

    }

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/batch/request-output#batch/request-output-response
    /// </summary>
    public class BatchRequestOutputError
    {

        /// <summary>
        /// A machine-readable error code.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// A human-readable error message.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

    }

}
