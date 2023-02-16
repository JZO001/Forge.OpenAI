using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.Common
{

    /// <summary>Represents a HTTP operation outcome. It can be a success or a failure.</summary>
    public class HttpOperationResult
    {

        /// <summary>Initializes a new instance of the <see cref="HttpOperationResult" /> class.</summary>
        public HttpOperationResult()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="HttpOperationResult" /> class.</summary>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        public HttpOperationResult(HttpStatusCode httpStatusCode)
        {
            StatusCode = httpStatusCode;
        }

        /// <summary>Initializes a new instance of the <see cref="HttpOperationResult" /> class.</summary>
        /// <param name="exception">The exception.</param>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        /// <param name="errorMessage">The error message.</param>
        public HttpOperationResult(Exception exception, HttpStatusCode httpStatusCode, string
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            errorMessage = null)
        {
            Exception = exception;
            StatusCode = httpStatusCode;
            ErrorMessage = errorMessage ?? exception?.Message;

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                ErrorResponse = JsonSerializer.Deserialize<ErrorResponse>(errorMessage, serializeOptions);
            }
        }

        /// <summary>Gets the error message.</summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; internal set; }

        /// <summary>Gets the exception.</summary>
        /// <value>The exception.</value>
        [JsonIgnore]
        public Exception Exception { get; internal set; }

        /// <summary>Gets the status code.</summary>
        /// <value>The status code.</value>
        public HttpStatusCode StatusCode { get; internal set; } = HttpStatusCode.OK;

        /// <summary>Gets a value indicating whether the operation was a success or not</summary>
        /// <value>
        ///   <c>true</c> if this instance is success; otherwise, <c>false</c>.</value>
        public bool IsSuccess => Exception == null;

        /// <summary>Gets the error response.</summary>
        /// <value>The error response.</value>
        public ErrorResponse
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            ErrorResponse { get; internal set; }

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => JsonSerializer.Serialize(this, GetType());

    }

    /// <summary>Represents a HTTP operation outcome. It can be a success or a failure.</summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public class HttpOperationResult<TResult> : HttpOperationResult where TResult : class
    {

        /// <summary>Initializes a new instance of the <see cref="HttpOperationResult{TResult}" /> class.</summary>
        /// <param name="result">The result.</param>
        public HttpOperationResult(TResult
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            result)
        {
            Result = result;
        }

        /// <summary>Initializes a new instance of the <see cref="HttpOperationResult{TResult}" /> class.</summary>
        /// <param name="result">The result.</param>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        public HttpOperationResult(TResult
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            result, HttpStatusCode httpStatusCode) : this(result)
        {
            StatusCode = httpStatusCode;
        }

        /// <summary>Initializes a new instance of the <see cref="HttpOperationResult{TResult}" /> class.</summary>
        /// <param name="exception">The exception.</param>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        /// <param name="errorMessage">The error message.</param>
        public HttpOperationResult(Exception exception, HttpStatusCode httpStatusCode, string
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            errorMessage = null) : base(exception, httpStatusCode, errorMessage)
        {
        }

        /// <summary>Gets the result.</summary>
        /// <value>The result.</value>
        public TResult
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            Result { get; internal set; }

        /// <summary>Performs an implicit conversion from TResult to <see cref="HttpOperationResult{TResult}" />.</summary>
        /// <param name="result">The result.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator HttpOperationResult<TResult>(TResult
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            result) => new HttpOperationResult<TResult>(result, HttpStatusCode.OK);

        /// <summary>Performs an implicit conversion from <see cref="HttpOperationResult{TResult}" /> to TResult />.</summary>
        /// <param name="result">The result.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator TResult(HttpOperationResult<TResult> result) => result?.Result;

    }

}
