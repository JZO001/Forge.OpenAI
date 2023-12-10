using Forge.OpenAI.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Files
{

    /// <summary>Represents a file upload request</summary>
    public class FileUploadRequest : RequestBase
    {

        /// <summary>
        /// https://platform.openai.com/docs/api-reference/files/object#files/object-purpose
        /// </summary>
        public const string PURPOSE_FINE_TUNE = "fine-tune";
        public const string PURPOSE_FINE_TUNE_RESULTS = "fine-tune-results";
        public const string PURPOSE_ASSISTANTS = "assistants";
        public const string PURPOSE_ASSISTANTS_OUTPUT = "assistants_output";

        /// <summary>Initializes a new instance of the <see cref="FileUploadRequest" /> class.</summary>
        public FileUploadRequest()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="FileUploadRequest" /> class.</summary>
        /// <param name="file">The file.</param>
        /// <param name="purpose">The purpose.</param>
        /// <exception cref="System.ArgumentNullException">file
        /// or
        /// purpose</exception>
        public FileUploadRequest(BinaryContentData file, string purpose)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));
            if (string.IsNullOrWhiteSpace(purpose)) throw new ArgumentNullException(nameof(purpose));
            File = file;
            Purpose = purpose;
        }

        /// <summary>Gets or sets the file data.</summary>
        /// <value>The file.</value>
        [Required]
        public BinaryContentData File { get; set; }

        /// <summary>Gets or sets the purpose of the file.</summary>
        /// <value>The purpose.</value>
        [Required]
        public string Purpose { get; set; } = PURPOSE_FINE_TUNE;

        /// <summary>Sets the set purpose as enum.</summary>
        /// <value>The set purpose as enum.</value>
        /// <exception cref="System.NotImplementedException">PurposeTypeEnum {value} not implemented</exception>
        [JsonIgnore]
        public PurposeTypeEnum SetPurposeAsEnum
        {
            set
            {
                switch (value)
                {
                    case PurposeTypeEnum.FineTune:
                        Purpose = PURPOSE_FINE_TUNE;
                        break;

                    case PurposeTypeEnum.FineTuneResults:
                        Purpose = PURPOSE_FINE_TUNE_RESULTS;
                        break;

                    case PurposeTypeEnum.Assistants:
                        Purpose = PURPOSE_ASSISTANTS;
                        break;

                    case PurposeTypeEnum.AssistantsOutput:
                        Purpose = PURPOSE_ASSISTANTS_OUTPUT;
                        break;

                    default:
                        throw new NotImplementedException($"PurposeTypeEnum {value} not implemented");
                }
            }
        }

    }

}
