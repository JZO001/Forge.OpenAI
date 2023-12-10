using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Settings;

namespace Forge.OpenAI.Models.TextCompletions
{

    /// <summary>
    /// Mostly matches the parameters in
    /// <see href="https://beta.openai.com/docs/api-reference/completions">the OpenAI docs</see>,
    /// although some have been renames or expanded into single/multiple properties for ease of use.
    /// </summary>
    [Obsolete]
    public class TextCompletionRequest : RequestBase
    {

        /// <summary>Initializes a new instance of the <see cref="TextCompletionRequest" /> class.</summary>
        public TextCompletionRequest()
        {
            Model = OpenAIDefaultOptions.DefaultTextCompletionModel;
        }

        /// <summary>
        /// Creates a new <see cref="TextCompletionRequest"/>, inheriting any parameters set in <paramref name="basedOn"/>.
        /// </summary>
        /// <param name="basedOn">The <see cref="TextCompletionRequest"/> to copy</param>
        public TextCompletionRequest(TextCompletionRequest basedOn)
        {
            if (basedOn == null) throw new ArgumentNullException(nameof(basedOn));

            Model = basedOn.Model;
            Prompts = basedOn.Prompts;
            Suffix = basedOn.Suffix;
            MaxTokens = basedOn.MaxTokens;
            Temperature = basedOn.Temperature;
            TopP = basedOn.TopP;
            NumberOfChoicesPerPrompt = basedOn.NumberOfChoicesPerPrompt;
            PresencePenalty = basedOn.PresencePenalty;
            FrequencyPenalty = basedOn.FrequencyPenalty;
            LogProbabilities = basedOn.LogProbabilities;
            Echo = basedOn.Echo;
            StopSequences = basedOn.StopSequences;
            LogitBias = basedOn.LogitBias;
            BestOf = basedOn.BestOf;
            User = basedOn.User;
        }

        /// <summary>
        /// Creates a new <see cref="TextCompletionRequest"/> with the specified parameters
        /// </summary>
        /// <param name="model">ID of the model to use. You can use the List models API to see all of your available models, or see our Model overview for descriptions of them.</param>
        /// <param name="prompt">The prompt to generate from</param>
        /// <param name="prompts">The prompts to generate from</param>
        /// <param name="suffix">The suffix that comes after a completion of inserted text.</param>
        /// <param name="maxTokens">How many tokens to complete to. Can return fewer if a stop sequence is hit.</param>
        /// <param name="temperature">What sampling temperature to use. Higher values means the model will take more risks.
        /// Try 0.9 for more creative applications, and 0 (argmax sampling) for ones with a well-defined answer.
        /// It is generally recommend to use this or <paramref name="topP"/> but not both.</param>
        /// <param name="topP">An alternative to sampling with temperature, called nucleus sampling,
        /// where the model considers the results of the tokens with top_p probability mass.
        /// So 0.1 means only the tokens comprising the top 10% probability mass are considered.
        /// It is generally recommend to use this or <paramref name="temperature"/> but not both.</param>
        /// <param name="numberOfChoicesPerPrompts">How many different choices to request for each prompt.</param>
        /// <param name="presencePenalty">The scale of the penalty applied if a token is already present at all.
        /// Should generally be between 0 and 1, although negative numbers are allowed to encourage token reuse.</param>
        /// <param name="frequencyPenalty">The scale of the penalty for how often a token is used.
        /// Should generally be between 0 and 1, although negative numbers are allowed to encourage token reuse.</param>
        /// <param name="logProbabilities">Include the log probabilities on the logProbabilities most likely tokens,
        /// which can be found in Completions -> <see cref="Choice.LogProbabilities"/>.
        /// So for example, if logprobs is 10, the API will return a list of the 10 most likely tokens. If logprobs is supplied,
        /// the API will always return the logprob of the sampled token, so there may be up to logprobs+1 elements in the response.</param>
        /// <param name="echo">Echo back the prompt in addition to the completion.</param>
        /// <param name="stopSequences">One or more sequences where the API will stop generating further tokens.
        /// The returned text will not contain the stop sequence.</param>
        /// <param name="logitBias">A dictionary of logit bias to influence the probability of generating a token.</param>
        /// <param name="bestOf">Returns the top bestOf results based on the best probability.</param>
        /// <param name="user">A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse.</param>
        public TextCompletionRequest(
            string model,
            string prompt = null,
            IEnumerable<string> prompts = null,
            string suffix = null,
            int? maxTokens = null,
            double? temperature = null,
            double? topP = null,
            int? numberOfChoicesPerPrompts = null,
            double? presencePenalty = null,
            double? frequencyPenalty = null,
            int? logProbabilities = null,
            bool? echo = null,
            IEnumerable<string> stopSequences = null,
            Dictionary<string, double> logitBias = null,
            int? bestOf = null,
            string user = null)
        {
            if (prompt != null)
            {
                Prompt = prompt;
            }
            else if (prompts != null)
            {
                Prompts.AddRange(prompts);
            }
            else
            {
                throw new ArgumentNullException($"Missing required {nameof(prompt)}(s)");
            }

            Model = model ?? OpenAIDefaultOptions.DefaultTextCompletionModel;
            Suffix = suffix;
            MaxTokens = maxTokens;
            Temperature = temperature;
            TopP = topP;
            NumberOfChoicesPerPrompt = numberOfChoicesPerPrompts;
            PresencePenalty = presencePenalty;
            FrequencyPenalty = frequencyPenalty;
            LogProbabilities = logProbabilities;
            Echo = echo;
            StopSequences = stopSequences?.ToArray();
            LogitBias = logitBias;
            BestOf = bestOf;
            User = user;
        }

