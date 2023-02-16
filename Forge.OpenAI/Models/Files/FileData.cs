using System;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Files
{

    /// <summary>Represent the data of an uploaded file</summary>
    public class FileData
    {

        /// <summary>Initializes a new instance of the <see cref="FileData" /> class.</summary>
        public FileData()
        {
        }

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>Gets the object type.</summary>
        /// <value>The object.</value>
        [JsonPropertyName("object")]
        public string Object { get; set; }

        /// <summary>Gets the size of the file.</summary>
        /// <value>The size.</value>
        [JsonPropertyName("bytes")]
        public int FileSize { get; set; }

        /// <summary>Gets or sets the created unix time.</summary>
        /// <value>The created unix time.</value>
        [JsonPropertyName("created_at")]
        public int CreatedUnixTime { get; set; }

        /// <summary>Gets the created at.</summary>
        /// <value>The created at.</value>
        [JsonIgnore]
        public DateTime CreatedAt => DateTimeOffset.FromUnixTimeSeconds(CreatedUnixTime).DateTime;

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
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>Gets the status details.</summary>
        /// <value>The status details.</value>
        [JsonPropertyName("status_details")]
        public string StatusDetails { get; set; }

        /// <summary>Performs an implicit conversion from <see cref="FileData" /> to <see cref="System.String" />.</summary>
        /// <param name="fileData">The file data.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(FileData fileData) => fileData?.Id;

    }

}
