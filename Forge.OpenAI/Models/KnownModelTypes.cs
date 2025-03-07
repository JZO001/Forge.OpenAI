﻿using Microsoft.Extensions.Primitives;

namespace Forge.OpenAI.Models
{

    /// <summary>Known AI model types</summary>
    public static class KnownModelTypes
    {

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public const string Ada = "ada";
        public const string AdaCodeSearchCode = "ada-code-search-code";
        public const string AdaCodeSearchText = "ada-code-search-text";
        public const string AdaSearchDocument = "ada-search-document";
        public const string AdaSearchQuery = "ada-search-query";
        public const string AdaSimilarity = "ada-similarity";

        public const string Babbage = "babbage";
        public const string Babbage002 = "babbage-002";
        public const string BabbageCodeSearchCode = "babbage-code-search-code";
        public const string BabbageCodeSearchText = "babbage-code-search-text";
        public const string BabbageSearchDocument = "babbage-search-document";
        public const string BabbageSearchQuery = "babbage-search-query";
        public const string BabbageSimilarity = "babbage-similarity";

        public const string CodeCushman001 = "code-cushman-001";
        public const string CodeDavinci002 = "code-davinci-002";
        public const string CodeDavinciEdit001 = "code-davinci-edit-001";
        public const string CodeSearchAdaCode001 = "code-search-ada-code-001";
        public const string CodeSearchAdaText001 = "code-search-ada-text-001";
        public const string CodeSearchBabbageCode001 = "code-search-babbage-code-001";
        public const string CodeSearchBabbageText001 = "code-search-babbage-text-001";

        public const string Curie = "curie";
        public const string CurieInstructBeta = "curie-instruct-beta";
        public const string CurieSearchDocument = "curie-search-document";
        public const string CurieSearchQuery = "curie-search-query";
        public const string CurieSimilarity = "curie-similarity";

        public const string Davinci = "davinci";
        public const string Davinci002 = "davinci-002";
        public const string DavinciIf_3_0_0 = "davinci-if:3.0.0";
        public const string DavinciInstructBeta = "davinci-instruct-beta";
        public const string DavinciInstructBeta_2_0_0 = "davinci-instruct-beta:2.0.0";
        public const string DavinciSearchDocument = "davinci-search-document";
        public const string DavinciSearchQuery = "davinci-search-query";
        public const string DavinciSimilarity = "davinci-similarity";

        public const string Dall_E_2 = "dall-e-2";
        public const string Dall_E_3 = "dall-e-3";

        public const string Gpt3_5Turbo = "gpt-3.5-turbo";
        public const string Gpt3_5Turbo_0301 = "gpt-3.5-turbo-0301";
        public const string Gpt3_5Turbo_16k = "gpt-3.5-turbo-16k";
        public const string Gpt3_5Turbo_0613 = "gpt-3.5-turbo-0613";
        public const string Gpt3_5Turbo_1106 = "gpt-3.5-turbo-1106";
        public const string Gpt3_5Turbo_0125 = "gpt-3.5-turbo-0125";
        public const string Gpt3_5Turbo_16k_0613 = "gpt-3.5-turbo-16k-0613";
        public const string Gpt3_5Turbo_Instruct = "gpt-3.5-turbo-instruct";
        public const string Gpt3_5Turbo_Instruct_0914 = "gpt-3.5-turbo-instruct-0914";

        public const string Gpt_4 = "gpt-4";
        public const string Gpt_4_0314 = "gpt-4-0314";
        public const string Gpt_4_32k = "gpt-4-32k";
        public const string Gpt_4_32k_0314 = "gpt-4-32k-0314";
        public const string Gpt_4_0613 = "gpt-4-0613";
        public const string Gpt_4_32k_0613 = "gpt-4-32k-0613";
        public const string Gpt_4_1106_vision_preview = "gpt-4-1106-vision-preview";
        public const string Gpt_4_1106_preview = "gpt-4-1106-preview";
        public const string Gpt_4_0125_preview = "gpt-4-0125-preview";
        public const string Gpt_4_vision_preview = "gpt-4-vision-preview";
        public const string Gpt_4_turbo = "gpt-4-turbo";
        public const string Gpt_4_turbo_preview = "gpt-4-turbo-preview";
        public const string Gpt_4_turbo_2024_04_09 = "gpt-4-turbo-2024-04-09";
        public const string Gpt_4o = "gpt-4o";
        public const string Gpt_4o_latest = "chatgpt-4o-latest";
        public const string Gpt_4o_2024_05_13 = "gpt-4o-2024-05-13";
        public const string Gpt_4o_2024_08_06 = "gpt-4o-2024-08-06";
        public const string Gpt_4o_2024_11_20 = "gpt-4o-2024-11-20";
        public const string Gpt_4o_mini = "gpt-4o-mini";
        public const string Gpt_4o_mini_2024_07_18 = "gpt-4o-mini-2024-07-18";
        public const string Gpt_4o_mini_realtime_preview = "gpt-4o-mini-realtime-preview-2024-12-17";
        public const string Gpt_4o_realtime_preview = "gpt-4o-realtime-preview";
        public const string Gpt_4o_realtime_preview_2024_10_01 = "gpt-4o-realtime-preview-2024-10-01";
        public const string Gpt_4o_realtime_preview_2024_12_17 = "gpt-4o-realtime-preview-2024-12-17";
        public const string Gpt_4o_audio_preview = "gpt-4o-audio-preview";
        public const string Gpt_4o_audio_preview_2024_10_01 = "gpt-4o-audio-preview-2024-10-01";
        public const string Gpt_4o_audio_preview_2024_12_17 = "gpt-4o-audio-preview-2024-12-17";
        public const string Gpt_4o_mini_audio_preview = "gpt-4o-mini-audio-preview-2024-12-17";
        public const string Gpt_4o_mini_audio_preview_2024_12_17 = "gpt-4o-mini-audio-preview-2024-12-17";

