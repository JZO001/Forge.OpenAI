using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.VectorStores
{

    /// <summary>
    /// The chunking strategy used to chunk the file(s). If not set, will use the auto strategy. Only applicable if file_ids is non-empty.
    /// https://platform.openai.com/docs/api-reference/vector-stores/create#vector-stores-create-chunking_strategy
    /// </summary>
    public class ChunkingStrategy
    {

        public const string TYPE_AUTO = "auto";
        public const string TYPE_STATIC = "static";

        /// <summary>
        /// Auto Chunking Strategy: the default strategy.This strategy currently uses a max_chunk_size_tokens of 800 and chunk_overlap_tokens of 400. Always auto.
        /// 
        /// Static Chunking Strategy: always static.
        /// 
        /// </summary>
        [Required]
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the static data in case of static type.
        /// </summary>
        [JsonPropertyName("static")]
        public ChunkingStrategyStatic Static { get; set; }

    }

    /// <summary>
    /// https://platform.openai.com/docs/api-reference/vector-stores/create#vector-stores-create-chunking_strategy
    /// </summary>
    public class ChunkingStrategyStatic
    {

        /// <summary>
        /// The maximum number of tokens in each chunk. The default value is 800. The minimum value is 100 and the maximum value is 4096.
        /// </summary>
        [Required]
        [JsonPropertyName("max_chunk_size_tokens")]
        public int MaxChunkSizeTokens { get; set; }

        /// <summary>
        /// The number of tokens that overlap between chunks. The default value is 400. Note that the overlap must not exceed half of max_chunk_size_tokens.
        /// </summary>
        [Required]
        [JsonPropertyName("chunk_overlap_tokens")]
        public int ChunkOverlapTokens { get; set; }

    }

}
