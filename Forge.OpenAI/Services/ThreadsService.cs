﻿using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Interfaces.Services;
using Forge.OpenAI.Settings;
using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Models.Threads;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace Forge.OpenAI.Services
{

    /// <summary>
    /// Create threads that assistants can interact with.
    /// https://platform.openai.com/docs/api-reference/threads
    /// </summary>
    public class ThreadsService : IThreadsService
    {

        private readonly OpenAIOptions _options;
        private readonly IApiHttpService _apiHttpService;
        private readonly IProviderEndpointService _providerEndpointService;

        /// <summary>Initializes a new instance of the <see cref="ThreadsService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// apiHttpService
        /// or
        /// providerEndpointService</exception>
        public ThreadsService(OpenAIOptions options, IServiceProvider serviceProvider, IProviderEndpointService providerEndpointService)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));
            if (providerEndpointService == null) throw new ArgumentNullException(nameof(providerEndpointService));

            _options = options;
            _apiHttpService = serviceProvider.GetRequiredService<IApiHttpService>();
            _providerEndpointService = providerEndpointService;

            _apiHttpService.OnPrepareRequest += OnPrepareRequestHandler;
        }

        /// <summary>Initializes a new instance of the <see cref="ThreadsService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        public ThreadsService(IOptions<OpenAIOptions> options, IServiceProvider serviceProvider, IProviderEndpointService providerEndpointService)
            : this(options?.Value, serviceProvider, providerEndpointService)
        {
        }

        /// <summary>Creates a thread asynchronously.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   CreateThreadResponse
        /// </returns>
        public async Task<HttpOperationResult<CreateThreadResponse>> CreateAsync(CreateThreadRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) return new HttpOperationResult<CreateThreadResponse>(new ArgumentNullException(nameof(request)), System.Net.HttpStatusCode.BadRequest);
            
            var validationResult = request.Validate<CreateThreadResponse>();
            if (validationResult != null) return validationResult;
            
            return await _apiHttpService.PostAsync<CreateThreadRequest, CreateThreadResponse>(GetCreateUri(), request, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Gets a thread data asynchronously.</summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   RetrieveThreadResponse
        /// </returns>
        public async Task<HttpOperationResult<RetrieveThreadResponse>> GetAsync(string threadId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(threadId)) return new HttpOperationResult<RetrieveThreadResponse>(new ArgumentNullException(nameof(threadId)), System.Net.HttpStatusCode.BadRequest);
            
            return await _apiHttpService.GetAsync<RetrieveThreadResponse>(GetUri(threadId), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Modifies a thread asynchronously</summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   ModifyThreadResponse
        /// </returns>
        public async Task<HttpOperationResult<ModifyThreadResponse>> ModifyAsync(ModifyThreadRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null) return new HttpOperationResult<ModifyThreadResponse>(new ArgumentNullException(nameof(request)), System.Net.HttpStatusCode.BadRequest);
            
            var validationResult = request.Validate<ModifyThreadResponse>();
            if (validationResult != null) return validationResult;
            
            return await _apiHttpService.PostAsync<ModifyThreadRequest, ModifyThreadResponse>(GetModifyUri(request), request, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Deletes a thread asynchronously.</summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   DeleteThreadResponse
        /// </returns>
        public async Task<HttpOperationResult<DeleteThreadResponse>> DeleteAsync(string threadId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(threadId)) return new HttpOperationResult<DeleteThreadResponse>(new ArgumentNullException(nameof(threadId)), System.Net.HttpStatusCode.BadRequest);
            
            return await _apiHttpService.DeleteAsync<DeleteThreadResponse>(GetDeleteUri(threadId), cancellationToken).ConfigureAwait(false);
        }

        private string GetCreateUri()
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), _options.ThreadCreateUri);
        }

        private string GetUri(string threadId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.ThreadGetUri, threadId));
        }

        private string GetModifyUri(ModifyThreadRequest request)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.ThreadModifyUri, request.ThreadId));
        }

        private string GetDeleteUri(string threadId)
        {
            return string.Format(_providerEndpointService.BuildBaseUri(), string.Format(_options.ThreadDeleteUri, threadId));
        }

        /// <summary>Called when api requires request to be prepared before sending</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="HttpRequestMessageEventArgs" /> instance containing the event data.</param>
        protected virtual void OnPrepareRequestHandler(object
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            sender, HttpRequestMessageEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_options.AssistantHeaderName))
            {
                e.RequestMessage.Headers.Add(_options.AssistantHeaderName, _options.AssistantHeaderValue);
            }
        }

    }

}
