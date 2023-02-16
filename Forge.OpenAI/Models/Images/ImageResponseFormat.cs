namespace Forge.OpenAI.Models.Images
{

    /// <summary>Helper class to convert ImageResponseFormatEnum to string</summary>
    public static class ImageResponseFormat
    {

        /// <summary>URL</summary>
        public const string IMAGE_FORMAT_URL = "url";

        /// <summary>Base64 json</summary>
        public const string IMAGE_FORMAT_BASE64_JSON = "b64_json";

        /// <summary>Converts the image response format enum to string.</summary>
        /// <param name="imageResponseFormatEnum">The image response format enum.</param>
        /// <returns>String format</returns>
        public static string ConvertImageResponseFormatEnumToString(ImageResponseFormatEnum imageResponseFormatEnum)
        {
            switch (imageResponseFormatEnum)
            {
                case ImageResponseFormatEnum.B64_Json:
                    return IMAGE_FORMAT_BASE64_JSON;
                default:
                    return IMAGE_FORMAT_URL;
            }
        }

    }

    /// <summary>Image format in the response</summary>
    public enum ImageResponseFormatEnum
    {

        /// <summary>URL</summary>
        Url,
        /// <summary>Base64 json</summary>
        B64_Json
    }

}
