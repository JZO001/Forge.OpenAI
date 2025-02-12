using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Runs;
using System;
using System.Collections.Generic;

namespace Forge.OpenAI.Interfaces.Models
{

    public interface IRunStepData
    {
        /// <summary>
        /// The identifier, which can be referenced in API endpoints.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Object type, ie: text_completion, file, fine-tune, list, etc
        /// </summary>
        string Object { get; }

        /// <summary>
        /// The time when the result was generated in unix epoch format
        /// </summary>
        int? CreatedAtUnixTime { get; }

        /// <summary>
        /// The time when the result was generated.
        /// </summary>
        DateTime? CreatedAt { get; }

        /// <summary>
        /// The ID of the assistant used for execution of this run.
        /// </summary>
        string AssistantId { get; }

        /// <summary>
        /// The thread ID that this run belongs to.
        /// </summary>
        string ThreadId { get; }

        /// <summary>
        /// The ID of the run that this run step is a part of.
        /// </summary>
        string RunId { get; }

        /// <summary>
        /// The type of run step.
        /// </summary>
        string Type { get; }

        /// <summary>
        /// The status of the run step. For possibel values, see consts in RunData.
        /// </summary>
        string Status { get; }

        /// <summary>
        /// The details of the run step.
        /// </summary>
        StepDetails StepDetails { get; }

        /// <summary>
        /// The last error associated with this run step. Will be null if there are no errors.
        /// </summary>
        OpenAI.Models.Runs.Error LastError
        {
            get;
        }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step expired. A step is considered expired if the parent run is expired.
        /// </summary>
        int? ExpiresAtUnixTimeSeconds
        {
            get;
        }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step expired. A step is considered expired if the parent run is expired.
        /// </summary>
        DateTime? ExpiresAt { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step was cancelled.
        /// </summary>
        int? CancelledAtUnixTimeSeconds
        {
            get;
        }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step was cancelled.
        /// </summary>
        DateTime? CancelledAt { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step failed.
        /// </summary>
        int? FailedAtUnixTimeSeconds
        {
            get;
        }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step failed.
        /// </summary>
        DateTime? FailedAt { get; }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step completed.
        /// </summary>
        int? CompletedAtUnixTimeSeconds
        {
            get;
        }

        /// <summary>
        /// The Unix timestamp (in seconds) for when the run step completed.
        /// </summary>
        DateTime? CompletedAt { get; }

        /// <summary>
        /// Set of 16 key-value pairs that can be attached to an object.
        /// This can be useful for storing additional information about the object in a structured format.
        /// Keys can be a maximum of 64 characters long and values can be a maximum of 512 characters long.
        /// </summary>
        IReadOnlyDictionary<string, string> Metadata
        {
            get;
        }

        /// <summary>
        /// Usage statistics related to the run step. This value will be `null` while the run step's status is `in_progress`.
        /// </summary>
        Usage Usage
        {
            get;
        }

    }

}
