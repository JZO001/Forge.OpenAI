using ErrorOr;
using Forge.OpenAI.Models.Common;
using System;

namespace Forge.OpenAI.ErrorOr
{

    /// <summary>
    /// </summary>
    public static class HttpOperationResultExtension
    {

        /// <summary>Use ErrorOr as return object</summary>
        /// <param name="httpOperationResult">The HTTP operation result.</param>
        /// <returns>
        /// </returns>
        public static ErrorOr<Success> AsErrorOr(this HttpOperationResult httpOperationResult)
        {
            if (httpOperationResult.IsSuccess)
            {
                return Result.Success;
            }

            return Error.Failure(code: httpOperationResult.Exception.GetType().Name, description: httpOperationResult.Exception.Message, metadata: httpOperationResult.Exception.ToErrorMetadata());
        }

        /// <summary>Use ErrorOr as return object</summary>
        /// <typeparam name="THttpOperationResult">The type of the result.</typeparam>
        /// <param name="httpOperationResult">The HTTP operation result.</param>
        /// <returns>
        /// </returns>
        public static ErrorOr<THttpOperationResult> AsErrorOr<THttpOperationResult>(this HttpOperationResult<THttpOperationResult> httpOperationResult) where THttpOperationResult : class
        {
            if (httpOperationResult.IsSuccess)
            {
#if NETCOREAPP3_1_OR_GREATER
                return httpOperationResult.Result!;
#else
                return httpOperationResult.Result;
#endif
            }

            return Error.Failure(code: httpOperationResult.Exception.GetType().Name, description: httpOperationResult.Exception.Message, metadata: httpOperationResult.Exception.ToErrorMetadata());
        }

        /// <summary>Use ErrorOr as return object</summary>
        /// <typeparam name="THttpOperationResult">The type of the HTTP operation result.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="httpOperationResult">The HTTP operation result.</param>
        /// <returns>
        /// </returns>
        public static ErrorOr<TResult> AsErrorOr<THttpOperationResult, TResult>(this HttpOperationResult<THttpOperationResult> httpOperationResult)
            where THttpOperationResult : class
            where TResult : class
        {
            if (httpOperationResult.IsSuccess)
            {
                try
                {
#if NETCOREAPP3_1_OR_GREATER
                    return (TResult)(object)httpOperationResult.Result!;
#else
                    return (TResult)(object)httpOperationResult.Result;
#endif
                }
                catch (Exception ex)
                {
                    return Error.Failure(code: ex.GetType().Name, description: ex.Message, metadata: ex.ToErrorMetadata());
                }
            }

            return Error.Failure(code: httpOperationResult.Exception.GetType().Name, description: httpOperationResult.Exception.Message, metadata: httpOperationResult.Exception.ToErrorMetadata());
        }

    }

}
