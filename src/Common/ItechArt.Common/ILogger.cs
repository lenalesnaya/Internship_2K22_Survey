using System;

namespace ItechArt.Common
{
    public interface ILogger
    {
        public void Log(LogLevel logLevel, string message, Exception exception = null);
    }
}