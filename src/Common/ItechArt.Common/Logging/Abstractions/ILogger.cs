using System;

namespace ItechArt.Common;

public interface ILogger
{
    void Log(LogLevel logLevel, string message, Exception exception = null);
}