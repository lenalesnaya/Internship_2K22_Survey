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


    public void Trace(string message)
    {
        _logger.LogTrace(message);
    }

    public void Information(string message)
    {
        _logger.LogInformation(message);
    }

    public void Warning(string message, Exception exception)
    {
        _logger.LogWarning(exception, message);
    }

    public void Error(string message, Exception exception)
    {
        _logger.LogError(exception, message);
    }

    public void Critical(string message, Exception exception)
    {
        _logger.LogCritical(exception, message);
    }
}