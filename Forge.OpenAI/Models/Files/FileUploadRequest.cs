using Forge.OpenAI.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Forge.OpenAI.Models.Files
{

    /// <summary>Represents a file upload request</summary>
    public class FileUploadRequest : RequestBase
    {

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
        public string Purpose { get; set; }

    }

}
