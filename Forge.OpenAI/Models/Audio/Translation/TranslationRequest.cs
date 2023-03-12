using Forge.OpenAI.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Audio.Translation
{

    /// <summary>Represents a transcription request</summary>
    public class TranslationRequest : RequestBase
    {

        /// <summary>Initializes a new instance of the <see cref="TranslationRequest" /> class.</summary>
        public TranslationRequest()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="TranslationRequest" /> class.</summary>
        /// <param name="audioFile">The audio file.</param>
        /// <param name="model">The model.</param>
        /// <exception cref="System.ArgumentNullException">file</exception>
        public TranslationRequest(BinaryContentData audioFile, string model)
        {
            if (audioFile == null) throw new ArgumentNullException(nameof(audioFile));

            AudioFile = audioFile;
            Model = model ?? KnownModelTypes.Whisper1;
        }

        /// <summary>
        /// The audio file to transcribe, in one of these formats: mp3, mp4, mpeg, mpga, m4a, wav, or webm.
        /// <see href="https://platform.openai.com/docs/api-reference/audio/create#audio/create-file" />
        /// </summary>
        /// <value>The file.</value>
        [Required]
        public BinaryContentData AudioFile { get; set; }

        /// <summary>
        /// ID of the model to use. Only whisper-1 is currently available.
        /// <see href="https://platform.openai.com/docs/api-reference/audio/create#audio/create-model" />
        /// </summary>
        [Required]
        [JsonPropertyName("model")]
        public string Model { get; set; } = KnownModelTypes.Whisper1;

        /// <summary>
        /// An optional text to guide the model's style or continue a previous audio segment. The prompt should match the audio language.
        /// <see href="https://platform.openai.com/docs/api-reference/audio/create#audio/create-prompt" />
        /// </summary>
        /// <value>The prompts.</value>
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }

        /// <summary>
        /// The format of the transcript output, in one of these options: json, text, srt, verbose_json, or vtt.
        /// <see href="https://platform.openai.com/docs/api-reference/audio/create#audio/create-response_format" />
        /// </summary>
        [JsonPropertyName("response_format")]
        public string ResponseFormat { get; set; }

        /// <summary>
        /// The sampling temperature, between 0 and 1. Higher values like 0.8 will make the output more random, while lower values like 0.2 will make it more focused and deterministic. If set to 0, the model will use log probability to automatically increase the temperature until certain thresholds are hit.
        /// <see href="https://platform.openai.com/docs/api-reference/audio/create#audio/create-temperature" />
        /// </summary>
        /// <value>The temperature.</value>
        [JsonPropertyName("temperature")]
        public double? Temperature { get; set; }

    }

}
