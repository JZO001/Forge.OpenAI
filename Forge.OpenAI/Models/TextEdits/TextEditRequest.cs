using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Settings;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.TextEdits
{

    /// <summary>Represents a text edit request</summary>
    public class TextEditRequest : RequestBase
    {

        /// <summary>Initializes a new instance of the <see cref="TextEditRequest" /> class.</summary>
        public TextEditRequest()
        {
            Model = OpenAIDefaultOptions.DefaultTextEditModel;
        }

        /// <summary>
        /// Creates a new edit request for the provided input, instruction, and parameters.
        /// </summary>
        /// <param name="inputTextForEditing">The input text to use as a starting point for the edit.</param>
        /// <param name="instruction">The instruction that tells the model how to edit the prompt.</param>
        /// <param name="numberOfEditedTexts">How many edits to generate for the input and instruction.</param>
        /// <param name="temperature">
        /// What sampling temperature to use. Higher values means the model will take more risks.
        /// Try 0.9 for more creative applications, and 0 (argmax sampling) for ones with a well-defined answer.
        /// We generally recommend altering this or top_p but not both.
        /// </param>
        /// <param name="topP">
        /// An alternative to sampling with temperature, called nucleus sampling, where the model considers the
        /// results of the tokens with top_p probability mass.
        /// So 0.1 means only the tokens comprising the top 10% probability mass are considered.
        /// We generally recommend altering this or temperature but not both.
        /// </param>
        /// <param name="model">
        /// ID of the model to use. Defaults to text-davinci-edit-001.
        /// You can use the text-davinci-edit-001 or code-davinci-edit-001 model with this endpoint.
        /// </param>
        public TextEditRequest(
            string inputTextForEditing,
            string instruction,
            int? numberOfEditedTexts = null,
            double? temperature = null,
            double? topP = null,
#if NETCOREAPP3_1_OR_GREATER
            string? model = null
#else
            string model = null
#endif
            )
        {
            Model = model ?? OpenAIDefaultOptions.DefaultTextEditModel;
            InputTextForEditing = inputTextForEditing;
            Instruction = instruction;
            NumberOfEditedTexts = numberOfEditedTexts;
            Temperature = temperature;
            TopP = topP;
        }

        /// <summary>
        /// ID of the model to use. Defaults to text-davinci-edit-001.
        /// </summary>
        [Required]
        [JsonPropertyName("model")]
        public string Model { get; set; }

        /// <summary>
        /// The input text to use as a starting point for the edit.
        /// </summary>
        [Required]
        [JsonPropertyName("input")]
        public string InputTextForEditing { get; set; }

        /// <summary>
        /// The instruction that tells the model how to edit the prompt.
        /// </summary>
        [Required]
        [JsonPropertyName("instruction")]
        public string Instruction { get; set; }

        /// <summary>
        /// How many edits to generate for the input and instruction.
        /// </summary>
        [JsonPropertyName("n")]
        public int? NumberOfEditedTexts { get; set; }

        /// <summary>
        /// What sampling temperature to use. Higher values means the model will take more risks.
        /// Try 0.9 for more creative applications, and 0 (argmax sampling) for ones with a well-defined answer.
        /// We generally recommend altering this or top_p but not both.
        /// </summary>
        [JsonPropertyName("temperature")]
        public double? Temperature { get; set; }

        /// <summary>
        /// An alternative to sampling with temperature, called nucleus sampling, where the model considers the
        /// results of the tokens with top_p probability mass.
        /// So 0.1 means only the tokens comprising the top 10% probability mass are considered.
        /// We generally recommend altering this or temperature but not both.
        /// </summary>
        [JsonPropertyName("top_p")]
        public double? TopP { get; set; }

    }

}
