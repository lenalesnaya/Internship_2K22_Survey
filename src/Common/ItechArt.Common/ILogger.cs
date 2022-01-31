using System;

namespace ItechArt.Common
{
    public interface ILogger<T>
    {
        public void Log(LogLevel logLevel, string message, Exception exception = null);
    }
}