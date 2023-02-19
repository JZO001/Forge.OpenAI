using Forge.OpenAI.Infrastructure;
using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Files;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Services
{

    /// <summary>Represents the file service</summary>
    public class FileService : ServiceBase, IFileService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;

        /// <summary>Initializes a new instance of the <see cref="FileService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API communication service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiCommunicationService</exception>
        public FileService(OpenAIOptions options, IApiHttpService apiHttpService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (apiHttpService == null) throw new ArgumentNullException(nameof(apiHttpService));
            _options = options;
            _apiHttpService = apiHttpService;
        }

        /// <summary>Initializes a new instance of the <see cref="FileService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API communication service.</param>
        public FileService(IOptions<OpenAIOptions> options, IApiHttpService apiHttpService)
            : this(options?.Value, apiHttpService)
        {
        }

        /// <summary>Gets the file list asynchronously</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FileListResponse
        /// </returns>
        public async Task<HttpOperationResult<FileListResponse>> GetFileListAsync(CancellationToken cancellationToken = default)
        {
            return await _apiHttpService.GetAsync<FileListResponse>(GetFileListUri(), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Uploads a file asynchronously</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FileUploadResponse
        /// </returns>
        public async Task<HttpOperationResult<FileUploadResponse>> UploadFileAsync(FileUploadRequest request, CancellationToken cancellationToken = default)
        {
            var validationResult = request.Validate<FileUploadResponse>();
            if (validationResult != null) return validationResult;
            return await _apiHttpService.PostAsync<FileUploadRequest, FileUploadResponse>(GetFileUploadUri(), request, FileUploadHttpContentFactoryAsync, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Deletes a file by id asynchronously</summary>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FileDeleteResponse
        /// </returns>
        public async Task<HttpOperationResult<FileDeleteResponse>> DeleteFileAsync(string fileId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fileId)) return new HttpOperationResult<FileDeleteResponse>(new ArgumentNullException(nameof(fileId)), System.Net.HttpStatusCode.BadRequest);
            return await DeleteFileAsync(fileId, 1, 1000, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Deletes a file by id asynchronously</summary>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="maxAttempts">The attempts to delete the file in case, if it is still processing.</param>
        /// <param name="delayBetweenAttemptsInMilliseconds">The delay between attempts in milliseconds.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FileDeleteResponse
        /// </returns>
        public async Task<HttpOperationResult<FileDeleteResponse>> DeleteFileAsync(string fileId, int maxAttempts, int delayBetweenAttemptsInMilliseconds, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fileId)) return new HttpOperationResult<FileDeleteResponse>(new ArgumentNullException(nameof(fileId)), System.Net.HttpStatusCode.BadRequest);
            if (delayBetweenAttemptsInMilliseconds < 0) return new HttpOperationResult<FileDeleteResponse>(new ArgumentOutOfRangeException(nameof(delayBetweenAttemptsInMilliseconds)), System.Net.HttpStatusCode.BadRequest);

            async Task<HttpOperationResult<FileDeleteResponse>> InternalDeleteFileAsync(int attempt)
            {
                HttpOperationResult<FileDeleteResponse> attemptResponse = await _apiHttpService.DeleteAsync<FileDeleteResponse>($"{GetFileDeleteUri()}/{fileId}", cancellationToken).ConfigureAwait(false);

                if (!attemptResponse.IsSuccess && attempt < maxAttempts)
                {
                    if (!string.IsNullOrWhiteSpace(attemptResponse.ErrorMessage) &&
                        attemptResponse.ErrorMessage.Contains("File is still processing. Check back later."))
                    {
                        await Task.Delay(delayBetweenAttemptsInMilliseconds, cancellationToken).ConfigureAwait(false);
                        return await InternalDeleteFileAsync(attempt + 1).ConfigureAwait(false);
                    }
                }

                return attemptResponse;
            }

            return await InternalDeleteFileAsync(0).ConfigureAwait(false);
        }

        /// <summary>Gets the file data asynchronous.</summary>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FileDataResponse
        /// </returns>
        public async Task<HttpOperationResult<FileDataResponse>> GetFileDataAsync(string fileId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fileId)) return new HttpOperationResult<FileDataResponse>(new ArgumentNullException(nameof(fileId)), System.Net.HttpStatusCode.BadRequest);
            return await _apiHttpService.GetAsync<FileDataResponse>(string.Format(GetFileDataUri(), fileId), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Downloads the file asynchronous.</summary>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="resultStream">The result stream.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   Output Stream, which can receive the data from the underlying network stream.
        /// </returns>
        public async Task<HttpOperationResult<Stream>> DownloadFileAsync(string fileId, Stream resultStream, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fileId)) return new HttpOperationResult<Stream>(new ArgumentNullException(nameof(fileId)), System.Net.HttpStatusCode.BadRequest);
            if (resultStream == null) return new HttpOperationResult<Stream>(new ArgumentNullException(nameof(resultStream)), System.Net.HttpStatusCode.BadRequest);
            return await _apiHttpService.GetContentAsStream(string.Format(GetDownloadFileUri(), fileId), resultStream, cancellationToken).ConfigureAwait(false);
        }

        private string GetFileListUri()
        {
            return $"{GetBaseUri(_options)}{_options.FileListUri}";
        }

        private string GetFileUploadUri()
        {
            return $"{GetBaseUri(_options)}{_options.FileUploadUri}";
        }

        private string GetFileDataUri()
        {
            return $"{GetBaseUri(_options)}{_options.FileDataUri}";
        }

        private string GetDownloadFileUri()
        {
            return $"{GetBaseUri(_options)}{_options.FileDownloadUri}";
        }

        private string GetFileDeleteUri()
        {
            return $"{GetBaseUri(_options)}{_options.FileDeleteUri}";
        }

        private async Task<HttpContent> FileUploadHttpContentFactoryAsync(FileUploadRequest request, CancellationToken cancellationToken)
        {
            if (request.File == null) throw new InvalidOperationException("Missing file content data");
            if (request.File.SourceContent == null && request.File.SourceStream == null) throw new InvalidOperationException("No file content nor file stream defined in file content data.");
            if (string.IsNullOrWhiteSpace(request.File.ContentName)) throw new InvalidOperationException("Missing file name in file content data");

            MultipartFormDataContent content = new MultipartFormDataContent();

            // add file content
            if (request.File.SourceContent != null)
            {
                content.Add(new ByteArrayContent(request.File.SourceContent), "file", request.File.ContentName);
            }
            else
            {
                using (MemoryStream fileData = new MemoryStream())
                {
                    await request.File.SourceStream.CopyToAsync(fileData).ConfigureAwait(false);
                    content.Add(new ByteArrayContent(fileData.ToArray()), "file", request.File.ContentName);
                    fileData.SetLength(0);
                }
            }

            content.Add(new StringContent(request.Purpose), "purpose");

            return content;
        }

    }

}
