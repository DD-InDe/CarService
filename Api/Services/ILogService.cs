using Api.Models;

namespace Api.Services;

public interface ILogService
{
    void LogAction(String action, bool success);
    List<LogModel> GetLogs();
}