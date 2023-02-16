using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Moderations
{

    /// <summary>Represents the moderation categories</summary>
    public class ModerationCategories
    {

        /// <summary>Initializes a new instance of the <see cref="ModerationCategories" /> class.</summary>
        public ModerationCategories()
        {
        }

        /// <summary>Gets or sets a value indicating whether this <see cref="ModerationCategories" /> is hate.</summary>
        /// <value>
        ///   <c>true</c> if hate; otherwise, <c>false</c>.</value>
        [JsonPropertyName("hate")]
        public bool Hate { get; set; }

        /// <summary>Gets or sets a value indicating whether [hate threatening].</summary>
        /// <value>
        ///   <c>true</c> if [hate threatening]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("hate/threatening")]
        public bool HateThreatening { get; set; }

        /// <summary>Gets or sets a value indicating whether [self harm].</summary>
        /// <value>
        ///   <c>true</c> if [self harm]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("self-harm")]
        public bool SelfHarm { get; set; }

        /// <summary>Gets or sets a value indicating whether this <see cref="ModerationCategories" /> is sexual.</summary>
        /// <value>
        ///   <c>true</c> if sexual; otherwise, <c>false</c>.</value>
        [JsonPropertyName("sexual")]
        public bool Sexual { get; set; }

        /// <summary>Gets or sets a value indicating whether [sexual minors].</summary>
        /// <value>
        ///   <c>true</c> if [sexual minors]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("sexual/minors")]
        public bool SexualMinors { get; set; }

        /// <summary>Gets or sets a value indicating whether this <see cref="ModerationCategories" /> is violence.</summary>
        /// <value>
        ///   <c>true</c> if violence; otherwise, <c>false</c>.</value>
        [JsonPropertyName("violence")]
        public bool Violence { get; set; }

        /// <summary>Gets or sets a value indicating whether [violence graphic].</summary>
        /// <value>
        ///   <c>true</c> if [violence graphic]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("violence/graphic")]
        public bool ViolenceGraphic { get; set; }

    }

}
