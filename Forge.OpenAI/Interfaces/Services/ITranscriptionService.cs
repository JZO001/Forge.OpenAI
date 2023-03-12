using Forge.OpenAI.Models.Audio.Transcription;
using Forge.OpenAI.Models.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>Represents the transcription service</summary>
    public interface ITranscriptionService
    {

        /// <summary>Request an audio file transcripted and get back the recognised text</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   TranscriptionResponse
        /// </returns>
        Task<HttpOperationResult<TranscriptionResponse>> GetAsync(TranscriptionRequest request, CancellationToken cancellationToken);

    }

}
