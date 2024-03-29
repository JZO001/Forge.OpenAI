﻿using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Moderations
{
    /// <summary>Represents the scores of the categories</summary>
    public class ModerationCategoriesScores
    {

        /// <summary>Initializes a new instance of the <see cref="ModerationCategoriesScores" /> class.</summary>
        public ModerationCategoriesScores()
        {
        }

        /// <summary>Gets or sets the hate score.</summary>
        /// <value>The hate.</value>
        [JsonPropertyName("hate")]
        public double Hate { get; set; }

        /// <summary>Gets or sets the hate threatening score.</summary>
        /// <value>The hate threatening.</value>
        [JsonPropertyName("hate/threatening")]
        public double HateThreatening { get; set; }

        /// <summary>Gets or sets a value indicating whether this <see cref="ModerationCategories" /> is harassment.</summary>
        /// <value>
        ///   <c>true</c> if harassment; otherwise, <c>false</c>.</value>
        [JsonPropertyName("harassment")]
        public double Harassment { get; set; }

        /// <summary>Gets or sets a value indicating whether [harassment threatening].</summary>
        /// <value>
        ///   <c>true</c> if [harassment threatening]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("harassment/threatening")]
        public double HarassmentThreatening { get; set; }

        /// <summary>Gets or sets the self harm score.</summary>
        /// <value>The self harm.</value>
        [JsonPropertyName("self-harm")]
        public double SelfHarm { get; set; }

        /// <summary>Gets or sets a value indicating whether [self harm intent].</summary>
        /// <value>
        ///   <c>true</c> if [self harm intent]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("self-harm/intent")]
        public double SelfHarmIntent { get; set; }

        /// <summary>Gets or sets a value indicating whether [self harm instructions].</summary>
        /// <value>
        ///   <c>true</c> if [self harm instructions]; otherwise, <c>false</c>.</value>
        [JsonPropertyName("self-harm/instructions")]
        public double SelfHarmInstructions { get; set; }

        /// <summary>Gets or sets the sexual score.</summary>
        /// <value>The sexual.</value>
        [JsonPropertyName("sexual")]
        public double Sexual { get; set; }

        /// <summary>Gets or sets the sexual minors score.</summary>
        /// <value>The sexual minors.</value>
        [JsonPropertyName("sexual/minors")]
        public double SexualMinors { get; set; }

        /// <summary>Gets or sets the violence score.</summary>
        /// <value>The violence.</value>
        [JsonPropertyName("violence")]
        public double Violence { get; set; }

        /// <summary>Gets or sets the violence graphic score.</summary>
        /// <value>The violence graphic.</value>
        [JsonPropertyName("violence/graphic")]
        public double ViolenceGraphic { get; set; }

    }

}
