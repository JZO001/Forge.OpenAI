using Forge.OpenAI.Models.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Images
{

    /// <summary>Represents the general response of an image operation request</summary>
    public abstract class ImageResponseBase : ResponseBase
    {

        /// <summary>Initializes a new instance of the <see cref="ImageResponseBase" /> class.</summary>
        protected ImageResponseBase()
        {
        }

        /// <summary>Gets the created/edited/modified image data.</summary>
        /// <value>The image data.</value>
        [JsonPropertyName("data")]
        public List<ImageData> ImageData { get; set; }

    }

}
