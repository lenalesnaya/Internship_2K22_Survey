using System;
using Microsoft.Extensions.Logging;
using ILogger = ItechArt.Common.Logging.Abstractions.ILogger;
using LogLevel = ItechArt.Common.Logging.Abstractions.LogLevel;

namespace ItechArt.Common.Logging;

public class Logger : ILogger
{
    private readonly Microsoft.Extensions.Logging.ILogger _logger;


    public Logger(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger("Logger");
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
                _logger.LogWarning(exception, message);
                break;
            case LogLevel.Error:
                _logger.LogError(exception, message);
                break;
            case LogLevel.Critical:
                _logger.LogCritical(exception, message);
                break;
            default:
                try
                {
                    throw new ArgumentOutOfRangeException();
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    throw new ArgumentOutOfRangeException(
                        "Unregistered LogLevel value",
                        ex,
                        $"The Loglevel value \"{logLevel}\" isn`t registered in the current method");
                }
        }
    }
}