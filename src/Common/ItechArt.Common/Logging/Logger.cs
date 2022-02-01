using System;
using Microsoft.Extensions.Logging;

namespace ItechArt.Common;

public class Logger : ILogger
{
    private readonly Microsoft.Extensions.Logging.ILogger _logger;


    public Logger()
    {
        ILoggerFactory loggerFactory = new LoggerFactory();
        _logger = loggerFactory.CreateLogger("MyLogger");
    }


    public void LogTrace(string message)
    {
        _logger.LogTrace(message);
    }

    public void LogInformation(string message)
    {
        _logger.LogInformation(message);
    }

    public void LogWarning(string message, Exception exception)
    {
        _logger.LogWarning(exception, message);
    }

    public void LogError(string message, Exception exception)
    {
        _logger.LogError(exception, message);
    }

    public void LogCritical(string message, Exception exception)
    {
        _logger.LogCritical(exception, message);
    }
}