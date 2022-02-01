using System;

namespace ItechArt.Common;

public interface ILogger
{
    public void Trace(string message);

    public void Information(string message);

    public void Warning(string message, Exception exception);

    public void Error(string message, Exception exception);

    public void Critical(string message, Exception exception);
}