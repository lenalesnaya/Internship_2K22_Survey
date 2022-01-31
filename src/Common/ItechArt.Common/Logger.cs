using System;
using Serilog;

namespace ItechArt.Common
{
    public class Logger : ILogger
    {
        public void Trace(string logMessage)
        {
            Log.Verbose(logMessage + Environment.NewLine);
        }

        public void Information(string logMessage)
        {
            Log.Information(logMessage + Environment.NewLine);
        }

        public void Warning(string logMessage)
        {
            Log.Warning(logMessage + Environment.NewLine);
        }

        public void Error(string logMessage, Exception exception)
        {
            Log.Error(exception, logMessage + Environment.NewLine);
        }

        public void Critical(string logMessage, Exception exception)
        {
            Log.Fatal(exception, logMessage + Environment.NewLine);
        }
    }
}