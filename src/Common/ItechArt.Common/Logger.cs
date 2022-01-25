using Serilog;
using Serilog.Events;

namespace ItechArt.Common
{
    public class Logger : ILogger
    {
        public void Write(LogEvent logEvent)
        {
            using var log = new LoggerConfiguration().WriteTo.File(
                "../../Survey/ItechArt.Survey.WebApp/bin/Debug/net6.0/logs/Survey.log",
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}