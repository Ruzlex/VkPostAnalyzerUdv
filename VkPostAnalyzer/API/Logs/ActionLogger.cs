namespace VkPostAnalyzer.Logs;

public class ActionLogger
{
    private readonly string _logFilePath = Path.Combine("Logs", "analysis.log");

    public void LogStart(long ownerId)
    {
        File.AppendAllText(_logFilePath, 
            $"[{DateTime.UtcNow}] Начало анализа для пользователя {ownerId}\n");
    }

    public void LogEnd(long ownerId)
    {
        File.AppendAllText(_logFilePath, 
            $"[{DateTime.UtcNow}] Анализ завершен для пользователя {ownerId}\n");
    }
}