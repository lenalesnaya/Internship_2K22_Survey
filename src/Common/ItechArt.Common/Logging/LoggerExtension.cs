using System;

namespace ItechArt.Common;

public static class LoggerExtension
{
    public static void Log(this ILogger logger, LogLevel logLevel, string message, Exception exception = null)
    {
        switch (logLevel)
        {
            case LogLevel.Trace:
                logger.LogTrace(message);
                break;
            case LogLevel.Information:
                logger.LogInformation(message);
                break;
            case LogLevel.Warning:
                logger.LogWarning(message, exception);
                break;
            case LogLevel.Error:
                logger.LogError(message, exception);
                break;
            case LogLevel.Critical:
                logger.LogCritical(message, exception);
                break;
        }
    }
}