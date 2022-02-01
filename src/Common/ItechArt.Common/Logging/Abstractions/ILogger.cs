using System;

namespace ItechArt.Common;

public interface ILogger
{
    public void LogTrace(string message);

    public void LogInformation(string message);

    public void LogWarning(string message, Exception exception);

    public void LogError(string message, Exception exception);

    public void LogCritical(string message, Exception exception);
}