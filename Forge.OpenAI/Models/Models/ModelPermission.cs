using System;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Models
{

    /// <summary>Represents the model permissions</summary>
    public class ModelPermission
    {

        /// <summary>Initializes a new instance of the <see cref="ModelPermission" /> class.</summary>
        public ModelPermission()
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

        /// <summary>Gets or sets the created at unix time.</summary>
        /// <value>The created at unix time.</value>
        [JsonPropertyName("created")]
        public long CreatedAtUnixTime { get; set; }

        /// <summary>Gets the creation time</summary>
        /// <value>The created at.</value>
        [JsonIgnore]
        public DateTime CreatedAt => DateTimeOffset.FromUnixTimeSeconds(CreatedAtUnixTime).DateTime;

        /// <summary>Gets or sets a value indicating whether [allow create engine].</summary>
        /// <value>
        ///   <c>true</c> if [allow create engine]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("allow_create_engine")]
        public bool AllowCreateEngine { get; set; }

        /// <summary>Gets or sets a value indicating whether [allow sampling].</summary>
        /// <value>
        ///   <c>true</c> if [allow sampling]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("allow_sampling")]
        public bool AllowSampling { get; set; }

        /// <summary>Gets or sets a value indicating whether [allow log probs].</summary>
        /// <value>
        ///   <c>true</c> if [allow log probs]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("allow_logprobs")]
        public bool AllowLogProbs { get; set; }

        /// <summary>Gets or sets a value indicating whether [allow search indices].</summary>
        /// <value>
        ///   <c>true</c> if [allow search indices]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("allow_search_indices")]
        public bool AllowSearchIndices { get; set; }

        /// <summary>Gets or sets a value indicating whether [allow view].</summary>
        /// <value>
        ///   <c>true</c> if [allow view]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("allow_view")]
        public bool AllowView { get; set; }

        /// <summary>Gets or sets a value indicating whether [allow fine tuning].</summary>
        /// <value>
        ///   <c>true</c> if [allow fine tuning]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("allow_fine_tuning")]
        public bool AllowFineTuning { get; set; }

        /// <summary>Gets or sets the organization.</summary>
        /// <value>The organization.</value>
        [JsonPropertyName("organization")]
        public string Organization { get; set; }

        /// <summary>Gets or sets the group.</summary>
        /// <value>The group.</value>
        [JsonPropertyName("group")]
        public object Group { get; set; }

        /// <summary>Gets or sets a value indicating whether this instance is blocking.</summary>
        /// <value>
        ///   <c>true</c> if this instance is blocking; otherwise, <c>false</c>.</value>
        [JsonPropertyName("is_blocking")]
        public bool IsBlocking { get; set; }

    }

}
