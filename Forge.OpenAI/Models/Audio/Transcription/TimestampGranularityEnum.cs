namespace Forge.OpenAI.Models.Audio.Transcription
{

    /// <summary>
    ///   <a href="https://platform.openai.com/docs/api-reference/audio/createTranscription#audio-createtranscription-timestamp_granularities">https://platform.openai.com/docs/api-reference/audio/createTranscription#audio-createtranscription-timestamp_granularities</a>
    /// </summary>
    public enum TimestampGranularityEnum
    {
        None = 0,
        Word,
        Segment
    }

}
