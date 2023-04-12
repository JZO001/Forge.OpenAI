using Forge.OpenAI.Interfaces.Infrastructure;
using Forge.OpenAI.Interfaces.Providers;
using Forge.OpenAI.Models.Common;
using Forge.OpenAI.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Infrastructure
{

    /// <summary>API Http Service implementation</summary>
    public class ApiHttpService : IApiHttpService
    {

        private const string ORGANIZATION = "Openai-Organization";
        private const string REQUEST_ID = "X-Request-ID";
        private const string PROCESSING_TIME = "Openai-Processing-Ms";
        private const string OPENAI_VERSION = "Openai-Version";
        private const string OPENAI_MODEL = "Openai-Model";

#if NETCOREAPP3_1_OR_GREATER
        private readonly ILogger<ApiHttpService>? _logger;
#else
        private readonly ILogger<ApiHttpService> _logger;
#endif
        private readonly IApiHttpClientFactory _httpClientFactory;
        private readonly IApiHttpLoggerService _apiHttpLoggerService;
        private readonly IProviderEndpointService _providerEndpointService;
        private readonly OpenAIOptions _options;

        /// <summary>Initializes a new instance of the <see cref="ApiHttpService" /> class.</summary>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <param name="apiHttpLoggerService">The API HTTP logger service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        /// <param name="options">The options.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="System.ArgumentNullException">httpClientFactory
        /// or
        /// options</exception>
        public ApiHttpService(
            IApiHttpClientFactory httpClientFactory,
            IApiHttpLoggerService apiHttpLoggerService,
            IProviderEndpointService providerEndpointService,
            OpenAIOptions options,
#if NETCOREAPP3_1_OR_GREATER
            ILogger<ApiHttpService>? logger = null
#else
            ILogger<ApiHttpService> logger = null
#endif
            )
        {
            if (httpClientFactory == null) throw new ArgumentNullException(nameof(httpClientFactory));
            if (providerEndpointService == null) throw new ArgumentNullException(nameof(providerEndpointService));
            if (options == null) throw new ArgumentNullException(nameof(options));
            
            _httpClientFactory = httpClientFactory;
            _apiHttpLoggerService = apiHttpLoggerService;
            _providerEndpointService = providerEndpointService;
            _options = options;
            _logger = logger;
        }

        /// <summary>Initializes a new instance of the <see cref="ApiHttpService" /> class.</summary>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <param name="apiHttpLoggerService">The API HTTP logger service.</param>
        /// <param name="providerEndpointService">The provider endpoint service.</param>
        /// <param name="options">The options.</param>
        /// <param name="logger">The logger.</param>
        public ApiHttpService(
            IApiHttpClientFactory httpClientFactory,
            IApiHttpLoggerService apiHttpLoggerService,
            IProviderEndpointService providerEndpointService,
            IOptions<OpenAIOptions> options,
#if NETCOREAPP3_1_OR_GREATER
            ILogger<ApiHttpService>? logger = null
#else
            ILogger<ApiHttpService> logger = null
#endif
            )
            : this(httpClientFactory, apiHttpLoggerService, providerEndpointService, options?.Value, logger)
        {
        }

        /// <summary>Gets data</summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result object</returns>
        public async Task<HttpOperationResult<TResult>> GetAsync<TResult>(string uri, CancellationToken cancellationToken = default)
            where TResult : class
        {
            return await ApiCall<object, TResult>(HttpMethod.Get, uri, null, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Posts data or creates a resource</summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <typeparam name="TResult">The type of the result data.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="data">The data.</param>
        /// <param name="contentFactory">The content factory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result data</returns>
        public async Task<HttpOperationResult<TResult>> PostAsync<TData, TResult>(string uri,
#if NETCOREAPP3_1_OR_GREATER
            TData? data,
#else
            TData data, 
#endif
#if NETCOREAPP3_1_OR_GREATER
            Func<TData, CancellationToken, Task<HttpContent>>? contentFactory,
#else
            Func<TData, CancellationToken, Task<HttpContent>> contentFactory, 
#endif
            CancellationToken cancellationToken = default)
            where TData : class
            where TResult : class
        {
            return await ApiCall<TData, TResult>(HttpMethod.Post, uri, data, contentFactory, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Deletes a</summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The return data</returns>
        public async Task<HttpOperationResult<TResult>> DeleteAsync<TResult>(string uri, CancellationToken cancellationToken = default)
            where TResult : class
        {
            return await ApiCall<object, TResult>(HttpMethod.Delete, uri, null, null, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Gets the response content as stream and copy data into the result stream</summary>
        /// <param name="uri">The URI.</param>
        /// <param name="resultStream">The result stream.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   Task
        /// </returns>
        public async Task<HttpOperationResult<Stream>> GetContentAsStream(string uri, Stream resultStream, CancellationToken cancellationToken = default)
        {
            return await ErrorHandlerContextAsync(async (IApiHttpLoggerContext logContext) =>
            {
                if (string.IsNullOrWhiteSpace(uri)) throw new ArgumentNullException(nameof(uri));
                if (resultStream == null) throw new ArgumentNullException(nameof(resultStream));

                HttpOperationResult<Stream> result = new HttpOperationResult<Stream>(resultStream);

                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri))
                {
                    _providerEndpointService.ConfigureHttpRequestHeaders(request.Headers);

                    using (HttpClient httpClient = _httpClientFactory.GetHttpClient())
                    {
                        _logger?.LogDebug($"GetContentAsStream, sending {request.Method.Method} to baseAddress: {httpClient.BaseAddress}, uri: {uri}");

                        using (HttpResponseMessage response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false))
                        {
                            _logger?.LogDebug($"GetContentAsStream, response arrived from baseAddress: {httpClient.BaseAddress}, uri: {uri}, method: {request.Method.Method}");

                            if (response.IsSuccessStatusCode)
                            {
                                using (Stream contentStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                                {
                                    // 81920 constant comes from HttpClient source code
                                    await contentStream.CopyToAsync(resultStream, 81920, cancellationToken).ConfigureAwait(false);
                                }
                            }
                            else
                            {
                                _logger?.LogDebug($"GetContentAsStream, response indicates an unsuccessful operation from {httpClient.BaseAddress}{uri}, method: {request.Method.Method}");

                                string errorResponse = await response.Content.ReadAsStringAsync();
                                result = new HttpOperationResult<Stream>(new Exception(response.StatusCode.ToString(), new Exception(errorResponse)), response.StatusCode, errorResponse);
                            }
                        }
                    }
                }

                logContext?.Log(result);

                return result;
            });
        }

        /// <summary>Sends a get asynchronously and handle the response as a stream.</summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="resultCallback">The result callback.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   HttpOperationResult
        /// </returns>
        public async Task<HttpOperationResult> StreamedGetAsync<TResult>(string uri, Action<HttpOperationResult<TResult>> resultCallback, CancellationToken cancellationToken = default)
            where TResult : class
        {
            return await StreamedAsync<object, TResult>(HttpMethod.Get, uri, null, resultCallback, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>Sends a post asynchronously and handle the response as a stream.</summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="data">The data.</param>
        /// <param name="resultCallback">The result callback.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   HttpOperationResult
        /// </returns>
        public async Task<HttpOperationResult> StreamedPostAsync<TData, TResult>(string uri, TData data, Action<HttpOperationResult<TResult>> resultCallback, CancellationToken cancellationToken = default)
            where TData : class
            where TResult : class
        {
            return await StreamedAsync(HttpMethod.Post, uri, data, resultCallback, cancellationToken).ConfigureAwait(false);
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <summary>Sends a get asynchronously and handle the response as a stream.</summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   IAsyncEnumerable
        /// </returns>
        public IAsyncEnumerable<HttpOperationResult<TResult>> StreamedGetAsync<TResult>(string uri, CancellationToken cancellationToken)
            where TResult : class
        {
            return StreamedAsync<object, TResult>(HttpMethod.Get, uri, null, cancellationToken);
        }

        /// <summary>Sends a post asynchronously and handle the response as a stream.</summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="data">The data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   IAsyncEnumerable
        /// </returns>
        public IAsyncEnumerable<HttpOperationResult<TResult>> StreamedPostAsync<TData, TResult>(string uri, TData data, CancellationToken cancellationToken = default)
            where TData : class
            where TResult : class
        {
            return StreamedAsync<TData, TResult>(HttpMethod.Post, uri, data, cancellationToken);
        }

        /// <summary>Perform an API call in streamed mode the asynchronously.</summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="data">The data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        private async IAsyncEnumerable<HttpOperationResult<TResult>> StreamedAsync<TData, TResult>(HttpMethod httpMethod, string uri, TData data, [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
            where TData : class
            where TResult : class
        {
            IApiHttpLoggerContext logContext = _apiHttpLoggerService?.Create();
            logContext?.Log(data);

            using (HttpRequestMessage request = new HttpRequestMessage(httpMethod, uri))
            {
                _providerEndpointService.ConfigureHttpRequestHeaders(request.Headers);

                if (data != null)
                {
                    request.Content = new StringContent(JsonSerializer.Serialize(data, _options.JsonSerializerOptions), Encoding.UTF8, "application/json");
                }

                using (HttpClient httpClient = _httpClientFactory.GetHttpClient())
                {
                    _logger?.LogDebug($"StreamedAsync, sending {request.Method.Method} to baseAddress: {httpClient.BaseAddress}, uri: {uri}");

                    using (HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
                    {
                        _logger?.LogDebug($"StreamedAsync, response arrived from baseAddress: {httpClient.BaseAddress}, uri: {uri}, method: {request.Method.Method}");

                        if (response.IsSuccessStatusCode)
                        {
                            using (Stream contentStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                            {
                                using (StreamReader reader = new StreamReader(contentStream))
                                {
                                    string? line = null;
                                    
                                    while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null &&
                                        !cancellationToken.IsCancellationRequested)
                                    {
                                        _logger?.LogDebug($"StreamedAsync, response string content from uri: {uri}, data: {line}");

                                        logContext?.Log(line);

                                        if (line.StartsWith("data: ")) line = line.Substring("data: ".Length);

                                        if ("[DONE]".Equals(line.Trim())) break;

                                        if (!string.IsNullOrWhiteSpace(line))
                                        {
                                            TResult jsonResult = JsonSerializer.Deserialize<TResult>(line.Trim(), _options.JsonSerializerOptions)
#if NETCOREAPP3_1_OR_GREATER
                                                !
#endif
                                                ;
                                            SetResponseData(response, jsonResult);
                                            
                                            HttpOperationResult<TResult> resData = new HttpOperationResult<TResult>(jsonResult);
                                            logContext?.Log(resData);
                                            
                                            yield return resData;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            _logger?.LogDebug($"StreamedAsync, response indicates an unsuccessful operation from {httpClient.BaseAddress}{uri}, method: {request.Method.Method}");

                            string? jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                            HttpOperationResult<TResult> unsucRes = new HttpOperationResult<TResult>(new Exception(response.StatusCode.ToString(), new Exception(jsonResult)), response.StatusCode, jsonResult);
                            logContext?.Log(unsucRes);

                            yield return unsucRes;
                        }
                    }
                }
            }
        }
#endif

        /// <summary>Perform the API call with the given parameters.</summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="data">The data.</param>
        /// <param name="contentFactory">The content factory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The return value</returns>
        private async Task<HttpOperationResult<TResult>> ApiCall<TData, TResult>(HttpMethod httpMethod, 
            string uri,
#if NETCOREAPP3_1_OR_GREATER
            TData? data,
            Func<TData?, CancellationToken, Task<HttpContent>>? contentFactory,
#else
            TData data, 
            Func<TData, CancellationToken, Task<HttpContent>> contentFactory, 
#endif
            CancellationToken cancellationToken)
            where TData : class
            where TResult : class
        {
            return await ErrorHandlerContextAsync(async (IApiHttpLoggerContext logContext) =>
            {
                logContext?.Log(data);

#if NETCOREAPP3_1_OR_GREATER
                HttpOperationResult<TResult>? result = default;
#else
                HttpOperationResult<TResult> result = default;
#endif

                using (HttpRequestMessage request = new HttpRequestMessage(httpMethod, uri))
                {
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    if (contentFactory != null)
                    {
                        // construct the content with a factory method
                        request.Content = await contentFactory(data, cancellationToken);
                    }
                    else if (data != null)
                    {
                        // default contruction method
                        request.Content = new StringContent(JsonSerializer.Serialize(data, _options.JsonSerializerOptions), Encoding.UTF8, "application/json");
                    }

                    _providerEndpointService.ConfigureHttpRequestHeaders(request.Headers);

                    using (HttpClient httpClient = _httpClientFactory.GetHttpClient())
                    {
                        _logger?.LogDebug($"ApiCall, sending {httpMethod.Method} to baseAddress: {httpClient.BaseAddress}, uri: {uri}");
                        using (HttpResponseMessage response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false))
                        {
                            _logger?.LogDebug($"ApiCall, response arrived from baseAddress: {httpClient.BaseAddress}, uri: {uri}, method: {httpMethod.Method}");

                            string
#if NETCOREAPP3_1_OR_GREATER
                                ?
#endif
                                jsonResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                            _logger?.LogDebug($"ApiCall, response string content from uri: {uri}, data: {jsonResult}");

                            logContext?.Log(jsonResult);

                            if (response.IsSuccessStatusCode)
                            {
                                if (typeof(string).IsAssignableFrom(typeof(TResult)))
                                {
                                    result = new HttpOperationResult<TResult>(jsonResult as TResult);
                                }
                                else
                                {
                                    result = new HttpOperationResult<TResult>(JsonSerializer.Deserialize<TResult>(jsonResult, _options.JsonSerializerOptions));
                                    SetResponseData(response, result.Result);
                                }
                            }
                            else
                            {
                                _logger?.LogDebug($"ApiCall, response indicates an unsuccessful operation from {httpClient.BaseAddress}{uri}, method: {httpMethod.Method}");

                                result = new HttpOperationResult<TResult>(new Exception(response.StatusCode.ToString(), new Exception(jsonResult)), response.StatusCode, jsonResult);
                            }
                        }
                    }
                }

                logContext?.Log(result);

                return result;
            });
        }

        /// <summary>Perform the API call in streaming mode the asynchronously.</summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="data">The data.</param>
        /// <param name="resultCallback">The result callback.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   HttpOperationResult
        /// </returns>
        private async Task<HttpOperationResult> StreamedAsync<TData, TResult>(HttpMethod httpMethod, string uri, TData data, Action<HttpOperationResult<TResult>> resultCallback, CancellationToken cancellationToken)
            where TData : class
            where TResult : class
        {
            return await ErrorHandlerContextAsync(async (IApiHttpLoggerContext logContext) =>
            {
                if (string.IsNullOrWhiteSpace(uri)) throw new ArgumentNullException(nameof(uri));
                if (resultCallback == null) throw new ArgumentNullException(nameof(resultCallback));

                logContext?.Log(data);

                using (HttpRequestMessage request = new HttpRequestMessage(httpMethod, uri))
                {
                    _providerEndpointService.ConfigureHttpRequestHeaders(request.Headers);

                    if (data != null)
                    {
                        request.Content = new StringContent(JsonSerializer.Serialize(data, _options.JsonSerializerOptions), Encoding.UTF8, "application/json");
                    }

                    using (HttpClient httpClient = _httpClientFactory.GetHttpClient())
                    {
                        _logger?.LogDebug($"StreamedAsync, sending {request.Method.Method} to baseAddress: {httpClient.BaseAddress}, uri: {uri}");

                        using (HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
                        {
                            _logger?.LogDebug($"StreamedAsync, response arrived from baseAddress: {httpClient.BaseAddress}, uri: {uri}, method: {request.Method.Method}");

                            if (response.IsSuccessStatusCode)
                            {
                                using (Stream contentStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                                {
                                    using (StreamReader reader = new StreamReader(contentStream))
                                    {
                                        string
#if NETCOREAPP3_1_OR_GREATER
                                            ?
#endif
                                            line = null;

                                        while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null &&
                                            !cancellationToken.IsCancellationRequested)
                                        {
                                            _logger?.LogDebug($"StreamedAsync, response string content from uri: {uri}, data: {line}");

                                            logContext?.Log(line);

                                            if (line.StartsWith("data: ")) line = line.Substring("data: ".Length);

                                            if ("[DONE]".Equals(line.Trim())) break;

                                            if (!string.IsNullOrWhiteSpace(line))
                                            {
                                                TResult jsonResult = JsonSerializer.Deserialize<TResult>(line.Trim(), _options.JsonSerializerOptions)
#if NETCOREAPP3_1_OR_GREATER
                                                !
#endif
                                                ;
                                                SetResponseData(response, jsonResult);
                                                
                                                HttpOperationResult<TResult> callbackRes = new HttpOperationResult<TResult>(jsonResult);
                                                logContext?.Log(callbackRes);
                                                
                                                resultCallback(callbackRes);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                _logger?.LogDebug($"StreamedAsync, response indicates an unsuccessful operation from {httpClient.BaseAddress}{uri}, method: {request.Method.Method}");

                                string errorResponse = await response.Content.ReadAsStringAsync();
                                
                                HttpOperationResult<TResult> unsucRes = new HttpOperationResult<TResult>(new Exception(response.StatusCode.ToString(), new Exception(errorResponse)), response.StatusCode, errorResponse);
                                logContext?.Log(unsucRes);
                                
                                resultCallback(unsucRes);
                            }
                        }
                    }
                }

                HttpOperationResult res = new HttpOperationResult();
                logContext?.Log(res);

                return res;
            });
        }

        private static void SetResponseData<TResult>(HttpResponseMessage response, TResult result)
        {
            if (typeof(ResponseBase).IsAssignableFrom(typeof(TResult)))
            {
                ResponseBase rb = (result as ResponseBase)
#if NETCOREAPP3_1_OR_GREATER
                    !
#endif
                    ;
                if (response.Headers.Contains(ORGANIZATION)) rb.Organization = response.Headers.GetValues(ORGANIZATION).FirstOrDefault();
                if (response.Headers.Contains(REQUEST_ID)) rb.RequestId = response.Headers.GetValues(REQUEST_ID).FirstOrDefault();
                if (response.Headers.Contains(PROCESSING_TIME)) rb.ProcessingTime = TimeSpan.FromMilliseconds(int.Parse(response.Headers.GetValues(PROCESSING_TIME).First()));
                if (response.Headers.Contains(OPENAI_VERSION)) rb.OpenAIVersion = response.Headers.GetValues(OPENAI_VERSION).FirstOrDefault();
                if (string.IsNullOrEmpty(rb.Model) && response.Headers.Contains(OPENAI_MODEL)) rb.Model = response.Headers.GetValues(OPENAI_MODEL).FirstOrDefault();
            }
        }

        private async Task<HttpOperationResult<TResult>> ErrorHandlerContextAsync<TResult>(Func<IApiHttpLoggerContext, Task<HttpOperationResult<TResult>>> func)
            where TResult : class
        {
            IApiHttpLoggerContext logContext = _apiHttpLoggerService?.Create();
            try
            {
                return await func(logContext).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
                HttpOperationResult<TResult> error = new HttpOperationResult<TResult>(ex, System.Net.HttpStatusCode.BadRequest);
                logContext?.Log(error);
                return error;
            }
        }

        private async Task<HttpOperationResult> ErrorHandlerContextAsync(Func<IApiHttpLoggerContext, Task<HttpOperationResult>> func)
        {
            IApiHttpLoggerContext logContext = _apiHttpLoggerService?.Create();
            try
            {
                return await func(logContext).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
                HttpOperationResult error = new HttpOperationResult(ex, System.Net.HttpStatusCode.BadRequest);
                logContext?.Log(error);
                return error;
            }
        }

    }

}
