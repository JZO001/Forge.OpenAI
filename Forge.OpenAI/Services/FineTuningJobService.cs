using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.FineTuningJob;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Services
{

    public class FineTuningJobService : IFineTuningJobService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;
        private readonly IProviderEndpointService _providerEndpointService;

        /// <summary>Initializes a new instance of the <see cref="FineTuningJobService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiCommunicationService</exception>
        public FineTuningJobService(OpenAIOptions options, IServiceProvider serviceProvider, IProviderEndpointService providerEndpointService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));
            if (providerEndpointService == null) throw new ArgumentNullException(nameof(providerEndpointService));

            _options = options;
            _apiHttpService = serviceProvider.GetRequiredService<IApiHttpService>();
            _providerEndpointService = providerEndpointService;
        }

        /// <summary>Initializes a new instance of the <see cref="FineTuningJobService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        public FineTuningJobService(IOptions<OpenAIOptions> options, IServiceProvider serviceProvider, IProviderEndpointService providerEndpointService)
            : this(options?.Value, serviceProvider, providerEndpointService)
        {
        }

        /// <summary>Creates a fine tune job asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuningJobResponse
        /// </returns>
        public async Task<HttpOperationResult<FineTuningJobResponse>> CreateAsync(FineTuningJobCreateRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) return new HttpOperationResult<FineTuningJobResponse>(new ArgumentNullException(nameof(request)), System.Net.HttpStatusCode.BadRequest);

            var validationResult = request.Validate<FineTuningJobResponse>();
            if (validationResult != null) return validationResult;

            return await _apiHttpService.PostAsync<FineTuningJobCreateRequest, FineTuningJobResponse>(GetCreateUri(), request, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Gets the list of fine tune jobs asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuningJobListResponse
        /// </returns>
        public async Task<HttpOperationResult<FineTuningJobListResponse>> GetAsync(FineTuningJobListRequest request = null, CancellationToken cancellationToken = default)
        {
            return await _apiHttpService.GetAsync<FineTuningJobListResponse>(GetListUri(request), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Gets a fine tune job data asynchronously.</summary>
        /// <param name="fineTuneJobId">The fine tune identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuningJobResponse
        /// </returns>
        public async Task<HttpOperationResult<FineTuningJobResponse>> GetAsync(string fineTuneJobId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fineTuneJobId)) return new HttpOperationResult<FineTuningJobResponse>(new ArgumentNullException(nameof(fineTuneJobId)), System.Net.HttpStatusCode.BadRequest);
            
            return await _apiHttpService.GetAsync<FineTuningJobResponse>(string.Format(GetUri(), fineTuneJobId), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Gets the events of a fine tune job asynchronously.</summary>
        /// <param name="fineTuneJobId">The fine tune identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuningJobEvent
        /// </returns>
        public async Task<HttpOperationResult<FineTuningJobEvent>> GetEventsAsync(string fineTuneJobId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fineTuneJobId)) return new HttpOperationResult<FineTuningJobEvent>(new ArgumentNullException(nameof(fineTuneJobId)), System.Net.HttpStatusCode.BadRequest);
            
            return await _apiHttpService.GetAsync<FineTuningJobEvent>(GetEventsUri(fineTuneJobId), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the events as stream asynchronous.
        /// WARNING: method will block until cancelling with CancellationToken,
        /// or the Fine Tune Job cancel called on the API.
        /// </summary>
        /// <param name="fineTuningJobId">The fine tune identifier.</param>
        /// <param name="resultCallback">The result callback.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   HttpOperationResult
        /// </returns>
        public async Task<HttpOperationResult> GetEventsAsStreamAsync(string fineTuningJobId, Action<HttpOperationResult<FineTuningJobEvent>> resultCallback, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fineTuningJobId)) return new HttpOperationResult(new ArgumentNullException(nameof(fineTuningJobId)), System.Net.HttpStatusCode.BadRequest);
            
            return await _apiHttpService.StreamedGetAsync(GetStreamedEventsUri(fineTuningJobId), resultCallback, cancellationToken).ConfigureAwait(false);
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <summary>
        /// Gets the events as stream asynchronous.
        /// WARNING: method will block until cancelling with CancellationToken,
        /// or the Fine Tuning Job cancel called on the API.
        /// </summary>
        /// <param name="fineTuningJobId">The fine tuning job identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuningJobEvent
        /// </returns>
        public IAsyncEnumerable<HttpOperationResult<FineTuningJobEvent>> GetEventsAsStreamAsync(string fineTuningJobId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fineTuningJobId)) return RequestBase.GetValidationResultAsAsyncEnumerable<FineTuningJobEvent>(new HttpOperationResult<FineTuningJobEvent>(new ArgumentNullException(nameof(fineTuningJobId)), System.Net.HttpStatusCode.BadRequest));
            
            return _apiHttpService.StreamedGetAsync<FineTuningJobEvent>(GetStreamedEventsUri(fineTuningJobId), cancellationToken);
        }
#endif

        /// <summary>Cancels a fine tune job asynchronously.</summary>
        /// <param name="fineTuneJobId">The fine tune identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   FineTuningJobResponse
        /// </returns>
        public async Task<HttpOperationResult<FineTuningJobResponse>> CancelAsync(string fineTuneJobId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(fineTuneJobId)) return new HttpOperationResult<FineTuningJobResponse>(new ArgumentNullException(nameof(fineTuneJobId)), System.Net.HttpStatusCode.BadRequest);
            
            return await _apiHttpService.PostAsync<object, FineTuningJobResponse>(GetCancelUri(fineTuneJobId), null, null, cancellationToken).ConfigureAwait(false);
        }

        private string GetCreateUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.FineTuningJobCreateUri);
        }

        private string GetListUri(FineTuningJobListRequest request)
        {
            StringBuilder sb = new StringBuilder(string.Format(_providerEndpointService.BuildBaseUri(), _options.FineTuningJobListUri));
            if (request != null)
            {
                List<string> queryParams = new List<string>();
                
                if (!string.IsNullOrEmpty(request.After)) queryParams.Add($"after={WebUtility.UrlEncode(request.After)}");
                
                if (request.Limit.HasValue) queryParams.Add($"limit={request.Limit.Value}");

                if (queryParams.Count > 0) sb.Append($"?{string.Join("&", queryParams)}");
            }
            return sb.ToString();
        }

        private string GetUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.FineTuningJobGetUri);
        }

        private string GetEventsUri(string fineTuneJobId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.FineTuningJobEventsUri, fineTuneJobId));
        }

        private string GetStreamedEventsUri(string fineTuningJobId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.FineTuningJobStreamedEventsUri, fineTuningJobId));
        }

        private string GetCancelUri(string fineTuneJobId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.FineTuningJobCancelUri, fineTuneJobId));
        }

    }

}
