using Forge.OpenAI.Models.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Files
{

    /// <summary>Represents the response of the file listing request</summary>
    public class FileListResponse : ResponseBase
    {

        /// <summary>Initializes a new instance of the <see cref="FileListResponse" /> class.</summary>
        public FileListResponse()
        {
        }

        /// <summary>Gets the list of files.</summary>
        /// <value>The files.</value>
        [JsonPropertyName("data")]
        public IReadOnlyList<FileData> Files { get; set; }

    }

}
