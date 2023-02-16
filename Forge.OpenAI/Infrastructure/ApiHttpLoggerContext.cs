using Forge.OpenAI.Interfaces.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forge.OpenAI.Infrastructure
{

    /// <summary>Represents the Http context logger implementation</summary>
    public class ApiHttpLoggerContext : IApiHttpLoggerContext
    {

        private static long CONTEXT_COUNTER = 0;

        private readonly ILogger<ApiHttpLoggerContext> _logger;
        private readonly string _logDirectory;
        private long _logId = 0;

        /// <summary>Initializes a new instance of the <see cref="ApiHttpLoggerContext" /> class.</summary>
        /// <param name="logDirectory">The log directory.</param>
        /// <param name="logger">The logger (optional).</param>
        /// <exception cref="System.ArgumentNullException">logDirectory</exception>
        public ApiHttpLoggerContext(string logDirectory, ILogger<ApiHttpLoggerContext> logger = null)
        {
            if (string.IsNullOrWhiteSpace(logDirectory)) throw new ArgumentNullException(nameof(logDirectory));
            _logDirectory = logDirectory;
            _logger = logger;
            ContextId = Interlocked.Increment(ref CONTEXT_COUNTER);
        }

        /// <summary>Gets the context identifier.</summary>
        /// <value>The context identifier.</value>
        public long ContextId { get; private set; }

        /// <summary>Logs the specified data.</summary>
        /// <param name="data">The data.</param>
        public async Task Log(object data)
        {
            long logId = Interlocked.Increment(ref _logId);
            Type dataType = data?.GetType();
            string typeName = data == null ? "null" : dataType.Name;
            if (data != null && dataType.IsGenericType)
            {
                Func<Type, string> readGenericType = null;
                readGenericType = (Type genericType) =>
                {
                    StringBuilder sb = new StringBuilder(genericType.Name);
                    foreach(Type genType in genericType.GenericTypeArguments)
                    {
                        sb.Append("_");
                        sb.Append(genType.Name);
                        if (genType.IsGenericType) sb.Append(readGenericType(genType));
                    }
                    return sb.ToString();
                };
                typeName = readGenericType(dataType);
            }

            string fileName = Path.Combine(_logDirectory, $"{ContextId}_{logId}_{typeName}.json");
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    if (data != null)
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            await sw.WriteLineAsync(data.ToString());
                            sw.Flush();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
            }
        }

    }

}
