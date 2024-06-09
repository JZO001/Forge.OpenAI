using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Forge.OpenAI.Models.Common;

namespace Forge.OpenAI.Models.Batch
{

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/batch/create
    /// </summary>
    public class CreateBatchRequest : RequestBase
    {

        /// <summary>
        /// The completion window 24h
        /// </summary>
        public const string COMPLETION_WINDOW_24H = "24h";

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBatchRequest"/> class.
        /// </summary>
        /// <param name="inputFileId">The input file identifier.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <param name="completionWindow">The completion window.</param>
        public CreateBatchRequest(string inputFileId, string endpoint, string completionWindow = COMPLETION_WINDOW_24H)
        {
            InputFileId = inputFileId;
            Endpoint = endpoint;
            CompletionWindow = completionWindow;
        }

        /// <summary>
        /// The ID of an uploaded file that contains requests for the new batch.
        /// See upload file for how to upload a file.
        /// Your input file must be formatted as a JSONL file, and must be uploaded with the purpose batch. The file can contain up to 50,000 requests, and can be up to 100 MB in size.
        /// https://platform.openai.com/docs/api-reference/batch/create#batch-create-input_file_id
        /// </summary>
        [Required]
        [JsonPropertyName("input_file_id")]
        public string InputFileId { get; set; }

        /// <summary>
        /// The endpoint to be used for all requests in the batch. 
        /// Currently /v1/chat/completions, /v1/embeddings, and /v1/completions are supported. 
        /// Note that /v1/embeddings batches are also restricted to a maximum of 50,000 embedding inputs across all requests in the batch.
        /// https://platform.openai.com/docs/api-reference/batch/create#batch-create-endpoint
        /// </summary>
        [Required]
        [JsonPropertyName("endpoint")]
        public string Endpoint { get; set; }

        /// <summary>
        /// The time frame within which the batch should be processed. Currently only 24h is supported.
        /// https://platform.openai.com/docs/api-reference/batch/create#batch-create-completion_window
        /// </summary>
        [Required]
        [JsonPropertyName("completion_window")]
        public string CompletionWindow { get; set; }

        /// <summary>
        /// Optional custom metadata for the batch.
        /// </summary>
        [JsonPropertyName("metadata")]
        public IDictionary<string, string> Metadata { get; set; }

    }

}