        /// <summary>
        /// ID of the model to use.<br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-model" />
        /// </summary>
        [Required]
        [JsonPropertyName("model")]
        public string Model { get; set; }

        /// <summary>
        /// The prompt(s) to generate completions for, encoded as a string, array of strings, array of tokens, or array of token arrays.
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-prompt" />
        /// </summary>
        [JsonPropertyName("prompt")]
        public List<string> Prompts { get; set; } = new List<string>();

        /// <summary>
        /// For convenience, if you are only requesting a single prompt, set it here
        /// </summary>
        [JsonIgnore]
        public string Prompt
        {
            get => Prompts?.FirstOrDefault();
            set
            {
                if (Prompts == null) Prompts = new List<string>();
                if (value == null)
                {
                    Prompts.Clear();
                }
                else
                {
                    if (Prompts.Count == 1)
                    {
                        Prompts[0] = value;
                    }
                    else
                    {
                        Prompts.Clear();
                        Prompts.Add(value);
                    }
                }
            }
        }

        /// <summary>
        /// The suffix that comes after a completion of inserted text. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-suffix" />
        /// </summary>
        [JsonPropertyName("suffix")]
        public string Suffix { get; set; }

        /// <summary>
        /// The maximum number of <a href="https://beta.openai.com/tokenizer">tokens</a> to generate in the completion. <br/> 
        ///  The token count of your prompt plus max_tokens cannot exceed the model's context length. Most models have a context length of 2048 tokens (except for the newest models, which support 4096). <br/>
        ///  <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-max_tokens" />
        /// </summary>
        [JsonPropertyName("max_tokens")]
        public int? MaxTokens { get; set; }

        /// <summary>
        /// What <a href="https://towardsdatascience.com/how-to-sample-from-language-models-682bceb97277">sampling temperature</a> to use. Higher values means the model will take more risks. <br/>
        /// Try 0.9 for more creative applications, and 0 (argmax sampling) for ones with a well-defined answer. <br/>
        /// We generally recommend altering this or top_p but not both. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-temperature" />
        /// </summary>
        [JsonPropertyName("temperature")]
        public double? Temperature { get; set; }

        /// <summary>
        /// An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. <br/>
        /// So 0.1 means only the tokens comprising the top 10% probability mass are considered. <br/>
        /// We generally recommend altering this or temperature but not both. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-top_p" />
        /// </summary>
        [JsonPropertyName("top_p")]
        public double? TopP { get; set; }

        /// <summary>
        /// Number between -2.0 and 2.0. Positive values penalize new tokens based on whether they
        /// appear in the text so far, increasing the model's likelihood to talk about new topics.
        /// </summary>
        [JsonPropertyName("presence_penalty")]
        public double? PresencePenalty { get; set; }

        /// <summary>
        /// Number between -2.0 and 2.0. <br/>
        /// Positive values penalize new tokens based on their existing frequency in the text so far, decreasing the model's likelihood to repeat the same line verbatim. <br/>
        /// <a href="https://beta.openai.com/docs/api-reference/parameter-details">See more information about frequency and presence penalties.</a> <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-frequency_penalty" />
        /// </summary>
        [JsonPropertyName("frequency_penalty")]
        public double? FrequencyPenalty { get; set; }

