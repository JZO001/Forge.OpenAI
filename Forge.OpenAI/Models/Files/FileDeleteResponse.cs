using Forge.OpenAI.Models.Common;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Files
{

    /// <summary>Represents the response of a file deletion request</summary>
    public class FileDeleteResponse : ResponseBase
    {

        /// <summary>Initializes a new instance of the <see cref="FileDeleteResponse" /> class.</summary>
        public FileDeleteResponse()
        {
        }

        /// <summary>Gets a value indicating whether the was deleted or not.</summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.</value>
        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }

    }

}
