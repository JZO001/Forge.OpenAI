using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Shared
{

    /// <summary>
    /// An array of content parts with a defined type, each can be of type text or images can be passed with image_url or image_file. Image types are only supported on Vision-compatible models.
    /// </summary>
    public class MessageContent
    {

        /// <summary>Initializes a new instance of the <see cref="MessageContent" /> class.</summary>
        /// <param name="type">The type.</param>
        /// <exception cref="System.ArgumentNullException">type</exception>
        public MessageContent(string type)
        {
            if (string.IsNullOrWhiteSpace(type)) throw new ArgumentNullException(nameof(type));
            Type = type;
        }

        /// <summary>Initializes a new instance of the <see cref="MessageContent" /> class.</summary>
        /// <param name="messageContentImageFile">The message content image file.</param>
        /// <exception cref="System.ArgumentNullException">messageContentImageFile</exception>
        public MessageContent(MessageContentImageFile messageContentImageFile) : this("image_file")
        {
            if (messageContentImageFile == null) throw new ArgumentNullException(nameof(messageContentImageFile));

            ImageFile = messageContentImageFile;
        }

        /// <summary>Initializes a new instance of the <see cref="MessageContent" /> class.</summary>
        /// <param name="type">The type of the content part.</param>
        /// <param name="messageContentImageUrl">The message content image URL.</param>
        /// <exception cref="System.ArgumentNullException">messageContentImageUrl</exception>
        public MessageContent(string type, MessageContentImageUrl messageContentImageUrl) : this(type)
        {
            if (messageContentImageUrl == null) throw new ArgumentNullException(nameof(messageContentImageUrl));

            ImageUrl = messageContentImageUrl;
        }

        /// <summary>The type of the content part.</summary>
        /// <value>The type.</value>
        [Required]
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("image_file")]
        public MessageContentImageFile ImageFile { get; set; }

        [JsonPropertyName("image_url")]
        public MessageContentImageUrl ImageUrl { get; set; }

    }

    public class MessageContentImageFile
    {

        /// <summary>Initializes a new instance of the <see cref="MessageContentImageFile" /> class.</summary>
        /// <param name="fileId">The file identifier.</param>
        /// <exception cref="System.ArgumentNullException">fileId</exception>
        public MessageContentImageFile(string fileId)
        {
            if (string.IsNullOrWhiteSpace(fileId)) throw new ArgumentNullException(nameof(fileId));

            FileId = fileId;
        }

        /// <summary>Initializes a new instance of the <see cref="MessageContentImageFile" /> class.</summary>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="detail">The detail.</param>
        public MessageContentImageFile(string fileId, string detail) : this(fileId)
        {
            Detail = detail;
        }

        /// <summary>The File ID of the image in the message content. Set purpose="vision" when uploading the File if you need to later display the file content.</summary>
        /// <value>The file identifier.</value>
        [Required]
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }

        /// <summary>Specifies the detail level of the image if specified by the user. low uses fewer tokens, you can opt in to high resolution using high.</summary>
        /// <value>The detail.</value>
        [JsonPropertyName("detail")]
        public string Detail { get; set; }

    }

    public class MessageContentImageUrl
    {

        /// <summary>Initializes a new instance of the <see cref="MessageContentImageUrl" /> class.</summary>
        /// <param name="url">The URL.</param>
        /// <exception cref="System.ArgumentNullException">url</exception>
        public MessageContentImageUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));

            Url = url;
        }

        /// <summary>Initializes a new instance of the <see cref="MessageContentImageUrl" /> class.</summary>
        /// <param name="url">The URL.</param>
        /// <param name="detail">The detail.</param>
        public MessageContentImageUrl(string url, string detail) : this(url)
        {
            Detail = detail;
        }

        /// <summary>The external URL of the image, must be a supported image types: jpeg, jpg, png, gif, webp.</summary>
        /// <value>The URL.</value>
        [Required]
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>Specifies the detail level of the image. low uses fewer tokens, you can opt in to high resolution using high. Default value is auto</summary>
        /// <value>The detail.</value>
        [JsonPropertyName("detail")]
        public string Detail { get; set; }

    }

}
