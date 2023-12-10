namespace Forge.OpenAI.Models.Images
{

    /// <summary>Helpers class to convert enumerator to string format</summary>
    public static class ImageSize
    {

        /// <summary>
        /// 256x256
        /// </summary>
        public const string IMAGE_SIZE_256_X_256 = "256x256";

        /// <summary>
        /// 256x256
        /// </summary>
        public const string IMAGE_SIZE_512_X_512 = "512x512";

        /// <summary>
        /// 256x256
        /// </summary>
        public const string IMAGE_SIZE_1024_X_1024 = "1024x1024";

        /// <summary>
        /// 1792x1024
        /// </summary>
        public const string IMAGE_SIZE_1792_X_1024 = "1792x1024";

        /// <summary>
        /// 1024x1792
        /// </summary>
        public const string IMAGE_SIZE_1024_X_1792 = "1024x1792";

        /// <summary>Converts the image size enum to string.</summary>
        /// <param name="imageSizeEnum">The image size enum.</param>
        /// <returns>Size in string format</returns>
        public static string ConvertImageSizeEnumToString(ImageSizeEnum imageSizeEnum)
        {
            switch (imageSizeEnum)
            {
                case ImageSizeEnum.Size_256_x_256:
                    return IMAGE_SIZE_256_X_256;
                case ImageSizeEnum.Size_512_x_512:
                    return IMAGE_SIZE_512_X_512;
                case ImageSizeEnum.Size_1024_x_1024:
                    return IMAGE_SIZE_1024_X_1024;
                case ImageSizeEnum.Size_1792_x_1024:
                    return IMAGE_SIZE_1792_X_1024;
                case ImageSizeEnum.Size_1024_x_1792:
                    return IMAGE_SIZE_1024_X_1792;
                default:
                    return IMAGE_SIZE_1024_X_1024;
            }
        }

    }

    /// <summary>
    /// Represents an image size
    /// https://platform.openai.com/docs/api-reference/images/create#images-create-size
    /// </summary>
    public enum ImageSizeEnum
    {
        /// <summary>
        /// 256x256
        /// </summary>
        Size_256_x_256,
        /// <summary>
        /// 512x512
        /// </summary>
        Size_512_x_512,
        /// <summary>
        /// 1024x1024
        /// </summary>
        Size_1024_x_1024,
        /// <summary>
        /// 1792x1024
        /// </summary>
        Size_1792_x_1024,
        /// <summary>
        /// 1024x1792
        /// </summary>
        Size_1024_x_1792,
    }

}
