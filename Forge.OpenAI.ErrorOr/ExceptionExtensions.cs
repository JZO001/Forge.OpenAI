using System;
using System.Collections.Generic;

namespace Forge.OpenAI.ErrorOr
{

    public static class ExceptionExtensions
    {
        public const string EXCEPTION_MESSAGE_KEY = "ExceptionMessage";
        public const string EXCEPTION_DETAILS_KEY = "ExceptionDetails";
        public const string INNER_EXCEPTION_MESSAGE_KEY = "InnerExceptionMessage";
        public const string INNER_EXCEPTION_DETAILS_KEY = "InnerExceptionDetails";

        public static Dictionary<string, object> ToErrorMetadata(this Exception exception, params (string key, string value)[] context)
        {
            var metadata = new Dictionary<string, object>
            {
                {
                    EXCEPTION_MESSAGE_KEY, exception.Message
                },
                {
                    EXCEPTION_DETAILS_KEY, exception.ToString()
                }
            };

            if (exception.InnerException != null)
            {
                metadata[INNER_EXCEPTION_MESSAGE_KEY] = exception.InnerException.Message;
                metadata[INNER_EXCEPTION_DETAILS_KEY] = exception.InnerException.ToString();
            }

            foreach (var (key, value) in context)
            {
                if (key != EXCEPTION_DETAILS_KEY && key != EXCEPTION_MESSAGE_KEY)
                {
                    metadata[key] = value;
                }
            }

            return metadata;
        }
    }

}
