namespace hangfire_task_scheduler.Services;

public static class TaskServices
{
    private static readonly object consoleLock = new();

    public static void ExecutarTarefa(string descricao, ConsoleColor consoleColor)
    {
        lock (consoleLock)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine($"{DateTime.Now}: {descricao}");
            Console.ResetColor();
        }
    }
}
