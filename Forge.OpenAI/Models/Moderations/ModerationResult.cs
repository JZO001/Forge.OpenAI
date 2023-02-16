using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Moderations
{

    /// <summary>Represents the result of a moderation input</summary>
    public class ModerationResult
    {

        /// <summary>Initializes a new instance of the <see cref="ModerationResult" /> class.</summary>
        public ModerationResult()
        {
        }

        /// <summary>Gets or sets the categories.</summary>
        /// <value>The categories.</value>
        [JsonPropertyName("categories")]
        public ModerationCategories Categories { get; set; }

        /// <summary>Gets or sets the categories scores.</summary>
        /// <value>The scores.</value>
        [JsonPropertyName("category_scores")]
        public ModerationCategoriesScores Scores { get; set; }

        /// <summary>Gets or sets a value indicating whether this <see cref="ModerationResult" /> is flagged.</summary>
        /// <value>
        ///   <c>true</c> if flagged; otherwise, <c>false</c>.</value>
        [JsonPropertyName("flagged")]
        public bool Flagged { get; set; }

    }

}
