using Api.Models;

namespace Api.Services;

public class LogService(IHttpContextAccessor httpContextAccessor) : ILogService
{
    private static String _path = "logs/log.txt";

    public void LogAction(String action, bool success)
    {
        DateTime dateTime = DateTime.Now;
        LogModel model = new()
        {
            Date = DateOnly.FromDateTime(dateTime),
            Time = TimeOnly.FromDateTime(dateTime),
            Action = action,
            Status = success ? "Успех" : "Провал",
            Ip = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "Неизвестный ip-адрес"
        };

        StreamWriter stream = File.AppendText(_path);
        stream.WriteLine(model.ToString());
        stream.Close();
    }

    public List<LogModel> GetLogs()
    {
        String[] lines = File.ReadAllLines(_path);
        List<LogModel> models = new();
        foreach (var line in lines)
        {
            if (line.Length < 1) break;

            String[] column = line.Split(" | ");
            String time = column[1].Replace("-", ":");
            models.Add(new()
            {
                Date = DateOnly.FromDateTime(Convert.ToDateTime(column[0])),
                Time = TimeOnly.Parse(time),
                Ip = column[2],
                Action = column[3],
                Status = column[4],
            });
        }

        return models;
    }
}