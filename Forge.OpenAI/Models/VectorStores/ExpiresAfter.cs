using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.VectorStores
{

    /// <summary>
    ///   <para>The expiration policy for a vector store.</para>
    ///   <para>
    ///     <a href="https://platform.openai.com/docs/api-reference/vector-stores/object#vector-stores/object-expires_after">https://platform.openai.com/docs/api-reference/vector-stores/object#vector-stores/object-expires_after</a>
    ///   </para>
    /// </summary>
    public class ExpiresAfter
    {

        /// <summary>Anchor timestamp after which the expiration policy applies. Supported anchors:</summary>
        /// <value>The anchor.</value>
        [JsonPropertyName("anchor")]
        public string Anchor { get; set; } = "last_active_at";

        [JsonPropertyName("days")]
        public int Days { get; set; }

    }

}
