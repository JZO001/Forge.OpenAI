using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Models
{

    /// <summary>Represents an OpenAI engine</summary>
    public class Model
    {

        /// <summary>Initializes a new instance of the <see cref="Model" /> class.</summary>
        public Model()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Model" /> class.</summary>
        /// <param name="id">The identifier.</param>
        public Model(string id)
        {
            Id = id;
        }

        /// <summary>
        /// The default Model to use in the case no other is specified. Defaults to <see cref="Davinci"/>
        /// </summary>
        public static Model Default => Davinci;

        /// <summary>
        /// The most powerful, largest engine available, although the speed is quite slow.<para/>
        /// Good at: Complex intent, cause and effect, summarization for audience
        /// https://platform.openai.com/docs/models/gpt-3
        /// </summary>
        public static Model Davinci => new Model(KnownModelTypes.TextDavinci003) { OwnedBy = "openai" };

        /// <summary>
        /// The 2nd most powerful engine, a bit faster than <see cref="Davinci"/>, and a bit faster.<para/>
        /// Good at: Language translation, complex classification, text sentiment, summarization.
        /// https://platform.openai.com/docs/models/gpt-3
        /// </summary>
        public static Model Curie => new Model(KnownModelTypes.TextCurie001) { OwnedBy = "openai" };

        /// <summary>
        /// The 2nd fastest engine, a bit more powerful than <see cref="Ada"/>, and a bit slower.<para/>
        /// Good at: Moderate classification, semantic search classification
        /// https://platform.openai.com/docs/models/gpt-3
        /// </summary>
        public static Model Babbage => new Model(KnownModelTypes.TextBabbage001) { OwnedBy = "openai" };

        /// <summary>
        /// The smallest, fastest engine available, although the quality of results may be poor.<para/>
        /// Good at: Parsing text, simple classification, address correction, keywords
        /// https://platform.openai.com/docs/models/gpt-3
        /// </summary>
        public static Model Ada => new Model(KnownModelTypes.TextAda001) { OwnedBy = "openai" };

        /// <summary>
        /// Allows a model to be implicitly cast to the string of its id.
        /// </summary>
        /// <param name="model">The <see cref="Model"/> to cast to a string.</param>
        public static implicit operator string(Model model) => model?.Id;

        /// <summary>
        /// Allows a string to be implicitly cast as a <see cref="Model"/>
        /// </summary>
        public static implicit operator Model(string name) => new Model(name);

        /// <inheritdoc />
        public override string ToString() => Id;

        /// <summary>Gets the model identifier.</summary>
        /// <value>The identifier.</value>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>Gets the object type.</summary>
        /// <value>The object.</value>
        [JsonPropertyName("object")]
        public string Object { get; set; }

        /// <summary>
        /// The time when the result was generated in unix epoch format
        /// </summary>
        [JsonPropertyName("created")]
        public long? CreatedUnixTime { get; set; }

        /// <summary>The time when the result was generated.</summary>
        [JsonIgnore]

        public DateTime? Created => CreatedUnixTime.HasValue ? (DateTime?)(DateTimeOffset.FromUnixTimeSeconds(CreatedUnixTime.Value).DateTime) : null;

        /// <summary>Gets the owner of the model.</summary>
        /// <value>The owned by.</value>
        [JsonPropertyName("owned_by")]
        public string OwnedBy { get; set; }

        /// <summary>Gets the permissions.</summary>
        /// <value>The permissions.</value>
        [JsonPropertyName("permission")]
        public List<ModelPermission> Permissions { get; set; }

        /// <summary>Gets the root model.</summary>
        /// <value>The root.</value>
        [JsonPropertyName("root")]
        public string Root { get; set; }

        /// <summary>Gets the parent model.</summary>
        /// <value>The parent.</value>
        [JsonPropertyName("parent")]
        public string Parent { get; set; }

    }

}
