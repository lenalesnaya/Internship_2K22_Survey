using System;

namespace ItechArt.Common
{
    public interface ILogger
    {
        void Trace(string logMessage);

        void Information(string logMessage);

        void Warning(string logMessage);

        void Error(string logMessage, Exception exception);

        void Critical(string logMessage, Exception exception);
    }
}