using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.VectorStores
{

    /// <summary>
    /// The expiration policy for a vector store.
    /// https://platform.openai.com/docs/api-reference/vector-stores/object#vector-stores/object-expires_after
    /// </summary>
    public class ExpiresAfter
    {

        public const string ANCHOR_LAST_ACTIVE_AT = "last_active_at";

        /// <summary>Anchor timestamp after which the expiration policy applies. Supported anchors:</summary>
        /// <value>The anchor.</value>
        [Required]
        [JsonPropertyName("anchor")]
        public string Anchor { get; set; } = ANCHOR_LAST_ACTIVE_AT;

        /// <summary>
        /// The number of days after the anchor time that the vector store will expire.
        /// </summary>
        [Required]
        [JsonPropertyName("days")]
        public int Days { get; set; }

    }

}
