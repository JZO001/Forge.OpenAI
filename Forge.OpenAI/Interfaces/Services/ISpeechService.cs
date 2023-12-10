using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Audio.Speech;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>Represents the speech service</summary>
    public interface ISpeechService
    {

        /// <summary>Create a sőeech.</summary>
        /// <param name="speechRequest">The request parameters.</param>
        /// <param name="resultStream">The result stream.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   Output Stream, which can receive the data from the underlying network stream.
        /// </returns>
        Task<HttpOperationResult<Stream>> CreateSpeechAsync(SpeechRequest speechRequest, Stream resultStream, CancellationToken cancellationToken = default);

    }

}
