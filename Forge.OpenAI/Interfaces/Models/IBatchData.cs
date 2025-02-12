using Forge.OpenAI.Models.Batch;
using System;
using System.Collections.Generic;

namespace Forge.OpenAI.Interfaces.Models
{

    public interface IBatchData
    {

        /// <summary>
        /// Id of the batch.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The object type, which is always batch.
        /// </summary>
        string Object { get; }

        /// <summary>
        /// The OpenAI API endpoint used by the batch.
        /// </summary>
        string Endpoint { get; }

        /// <summary>
        /// https://platform.openai.com/docs/api-reference/batch/object#batch/object-errors
        /// </summary>
        IReadOnlyList<BatchError> Errors { get; }

        /// <summary>
        /// The ID of the input file for the batch.
        /// </summary>
        string InputFileId
        {
            get;
        }

        /// <summary>
        /// The time frame within which the batch should be processed.
        /// </summary>
        string CompletionWindow
        {
            get;
        }

        /// <summary>
        /// The current status of the batch.
        /// </summary>
        string Status
        {
            get;
        }

        /// <summary>
        /// The ID of the file containing the outputs of successfully executed requests.
        /// </summary>
        string OutputFileId
        {
            get;
        }

        /// <summary>
        /// The ID of the file containing the outputs of requests with errors.
        /// </summary>
        string ErrorFileId
        {
            get;
        }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch was created.
        /// </summary>
        int CreatedAtUnixTimeSeconds
        {
            get;
        }

        /// <summary>
        /// The time when the result was generated.
        /// </summary>
        DateTime CreatedAt { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch started processing.
        /// </summary>
        int? InProgressAtUnixTimeSeconds
        {
            get;
        }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch started processing.
        /// </summary>
        DateTime? InProgressAt { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch will expire.
        /// </summary>
        int? ExpiresAtUnixTimeSeconds
        {
            get;
        }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch will expire.
        /// </summary>
        DateTime? ExpiresAt { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch started finalizing.
        /// </summary>
        int? FinalizingAtUnixTimeSeconds
        {
            get;
        }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch started finalizing.
        /// </summary>
        DateTime? FinalizingAt { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch was completed.
        /// </summary>
        int? CompletedAtUnixTimeSeconds
        {
            get;
        }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch was completed.
        /// </summary>
        DateTime? CompletedAt { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch failed.
        /// </summary>
        int? FailedAtUnixTimeSeconds
        {
            get;
        }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch failed.
        /// </summary>
        DateTime? FailedAt { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch expired.
        /// </summary>
        int? ExpiredAtUnixTimeSeconds
        {
            get;
        }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch expired.
        /// </summary>
        DateTime? ExpiredAt { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch started cancelling.
        /// </summary>
        int? CancellingAtUnixTimeSeconds
        {
            get;
        }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the batch started cancelling.
        /// </summary>
        DateTime? CancellingAt { get; }

        /// <summary>
        /// The request counts for different statuses within the batch.
        /// </summary>
        BatchRequestCount RequestCounts
        {
            get;
        }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object. 
        /// This can be useful for storing additional information about the object in a structured format. 
        /// Keys can be a maximum of 64 characters long and values can be a maxium of 512 characters long.
        /// </summary>
        IReadOnlyDictionary<string, string> Metadata
        {
            get;
        }

    }

}
