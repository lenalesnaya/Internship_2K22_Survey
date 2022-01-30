using Microsoft.Extensions.Logging;
using System;

namespace ItechArt.Common
{
    public interface ILogger
    {
        public void Write(LogLevel logLevel, string logMessage, Exception exception = null);
    }
}