using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Audio.Speech
{

    /// <summary>https://platform.openai.com/docs/api-reference/audio/createSpeech</summary>
    public class SpeechRequest
    {

        public const string VOICE_ALLOY = "alloy";
        public const string VOICE_ECHO = "echo";
        public const string VOICE_FABLE = "fable";
        public const string VOICE_NOVA = "nova";
        public const string VOICE_ONYX = "onyx";
        public const string VOICE_SHIMMER = "shimmer";

        public const string RESPONSE_FORMAT_MP3 = "mp3";
        public const string RESPONSE_FORMAT_OPUS = "opus";
        public const string RESPONSE_FORMAT_AAC = "aac";
        public const string RESPONSE_FORMAT_FLAC = "flac";

        /// <summary>
        /// One of the available TTS models: tts-1 or tts-1-hd
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; } = KnownModelTypes.Tts_1;

        /// <summary>
        /// The text to generate audio for. The maximum length is 4096 characters.
        /// </summary>
        [JsonPropertyName("input")]
        public string Input { get; set; }

        /// <summary>
        /// The voice to use when generating the audio. Supported voices are alloy, echo, fable, onyx, nova, and shimmer
        /// </summary>
        [JsonPropertyName("voice")]
        public string Voice { get; set; } = VOICE_ALLOY;

        /// <summary>
        /// The format to audio in. Supported formats are mp3, opus, aac, and flac. Defaults to mp3.
        /// </summary>
        [JsonPropertyName("responseFormat")]
        public string ResponseFormat { get; set; }

        /// <summary>
        /// The speed of the generated audio. Select a value from 0.25 to 4.0. Defaults to 1.0.
        /// </summary>
        [JsonPropertyName("speed")]
        public float? Speed { get; set; }

    }

}
