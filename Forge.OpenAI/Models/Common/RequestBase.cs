using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Forge.OpenAI.Models.Common
{

    /// <summary>Base class for requests</summary>
    public abstract class RequestBase
    {

        /// <summary>Validates this request instance data</summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <returns>
        ///   HttpOperationResult
        /// </returns>
        public HttpOperationResult<TResult> Validate<TResult>() where TResult : class
        {
            ICollection<ValidationResult> validationErrors = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this), validationErrors);
            
            return validationErrors.Count > 0 ? 
                new HttpOperationResult<TResult>(new ArgumentException(string.Join(Environment.NewLine, validationErrors)), System.Net.HttpStatusCode.BadRequest) 
                : 
                null;
        }

        /// <summary>Validates this request instance data</summary>
        /// <returns>
        ///   HttpOperationResult
        /// </returns>
        public HttpOperationResult Validate()
        {
            ICollection<ValidationResult> validationErrors = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this), validationErrors);

            return validationErrors.Count > 0 ?
                new HttpOperationResult(new ArgumentException(string.Join(Environment.NewLine, validationErrors)), System.Net.HttpStatusCode.BadRequest)
                :
                null;
        }

#if NETCOREAPP3_1_OR_GREATER
        /// <summary>Gets the validation result as asynchronous enumerable.</summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="result">The result.</param>
        /// <returns>
        ///   IAsyncEnumerable
        /// </returns>
        public static async IAsyncEnumerable<HttpOperationResult<TResult>> GetValidationResultAsAsyncEnumerable<TResult>(HttpOperationResult<TResult> result)
            where TResult : class
        {
            List<HttpOperationResult<TResult>> list = new List<HttpOperationResult<TResult>>();
            list.Add(result);
            var listEnum = list.GetEnumerator();
            while (listEnum.MoveNext())
            {
                yield return listEnum.Current;
            }
        }

        /// <summary>Gets the validation result as asynchronous enumerable.</summary>
        /// <param name="result">The result.</param>
        /// <returns>
        ///   IAsyncEnumerable
        /// </returns>
        public static async IAsyncEnumerable<HttpOperationResult> GetValidationResultAsAsyncEnumerable(HttpOperationResult result)
        {
            List<HttpOperationResult> list = new List<HttpOperationResult>();
            list.Add(result);
            var listEnum = list.GetEnumerator();
            while (listEnum.MoveNext())
            {
                yield return listEnum.Current;
            }
        }
#endif

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => JsonSerializer.Serialize(this, GetType());

        /// <summary>Performs an implicit conversion from <see cref="RequestBase" /> to <see cref="System.String" />.</summary>
        /// <param name="data">The data.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(RequestBase data) => data?.ToString();

    }

}
