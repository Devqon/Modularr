namespace Modularr.BackgroundTasks;

public interface IBackgroundTask
{
    string Schedule { get; }

    Task DoWorkAsync(IServiceProvider serviceProvider, CancellationToken stoppingToken);
}
