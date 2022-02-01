using System;

namespace ItechArt.Common;

public static class LoggerExtension
{
    public static void Log(this ILogger logger, LogLevel logLevel, string message, Exception exception = null)
    {
        switch (logLevel)
        {
            case LogLevel.Trace:
                logger.Trace(message);
                break;
            case LogLevel.Information:
                logger.Information(message);
                break;
            case LogLevel.Warning:
                logger.Warning(message, exception);
                break;
            case LogLevel.Error:
                logger.Error(message, exception);
                break;
            case LogLevel.Critical:
                logger.Critical(message, exception);
                break;
        }
    }
}