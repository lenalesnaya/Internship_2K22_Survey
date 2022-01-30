using System;
using Microsoft.Extensions.Logging;
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
            Log.Information(exception, logMessage + Environment.NewLine);
        }

        public void Warning(string logMessage, Exception exception = null)
        {
            Log.Warning(exception, logMessage + Environment.NewLine);
        }

        public void Critical(string logMessage, Exception exception = null)
        {
            Log.Fatal(exception, logMessage + Environment.NewLine);
        }

        public void Debug(string logMessage, Exception exception = null)
        {
            Log.Debug(exception, logMessage + Environment.NewLine);
        }

        public void Error(string logMessage, Exception exception = null)
        {
            Log.Error(exception, logMessage + Environment.NewLine);
        }
    }
}