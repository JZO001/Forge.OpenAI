using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models
{

    /// <summary>Represent the meta info of an image/file object</summary>
    public class BinaryContentData
    {

        /// <summary>Initializes a new instance of the <see cref="BinaryContentData" /> class.</summary>
        public BinaryContentData()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="BinaryContentData" /> class.</summary>
        /// <param name="contentName">Name of the image/file object.</param>
        /// <exception cref="ArgumentNullException">contentName</exception>
        public BinaryContentData(string contentName)
        {
            if (string.IsNullOrWhiteSpace(contentName)) throw new ArgumentNullException(nameof(contentName));
            ContentName = contentName;
        }

        /// <summary>Initializes a new instance of the <see cref="BinaryContentData" /> class.</summary>
        /// <param name="contentName">Name of the image/file object.</param>
        /// <param name="streamOfSource">The source stream.
        /// Warning! Request class is not responsible to dispose the given stream.</param>
        /// <exception cref="ArgumentNullException">imageStream</exception>
        public BinaryContentData(string contentName, Stream streamOfSource)
            : this(contentName)
        {
            if (streamOfSource == null) throw new ArgumentNullException(nameof(streamOfSource));
            SourceStream = streamOfSource;
        }

        /// <summary>Initializes a new instance of the <see cref="BinaryContentData" /> class.</summary>
        /// <param name="contentName">Name of the image.</param>
        /// <param name="contentOfSource">Content of the image/file in byte array</param>
        public BinaryContentData(string contentName, byte[] contentOfSource)
            : this(contentName)
        {
            SourceContent = contentOfSource;
        }

        /// <summary>Gets or sets the name of the data.</summary>
        /// <value>The name of the data.</value>
        [Required]
        public string ContentName { get; set; }

        /// <summary>
        /// Gets or sets the data stream.
        /// Do not set ImageStream and ImageContent at the same time.
        /// If you do, ImageContent will be used to send.
        /// </summary>
        /// <value>The data stream.</value>
        [JsonIgnore]
        public Stream SourceStream { get; set; }

        /// <summary>
        /// Gets or sets the content of the data.
        /// Do not set ImageStream and ImageContent at the same time.
        /// If you do, ImageContent will be used to send.
        /// </summary>
        /// <value>The content of the data.</value>
        public byte[] SourceContent { get; set; }

    }

}
