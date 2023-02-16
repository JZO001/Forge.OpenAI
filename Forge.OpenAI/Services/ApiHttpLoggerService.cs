using Forge.OpenAI.Infrastructure;
using Forge.OpenAI.Interfaces.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace Forge.OpenAI.Services
{

    /// <summary>Represents the Http logger implementation</summary>
    public class ApiHttpLoggerService : IApiHttpLoggerService
    {

        private readonly ILogger<ApiHttpLoggerContext> _logger;
        private readonly bool _isLogEnabled;
        private readonly string _logDirectory;

        /// <summary>Initializes a new instance of the <see cref="ApiHttpLoggerService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="logger">The logger (optional).</param>
        /// <exception cref="System.ArgumentNullException">options</exception>
        public ApiHttpLoggerService(OpenAIOptions options, ILogger<ApiHttpLoggerContext> logger = null)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            _logger = logger;

            if (_isLogEnabled = options.LogRequestsAndResponses)
            {
                string logDir = string.IsNullOrWhiteSpace(options.LogRequestsAndResponsesFolder) ? Environment.CurrentDirectory : options.LogRequestsAndResponsesFolder;
                if (!Path.IsPathRooted(logDir)) logDir = Path.Combine(Environment.CurrentDirectory, logDir);
                DirectoryInfo di = new DirectoryInfo(logDir);
                if (!di.Exists)
                {
                    try
                    {
                        di.Create();
                        _logDirectory = di.FullName;
                    }
                    catch (Exception ex)
                    {
                        logger?.LogError(ex, ex.Message);
                        _isLogEnabled = false;
                    }
                }
                else
                {
                    _logDirectory = di.FullName;
                }
            }
        }

        /// <summary>Initializes a new instance of the <see cref="ApiHttpLoggerService" /> class.</summary>
        /// <param name="options">The options.</param>
        /// <param name="logger">The logger (optional).</param>
        public ApiHttpLoggerService(IOptions<OpenAIOptions> options, ILogger<ApiHttpLoggerContext> logger = null)
            : this(options?.Value, logger)
        {
        }

        /// <summary>Creates a new logger context instance.</summary>
        /// <returns>IApiHttpLoggerContext</returns>
        public IApiHttpLoggerContext
#if NETCOREAPP3_1_OR_GREATER
            ?
#endif
            Create()
        {
            return _isLogEnabled ? new ApiHttpLoggerContext(_logDirectory, _logger) : null;
        }

    }

}
