using Forge.OpenAI.Models.Common;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Embeddings
{

    /// <summary>Reqpresents and embedding request. <a href="https://platform.openai.com/docs/api-reference/embeddings/create">https://platform.openai.com/docs/api-reference/embeddings/create</a></summary>
    public class EmbeddingsRequest : RequestBase
    {

        /// <summary>Initializes a new instance of the <see cref="EmbeddingsRequest" /> class.</summary>
        public EmbeddingsRequest()
        {
            Model = KnownModelTypes.TextEmbeddingAda002;
        }

        /// <summary>Initializes a new instance of the <see cref="EmbeddingsRequest" /> class.</summary>
        /// <param name="model">The model.</param>
        public EmbeddingsRequest(string model)
        {
            Model = string.IsNullOrWhiteSpace(model) ? KnownModelTypes.TextEmbeddingAda002 : model;
        }

        /// <summary>Initializes a new instance of the <see cref="EmbeddingsRequest" /> class.</summary>
        /// <param name="model">The model.</param>
        /// <param name="inputTextsForEmbeddings">The input.</param>
        public EmbeddingsRequest(string model, List<string> inputTextsForEmbeddings) 
            : this(model)
        {
            InputTextsForEmbeddings.AddRange(inputTextsForEmbeddings);
        }

        /// <summary>Initializes a new instance of the <see cref="EmbeddingsRequest" /> class.</summary>
        /// <param name="model">The model.</param>
        /// <param name="inputTextsForEmbeddings">The input.</param>
        /// <param name="user">The user.</param>
        public EmbeddingsRequest(string model, List<string> inputTextsForEmbeddings, string user) 
            : this(model, inputTextsForEmbeddings)
        {
            User = user;
        }

        /// <summary>
        /// ID of the model to use. <br />
        /// You can use the List models API to see all of your available models, or see our Model overview for descriptions of them. <br />
        /// <see href="https://beta.openai.com/docs/api-reference/embeddings#embeddings/create-model" />
        /// </summary>
        [Required]
        [JsonPropertyName("model")]
        public string Model { get; set; }

        /// <summary>
        /// Input text to get embeddings for, encoded as a string or array of tokens. <br />
        /// To get embeddings for multiple inputs in a single request, pass an array of strings or array of token arrays. <br />
        /// Each input must not exceed 8192 tokens in length. <br />
        /// <see href="https://beta.openai.com/docs/api-reference/embeddings/create#embeddings/create-input" />
        /// </summary>
        [Required]
        [JsonPropertyName("input")]
        public List<string> InputTextsForEmbeddings { get; set; } = new List<string>();

        /// <summary>
        /// For convenience, if you are only requesting a single text, set it here
        /// </summary>
        [JsonIgnore]
        public string InputTextForEmbeddings
        {
            get => InputTextsForEmbeddings?.FirstOrDefault();
            set
            {
                if (InputTextsForEmbeddings == null) InputTextsForEmbeddings = new List<string>();
                if (value == null)
                {
                    InputTextsForEmbeddings.Clear();
                }
                else
                {
                    if (InputTextsForEmbeddings.Count == 1)
                    {
                        InputTextsForEmbeddings[0] = value;
                    }
                    else
                    {
                        InputTextsForEmbeddings.Clear();
                        InputTextsForEmbeddings.Add(value);
                    }
                }
            }
        }

        /// <summary>
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse. <br/>
        /// <a href="https://beta.openai.com/docs/api-reference/images/create#images/create-user">Learn more</a>. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/embeddings/create#embeddings/create-user" />
        /// </summary>
        [JsonPropertyName("user")]
        public string User { get; set; }

    }

}
