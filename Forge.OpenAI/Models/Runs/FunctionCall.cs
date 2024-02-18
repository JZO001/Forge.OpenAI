using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Runs
{

    /// <summary>The function definition.</summary>
    public class FunctionCall
    {

        /// <summary>
        /// The name of the function.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The arguments that the model expects you to pass to the function.
        /// </summary>
        [JsonPropertyName("arguments")]
        public string Arguments { get; set; }

        /// <summary>
        /// The output of the function. This will be null if the outputs have not been submitted yet.
        /// </summary>
        [JsonPropertyName("output")]
        public string Output { get; set; }

    }

}
