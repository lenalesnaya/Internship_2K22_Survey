using System;

namespace ItechArt.Common.Logging.Abstractions;

public interface ILogger
{
    void Log(LogLevel logLevel, string message, Exception exception = null);
}