using System;
using Microsoft.Extensions.Logging;

namespace ItechArt.Common
{
    public class Logger<T> : ILogger<T>
    {
        private readonly Microsoft.Extensions.Logging.ILogger<T> _logger;


        public Logger()
        {
            _logger = new Microsoft.Extensions.Logging.Logger<T>(new LoggerFactory());
        }


        public void Log(LogLevel logLevel, string message, Exception exception = null)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    _logger.LogTrace(message);
                    break;
                case LogLevel.Information:
                    _logger.LogInformation(message);
                    break;
                case LogLevel.Warning:
                    _logger.LogWarning(message);
                    break;
                case LogLevel.Error:
                    _logger.LogError(message, exception);
                    break;
                case LogLevel.Critical:
                    _logger.LogCritical(message, exception);
                    break;
            }
        }
    }
}