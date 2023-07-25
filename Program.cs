using Hangfire;
using Hangfire.MemoryStorage;
using hangfire_task_scheduler.Services;

GlobalConfiguration.Configuration.UseMemoryStorage();

 var options = new BackgroundJobServerOptions
{
    SchedulePollingInterval = TimeSpan.FromSeconds(1)
};

using (var server = new BackgroundJobServer(options))
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Servidor Hangfire iniciado. Pressione ");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write("[Esc] ");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("para sair\n");
    Console.ResetColor();

    var taskId = BackgroundJob.Enqueue(() => TaskServices.ExecutarTarefa("Tarefa disparada uma única vez", ConsoleColor.Red));
    
    BackgroundJob.ContinueJobWith(taskId, () => TaskServices.ExecutarTarefa("Tarefa disparada somente após a conclusão de uma determinada tarefa", ConsoleColor.Cyan));
    
    BackgroundJob.Schedule(() => TaskServices.ExecutarTarefa("Tarefa disparada conforme agendamento", ConsoleColor.White), TimeSpan.FromSeconds(30));

    RecurringJob.AddOrUpdate(Guid.NewGuid().ToString(), () => TaskServices.ExecutarTarefa("Tarefa disparada a cada 15s", ConsoleColor.Yellow), "*/15 * * * * *");
    RecurringJob.AddOrUpdate(Guid.NewGuid().ToString(), () => TaskServices.ExecutarTarefa("Tarefa disparada a cada 1min", ConsoleColor.Magenta), Cron.Minutely);

    Console.ReadKey(true);
}