        /// <summary>
        /// How many completions to generate for each prompt. <br/>
        /// Note: Because this parameter generates many completions, it can quickly consume your token quota. <br/>
        /// Use carefully and ensure that you have reasonable settings for max_tokens and stop. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-n" />
        /// </summary>
        [JsonPropertyName("n")]
        public int? NumberOfChoicesPerPrompt { get; set; }

        /// <summary>
        /// Whether to stream back partial progress.  <br/>
        /// If set, tokens will be sent as data-only <a href="https://developer.mozilla.org/en-US/docs/Web/API/Server-sent_events/Using_server-sent_events#Event_stream_format">server-sent</a> events as they become available, with the stream terminated by a data: [DONE] message.  <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-stream" />
        /// </summary>
        [JsonPropertyName("stream")]
        public bool Stream { get; set; }

        /// <summary>
        /// Include the log probabilities on the logprobs most likely tokens, as well the chosen tokens. <br/>
        /// For example, if logprobs is 5, the API will return a list of the 5 most likely tokens. <br/>
        /// The API will always return the logprob of the sampled token, so there may be up to logprobs+1 elements in the response. <br/>
        /// The maximum value for logprobs is 5. If you need more than this, please contact us through our  <a href="https://help.openai.com/">Help center</a> and describe your use case. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-logprobs" />
        /// </summary>
        [JsonPropertyName("logprobs")]
        public int? LogProbabilities { get; set; }

        /// <summary>
        /// Echo back the prompt in addition to the completion <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-echo" />
        /// </summary>
        [JsonPropertyName("echo")]
        public bool? Echo { get; set; }

        /// <summary>
        /// Up to 4 sequences where the API will stop generating further tokens. <br/>
        /// The returned text will not contain the stop sequence. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-stop" />
        /// </summary>
        [JsonPropertyName("stop")]
        public string[] StopSequences { get; set; }

        /// <summary>
        /// The stop sequence where the API will stop generating further tokens. The returned text will not contain the stop sequence.
        /// For convenience, if you are only requesting a single stop sequence, set it here
        /// </summary>
        [JsonIgnore]
        public string StopSequence
        {
            get => StopSequences?.FirstOrDefault();
            set
            {
                if (value == null)
                {
                    StopSequences = Array.Empty<string>();
                }
                else
                {
                    if (StopSequences.Length == 1)
                    {
                        StopSequences[0] = value;
                    }
                    else
                    {
                        StopSequences = new[] { value };
                    }
                }
            }
        }

        /// <summary>
        /// Modify the likelihood of specified tokens appearing in the completion. <br/>
        /// Accepts a json object that maps tokens(specified by their token ID in the GPT tokenizer) to an associated bias value from -100 to 100. <br/>
        /// You can use this <a href="https://beta.openai.com/tokenizer?view=bpe"> tokenizer tool </a> (which works for both GPT-2 and GPT-3) to convert text to token IDs. <br/>
        /// Mathematically, the bias is added to the logits generated by the model prior to sampling. <br/>
        /// The exact effect will vary per model, but values between -1 and 1 should decrease or increase likelihood of selection; values like -100 or 100 should result in a ban or exclusive selection of the relevant token. <br/>
        /// As an example, you can pass  <br/>
        /// to prevent the  <![CDATA[<|endoftext|>]]>  token from being generated. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-logit_bias" />
        /// </summary>
        [JsonPropertyName("logit_bias")]
        public Dictionary<string, double> LogitBias { get; set; }

        /// <summary>
        /// Generates best_of completions server-side and returns the "best" (the one with the highest log probability per token). <br/>
        /// Results cannot be streamed.<br/>
        /// When used with n, best_of controls the number of candidate completions and n specifies how many to return – best_of must be greater than n. <br/>
        /// Note: Because this parameter generates many completions, it can quickly consume your token quota. <br/>
        /// Use carefully and ensure that you have reasonable settings for max_tokens and stop. <br/>
        /// <see href="https://beta.openai.com/docs/api-reference/completions/create#completions/create-best_of" />
        /// </summary>
        [JsonPropertyName("best_of")]
        public int? BestOf { get; set; }

        /// <summary>
        /// A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse. <a href="https://beta.openai.com/docs/guides/safety-best-practices/end-user-ids">Learn more</a>.
        /// </summary>
        [JsonPropertyName("user")]
        public string User { get; set; }

    }

}
