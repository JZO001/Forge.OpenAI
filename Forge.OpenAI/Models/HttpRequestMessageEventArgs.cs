using System;
using System.Net.Http;

namespace Forge.OpenAI.Models
{

    /// <summary>Contains the HttpRequestMessage</summary>
    public class HttpRequestMessageEventArgs : EventArgs
    {

        /// <summary>Initializes a new instance of the <see cref="HttpRequestMessageEventArgs" /> class.</summary>
        /// <param name="requestMessage">The request message.</param>
        /// <param name="data">The data to send out</param>
        /// <exception cref="ArgumentNullException">requestMessage</exception>
        public HttpRequestMessageEventArgs(HttpRequestMessage requestMessage, object
#if NETCOREAPP3_1_OR_GREATER
            ? 
#endif
            data)
        {
            if (requestMessage == null) throw new ArgumentNullException(nameof(requestMessage));
            RequestMessage = requestMessage;
            Data = data;
        }

        /// <summary>Gets the request message.</summary>
        /// <value>The request message.</value>
        public HttpRequestMessage RequestMessage { get; private set; }

        /// <summary>Gets the data.</summary>
        /// <value>The data.</value>
        public object
#if NETCOREAPP3_1_OR_GREATER
            ? 
#endif
            Data { get; private set; }

    }

}
