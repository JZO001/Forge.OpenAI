using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Files;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Services
{

    /// <summary>Represents the file service</summary>
    public interface IFileService
    {

        /// <summary>Gets the file list asynchronously</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FileListResponse
        /// </returns>
        Task<HttpOperationResult<FileListResponse>> GetFileListAsync(CancellationToken cancellationToken = default);

        /// <summary>Uploads a file asynchronously</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FileUploadResponse
        /// </returns>
        Task<HttpOperationResult<FileUploadResponse>> UploadFileAsync(FileUploadRequest request, CancellationToken cancellationToken = default);

        /// <summary>Gets the file data asynchronous.</summary>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FileDataResponse
        /// </returns>
        Task<HttpOperationResult<FileDataResponse>> GetFileDataAsync(string fileId, CancellationToken cancellationToken = default);

        /// <summary>Downloads the file asynchronous.</summary>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="resultStream">The result stream.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   Output Stream, which can receive the data from the underlying network stream.
        /// </returns>
        Task<HttpOperationResult<Stream>> DownloadFileAsync(string fileId, Stream resultStream, CancellationToken cancellationToken = default);

        /// <summary>Deletes a file by id asynchronously</summary>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FileDeleteResponse
        /// </returns>
        Task<HttpOperationResult<FileDeleteResponse>> DeleteFileAsync(string fileId, CancellationToken cancellationToken = default);

        /// <summary>Deletes a file by id asynchronously</summary>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="maxAttempts">The attempts to delete the file in case, if it is still processing.</param>
        /// <param name="delayBetweenAttemptsInMilliseconds">The delay between attempts in milliseconds.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FileDeleteResponse
        /// </returns>
        Task<HttpOperationResult<FileDeleteResponse>> DeleteFileAsync(string fileId, int maxAttempts, int delayBetweenAttemptsInMilliseconds, CancellationToken cancellationToken = default);

    }

}
