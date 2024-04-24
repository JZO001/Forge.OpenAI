using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Shared
{

    /// <summary>
    ///   <a href="https://platform.openai.com/docs/api-reference/runs/object#runs/object-incomplete_details">https://platform.openai.com/docs/api-reference/runs/object#runs/object-incomplete_details</a>
    ///   <a href="https://platform.openai.com/docs/api-reference/messages/object#messages/object-incomplete_details">https://platform.openai.com/docs/api-reference/messages/object#messages/object-incomplete_details</a>
    /// </summary>
    public class IncompleteDetails
    {

        /// <summary>Gets or sets the reason.</summary>
        /// <value>The reason.</value>
        [JsonPropertyName("reason")]
        public string Reason { get; set; }

    }

}
