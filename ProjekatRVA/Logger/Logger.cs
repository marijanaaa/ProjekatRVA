using ProjekatRVA.Enums;
using Serilog;

namespace ProjekatRVA.Logger
{
    public class Logger : ILogger
    {
        public Logger() {
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Debug()
                            .WriteTo.File("logs/logs.txt", rollingInterval: RollingInterval.Year, shared: true)
                            .CreateLogger();
        }
        public void LogEvent(ELog log, string _event)
        {
            if (log == ELog.INFO)
            {
                Log.Information(_event);
            }
            if (log == ELog.ERROR)
            {
                Log.Error(_event);
            }
        }
    }
}
