using Microsoft.Extensions.Logging;
using System;
using Serilog;

namespace ItechArt.Common
{
    public class Logger : ILogger
    {
        public void Write(LogLevel logLevel, string logMessage, Exception exception = null)
        {
            switch (logLevel)
            {
                case LogLevel.Information:
                    Log.Information(exception, logMessage + Environment.NewLine);
                    break;
                case LogLevel.Warning:
                    Log.Warning(exception, logMessage + Environment.NewLine);
                    break;
                case LogLevel.Critical:
                    Log.Fatal(exception, logMessage + Environment.NewLine);
                    break;
                case LogLevel.Debug:
                    Log.Debug(exception, logMessage + Environment.NewLine);
                    break;
                case LogLevel.Error:
                    Log.Error(exception, logMessage + Environment.NewLine);
                    break;
            }
        }

        public void Information(string logMessage, Exception exception = null)
        {
            Write(LogLevel.Information, logMessage, exception);
        }

        public void Warning(string logMessage, Exception exception = null)
        {
            Write(LogLevel.Warning, logMessage, exception);
        }

        public void Critical(string logMessage, Exception exception = null)
        {
            Write(LogLevel.Critical, logMessage, exception);
        }

        public void Debug(string logMessage, Exception exception = null)
        {
            Write(LogLevel.Debug, logMessage, exception);
        }

        public void Error(string logMessage, Exception exception = null)
        {
            Write(LogLevel.Error, logMessage, exception);
        }
    }
}