using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Forge.OpenAI.Models.Batch
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/batch/request-input
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BatchRequestInput
    {

        public const string METHOD_POST = "POST";

        /// <summary>
        /// A developer-provided per-request id that will be used to match outputs to inputs. Must be unique for each request in a batch.
        /// </summary>
        [Required]
        [JsonPropertyName("custom_id")]
        public string CustomId { get; set; }

        /// <summary>
        /// The HTTP method to be used for the request. Currently only POST is supported.
        /// </summary>
        [Required]
        [JsonPropertyName("method")]
        public string Method { get; set; } = METHOD_POST;

        /// <summary>
        /// The OpenAI API relative URL to be used for the request. Currently /v1/chat/completions, /v1/embeddings, and /v1/completions are supported.
        /// Use OpenAIOpetions to set the URL
        /// </summary>
        [Required]
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        [Required]
        [JsonPropertyName("body")]
        public object Body { get; set; }

    }

}