        public const string Gpt_4_5_preview = "gpt-4.5-preview";
        public const string Gpt_4_5_preview_2024_02_27 = "gpt-4.5-preview-2025-02-27";

        public const string O1 = "o1";
        public const string O1_2024_12_17 = "o1-2024-12-17";
        public const string O1_Preview = "o1-preview";
        public const string O1_Preview_2024_09_12 = "o1-preview-2024-09-12";
        
        public const string O1_Mini = "o1-mini";
        public const string O1_Mini_2024_09_12 = "o1-mini-2024-09-12";

        public const string O3_Mini = "o3-mini";
        public const string O3_Mini_2025_01_31 = "o3-mini-2025-01-31";

        public const string IfCurieV2 = "if-curie-v2";
        public const string IfDavinciV2 = "if-davinci-v2";
        public const string IfDavinci_3_0_0 = "if-davinci:3.0.0";

        public const string TextAda001 = "text-ada-001";
        public const string TextAda_001 = "text-ada:001";

        public const string TextBabbage001 = "text-babbage-001";
        public const string TextBabbage_001 = "text-babbage:001";

        public const string TextCurie001 = "text-curie-001";
        public const string TextCurie_001 = "text-curie:001";

        public const string TextDavinci001 = "text-davinci-001";
        public const string TextDavinci002 = "text-davinci-002";
        public const string TextDavinci003 = "text-davinci-003";
        public const string TextDavinciEdit001 = "text-davinci-edit-001";
        public const string TextDavinciInsert001 = "text-davinci-insert-001";
        public const string TextDavinciInsert002 = "text-davinci-insert-002";
        public const string TextDavinci_001 = "text-davinci:001";

        public const string TextEmbeddingAda002 = "text-embedding-ada-002";
        public const string TextEmbeddingV3Small = "text-embedding-3-small";
        public const string TextEmbeddingV3Large = "text-embedding-3-large";

        public const string TextModerationStable = "text-moderation-stable";
        public const string TextModerationLatest = "text-moderation-latest";
        public const string TextModeration007 = "text-moderation-007";
        public const string TextModeration_2024_09_26 = "text-moderation-2024-09-26";

        public const string TextSearchAdaDoc001 = "text-search-ada-doc-001";
        public const string TextSearchAdaQuery001 = "text-search-ada-query-001";
        public const string TextSearchBabbageDoc001 = "text-search-babbage-doc-001";
        public const string TextSearchBabbageQuery001 = "text-search-babbage-query-001";
        public const string TextSearchCurieDoc001 = "text-search-curie-doc-001";
        public const string TextSearchCurieQuery001 = "text-search-curie-query-001";
        public const string TextSearchDavinciDoc001 = "text-search-davinci-doc-001";
        public const string TextSearchDavinciQuery001 = "text-search-davinci-query-001";

        public const string TextSimilarityAda001 = "text-similarity-ada-001";
        public const string TextSimilarityBabbage001 = "text-similarity-babbage-001";
        public const string TextSimilarityCurie001 = "text-similarity-curie-001";
        public const string TextSimilarityDavinci001 = "text-similarity-davinci-001";

        public const string Tts_1 = "tts-1";
        public const string Tts_1_1106 = "tts-1-1106";
        public const string Tts_1_hd = "tts-1-hd";
        public const string Tts_1_Hd1106 = "tts-1-hd-1106";

        public const string Whisper1 = "whisper-1";
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

}
