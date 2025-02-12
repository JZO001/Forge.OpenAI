using Forge.OpenAI.Models.Common;
using System.Text.Json.Serialization;
using System;
using Forge.OpenAI.Interfaces.Models;

namespace Forge.OpenAI.Models.Files
{

    /// <summary>Represents a shared response content for file data</summary>
    public abstract class FileDataResponseBase : ResponseBase, IFileData
    {

        /// <summary>Initializes a new instance of the <see cref="FileDataResponseBase" /> class.</summary>
        protected FileDataResponseBase()
        {
        }

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>Gets the size of the file.</summary>
        /// <value>The size.</value>
        [JsonPropertyName("bytes")]
        public int FileSize { get; set; }

        /// <summary>Gets the created at unix time.</summary>
        /// <value>The created at unix time.</value>
        [JsonPropertyName("created_at")]
        public int CreatedAtUnixTime { get; set; }

        /// <summary>Gets the created at.</summary>
        /// <value>The created at.</value>
        [JsonIgnore]
        public DateTime CreatedAt => DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTime).DateTime;

        /// <summary>Gets the name of the file.</summary>
        /// <value>The name of the file.</value>
        [JsonPropertyName("filename")]
        public string FileName { get; set; }

        /// <summary>Gets the purpose of the file existence.</summary>
        /// <value>The purpose.</value>
        [JsonPropertyName("purpose")]
        public string Purpose { get; set; }

        /// <summary>Gets the status.</summary>
        /// <value>The status.</value>
        [Obsolete]
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>Gets the status details.</summary>
        /// <value>The status details.</value>
        [Obsolete]
        [JsonPropertyName("status_details")]
        public string StatusDetails { get; set; }

    }

}
