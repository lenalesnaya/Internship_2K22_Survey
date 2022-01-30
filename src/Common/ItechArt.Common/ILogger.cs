using Microsoft.Extensions.Logging;
using System;

namespace ItechArt.Common
{
    public interface ILogger
    {
        void Write(LogLevel logLevel, string logMessage, Exception exception = null);

        void Information(string logMessage, Exception exception = null);

        void Warning(string logMessage, Exception exception = null);

        void Critical(string logMessage, Exception exception = null);

        void Debug(string logMessage, Exception exception = null);

        void Error(string logMessage, Exception exception = null);
    }
}