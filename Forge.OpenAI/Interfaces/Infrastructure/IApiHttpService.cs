using Forge.OpenAI.Models;
using Forge.OpenAI.Models.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Infrastructure
{

    /// <summary>Represents an API Http service interface</summary>
    public interface IApiHttpService
    {

        /// <summary>Occurs before the request sent out to prepare it manually</summary>
        event EventHandler<HttpRequestMessageEventArgs> OnPrepareRequest;

        /// <summary>Gets the default request headers in scope of the current Api Http Service</summary>
        /// <value>The default request headers.</value>
        IDictionary<string, string> DefaultRequestHeaders { get; }

        /// <summary>Gets data</summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result object</returns>
        Task<HttpOperationResult<TResult>> GetAsync<TResult>(string uri, CancellationToken cancellationToken = default) where TResult : class;

        /// <summary>Posts data or creates a resource</summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <typeparam name="TResult">The type of the result data.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="data">The data.</param>
        /// <param name="contentFactory">The content factory.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result data</returns>
        Task<HttpOperationResult<TResult>> PostAsync<TData, TResult>(string uri, TData data, Func<TData, CancellationToken, Task<HttpContent>>
#if NETCOREAPP3_1_OR_GREATER
            ? 
#endif
            contentFactory, CancellationToken cancellationToken = default)
            where TData : class
            where TResult : class;

        /// <summary>Deletes a</summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The return data</returns>
        Task<HttpOperationResult<TResult>> DeleteAsync<TResult>(string uri, CancellationToken cancellationToken = default) where TResult : class;

        /// <summary>Sends a get asynchronously and handle the response as a stream.</summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="resultCallback">The result callback.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   HttpOperationResult
        /// </returns>
        Task<HttpOperationResult> StreamedGetAsync<TResult>(string uri, Action<HttpOperationResult<IAsyncEventInfo<TResult>>> resultCallback, CancellationToken cancellationToken = default)
            where TResult : class;

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
        Task<HttpOperationResult> StreamedPostAsync<TData, TResult>(string uri, TData data, Action<HttpOperationResult<IAsyncEventInfo<TResult>>> resultCallback, CancellationToken cancellationToken = default)
            where TData : class
            where TResult : class;

#if NETCOREAPP3_1_OR_GREATER
        /// <summary>Sends a get asynchronously and handle the response as a stream.</summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   IAsyncEnumerable
        /// </returns>
        System.Collections.Generic.IAsyncEnumerable<HttpOperationResult<IAsyncEventInfo<TResult>>> StreamedGetAsync<TResult>(string uri, CancellationToken cancellationToken = default)
            where TResult : class;

        /// <summary>Sends a post asynchronously and handle the response as a stream.</summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="data">The data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   IAsyncEnumerable
        /// </returns>
        System.Collections.Generic.IAsyncEnumerable<HttpOperationResult<IAsyncEventInfo<TResult>>> StreamedPostAsync<TData, TResult>(string uri, TData data, CancellationToken cancellationToken = default)
            where TData : class
            where TResult : class;
#endif

        /// <summary>Gets the response content as stream and copy data into the result stream</summary>
        /// <param name="uri">The URI.</param>
        /// <param name="resultStream">The result stream.</param>
        /// <param name="data">The request content.</param>
        /// <param name="contentFactory">The content factory.</param>
        /// <param name="httpMethod">The http method.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        ///   Task
        /// </returns>
        Task<HttpOperationResult<Stream>> GetContentAsStream<TData>(string uri, 
            Stream resultStream,
#if NETCOREAPP3_1_OR_GREATER
            TData? data,
            Func<TData?, CancellationToken, Task<HttpContent>>? contentFactory = null,
#else
            TData data,
            Func<TData, CancellationToken, Task<HttpContent>> contentFactory = null,
#endif
            HttpMethod httpMethod = null,
            CancellationToken cancellationToken = default)
            where TData : class;

    }

}
