using Forge.OpenAI.Models.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

/* Unmerged change from project 'Forge.OpenAI (net461)'
Before:
using System.Linq;
After:
using System.Linq;
using Forge;
using Forge.OpenAI;
using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Moderation;
using Forge.OpenAI.Models.Moderations;
*/
using System.Linq;

namespace Forge.OpenAI.Models.Moderations
{

    /// <summary>Represents a moderation request</summary>
    public class ModerationRequest : RequestBase
    {

        /// <summary>Initializes a new instance of the <see cref="ModerationRequest" /> class.</summary>
        public ModerationRequest()
        {
            Model = KnownModelTypes.TextModerationLatest;
        }

        /// <summary>Initializes a new instance of the <see cref="ModerationRequest" /> class.</summary>
        /// <param name="inputTextsToClassify">The inputs.</param>
        public ModerationRequest(IEnumerable<string> inputTextsToClassify) : this()
        {
            InputTextsToClassify.AddRange(inputTextsToClassify);
        }

        /// <summary>Initializes a new instance of the <see cref="ModerationRequest" /> class.</summary>
        /// <param name="inputTextsToClassify">The inputs.</param>
        /// <param name="model">The model.</param>
        public ModerationRequest(IEnumerable<string> inputTextsToClassify, string model) : this(inputTextsToClassify)
        {
            Model = string.IsNullOrEmpty(model) ? KnownModelTypes.TextModerationLatest : model;
        }

        /// <summary>
        /// The input text(s) to classify <br />
        /// <see href="https://beta.openai.com/docs/api-reference/moderations/create#moderations/create-input" />
        /// </summary>
        [Required]
        [JsonPropertyName("input")]
        public List<string> InputTextsToClassify { get; set; } = new List<string>();

        /// <summary>
        /// For convenience, if you are only requesting a single text, set it here
        /// </summary>
        [JsonIgnore]
        public string InputTextToClassify
        {
            get => InputTextsToClassify?.FirstOrDefault();
            set
            {
                if (InputTextsToClassify == null) InputTextsToClassify = new List<string>();
                if (value == null)
                {
                    InputTextsToClassify.Clear();
                }
                else
                {
                    if (InputTextsToClassify.Count == 1)
                    {
                        InputTextsToClassify[0] = value;
                    }
                    else
                    {
                        InputTextsToClassify.Clear();
                        InputTextsToClassify.Add(value);
                    }
                }
            }
        }

        /// <summary>
        /// Two content moderations models are available: text-moderation-stable and text-moderation-latest. <br />
        /// The default is text-moderation-latest which will be automatically upgraded over time. <br />
        /// This ensures you are always using our most accurate model. <br />
        /// If you use text-moderation-stable, we will provide advanced notice before updating the model. <br />
        /// Accuracy of text-moderation-stable may be slightly lower than for text-moderation-latest. <br />
        /// <see href="https://beta.openai.com/docs/api-reference/moderations/create#moderations/create-model" />
        /// </summary>
        [Required]
        [JsonPropertyName("model")]
        public string Model { get; set; }

    }

}
