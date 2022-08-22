using ProjekatRVA.Enums;

namespace ProjekatRVA.Logger
{
    public interface ILogger
    {
        void LogEvent(ELog log, string _event);
    }
}
