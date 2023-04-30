using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.FineTunes;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Services
{

    /// <summary>Represents the fine tune service</summary>
    public class FineTuneService : IFineTuneService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;
        private readonly IProviderEndpointService _providerEndpointService;

        /// <summary>Initializes a new instance of the <see cref="FineTuneService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API communication service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiCommunicationService</exception>
        public FineTuneService(OpenAIOptions options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (apiHttpService == null) throw new ArgumentNullException(nameof(apiHttpService));
            if (providerEndpointService == null) throw new ArgumentNullException(nameof(providerEndpointService));

            _options = options;
            _apiHttpService = apiHttpService;
            _providerEndpointService = providerEndpointService;
        }

        /// <summary>Initializes a new instance of the <see cref="FineTuneService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="apiHttpService">The API communication service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        public FineTuneService(IOptions<OpenAIOptions> options, IApiHttpService apiHttpService, IProviderEndpointService providerEndpointService)
            : this(options?.Value, apiHttpService, providerEndpointService)
        {
        }

        /// <summary>Creates a fine tune job asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuneCreateResponse
        /// </returns>
        public async Task<HttpOperationResult<FineTuneCreateResponse>> CreateAsync(FineTuneCreateRequest request, CancellationToken cancellationToken = default)
        {
            var validationResult = request.Validate<FineTuneCreateResponse>();
            if (validationResult != null) return validationResult;
            return await _apiHttpService.PostAsync<FineTuneCreateRequest, FineTuneCreateResponse>(GetCreateUri(), request, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Gets the list of fine tune jobs asynchronously.</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuneListResponse
        /// </returns>
        public async Task<HttpOperationResult<FineTuneListResponse>> GetAsync(CancellationToken cancellationToken = default)
        {
            return await _apiHttpService.GetAsync<FineTuneListResponse>(GetListUri(), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Gets a fine tune job data asynchronously.</summary>
        /// <param name="fineTuneId">The fine tune identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuneJobDataResponse
        /// </returns>
        public async Task<HttpOperationResult<FineTuneJobDataResponse>> GetAsync(string fineTuneId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fineTuneId)) return new HttpOperationResult<FineTuneJobDataResponse>(new ArgumentNullException(nameof(fineTuneId)), System.Net.HttpStatusCode.BadRequest);
            return await _apiHttpService.GetAsync<FineTuneJobDataResponse>(string.Format(GetUri(), fineTuneId), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Gets the events of a fine tune job asynchronously.</summary>
        /// <param name="fineTuneId">The fine tune identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuneEventsResponse
        /// </returns>
        public async Task<HttpOperationResult<FineTuneJobEventsResponse>> GetEventsAsync(string fineTuneId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fineTuneId)) return new HttpOperationResult<FineTuneJobEventsResponse>(new ArgumentNullException(nameof(fineTuneId)), System.Net.HttpStatusCode.BadRequest);
            return await _apiHttpService.GetAsync<FineTuneJobEventsResponse>(string.Format(GetEventsUri(), fineTuneId), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the events as stream asynchronous.
        /// WARNING: method will block until cancelling with CancellationToken,
        /// or the Fine Tune Job cancel called on the API.
        /// </summary>
        /// <param name="fineTuneId">The fine tune identifier.</param>
        /// <param name="resultCallback">The result callback.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   HttpOperationResult
        /// </returns>
        public async Task<HttpOperationResult> GetEventsAsStreamAsync(string fineTuneId, Action<HttpOperationResult<FineTuneJobEvent>> resultCallback, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fineTuneId)) return new HttpOperationResult(new ArgumentNullException(nameof(fineTuneId)), System.Net.HttpStatusCode.BadRequest);
            return await _apiHttpService.StreamedGetAsync(string.Format(GetStreamedEventsUri(), fineTuneId), resultCallback, cancellationToken).ConfigureAwait(false);
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <summary>
        /// Gets the events as stream asynchronous.
        /// WARNING: method will block until cancelling with CancellationToken,
        /// or the Fine Tune Job cancel called on the API.
        /// </summary>
        /// <param name="fineTuneId">The fine tune identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuneJobEvent
        /// </returns>
        public IAsyncEnumerable<HttpOperationResult<FineTuneJobEvent>> GetEventsAsStreamAsync(string fineTuneId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fineTuneId)) return RequestBase.GetValidationResultAsAsyncEnumerable<FineTuneJobEvent>(new HttpOperationResult<FineTuneJobEvent>(new ArgumentNullException(nameof(fineTuneId)), System.Net.HttpStatusCode.BadRequest));
            return _apiHttpService.StreamedGetAsync<FineTuneJobEvent>(string.Format(GetStreamedEventsUri(), fineTuneId), cancellationToken);
        }
#endif

        /// <summary>Cancels a fine tune job asynchronously.</summary>
        /// <param name="fineTuneId">The fine tune identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuneCancelResponse
        /// </returns>
        public async Task<HttpOperationResult<FineTuneCancelResponse>> CancelAsync(string fineTuneId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fineTuneId)) return new HttpOperationResult<FineTuneCancelResponse>(new ArgumentNullException(nameof(fineTuneId)), System.Net.HttpStatusCode.BadRequest);
            return await _apiHttpService.PostAsync<object, FineTuneCancelResponse>(string.Format(GetCancelUri(), fineTuneId), null, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Deletes a fine tune model asynchronously, owned by your organization.</summary>
        /// <param name="model">The model.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuneDeleteModelResponse
        /// </returns>
        public async Task<HttpOperationResult<FineTuneDeleteModelResponse>> DeleteAsync(string model, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(model)) return new HttpOperationResult<FineTuneDeleteModelResponse>(new ArgumentNullException(nameof(model)), System.Net.HttpStatusCode.BadRequest);
            return await _apiHttpService.DeleteAsync<FineTuneDeleteModelResponse>(string.Format(GetModelDeleteUri(), model), cancellationToken).ConfigureAwait(false);
        }

        private string GetCreateUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.FineTuneCreateUri);
        }

        private string GetListUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.FineTuneListUri);
        }

        private string GetUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.FineTuneGetUri);
        }

        private string GetEventsUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.FineTuneEventsUri);
        }

        private string GetStreamedEventsUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.FineTuneStreamedEventsUri);
        }

        private string GetCancelUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.FineTuneCancelUri);
        }

        private string GetModelDeleteUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.FineTuneDeleteModelUri);
        }

    }

}
