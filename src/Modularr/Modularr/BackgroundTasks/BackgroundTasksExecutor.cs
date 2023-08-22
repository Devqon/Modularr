using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Modularr.BackgroundTasks.Extensions;
using NCrontab;

namespace Modularr.BackgroundTasks;

internal class BackgroundTasksExecutor : BackgroundService
{
    private static readonly TimeSpan _runInterval = TimeSpan.FromMinutes(1);

    private readonly Dictionary<string, DateTime> _taskSchedules = new();

    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;

    public BackgroundTasksExecutor(IServiceProvider serviceProvider, ILogger<BackgroundTasksExecutor> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.Register(() =>
        {
            _logger.LogInformation("'{ServiceName' is stopping.", nameof(BackgroundTasksExecutor));
        });

        using var pollingTimer = new PeriodicTimer(_runInterval);

        do
        {
            using var scope = _serviceProvider.CreateScope();

            var backgroundTaskManager = scope.ServiceProvider.GetService<IBackgroundTaskManager>();
            var backgroundTasks = await backgroundTaskManager.GetBackgroundTasksAsync();

            var backgroundTasksToRun = backgroundTasks
                .Where(ShouldRun)
                .ToList();

            var backgroundTaskTasks = backgroundTasksToRun.Select(task => RunBackgroundTask(task, scope.ServiceProvider, stoppingToken));

            await Task.WhenAll(backgroundTaskTasks);
        }
        while (await pollingTimer.WaitForNextTickAsync(stoppingToken));
    }

    private bool ShouldRun(BackgroundTask backgroundTask)
    {
        if (!backgroundTask.Enabled)
        {
            return false;
        }

        var referenceDate = backgroundTask.LastStartDate ?? DateTime.MinValue;
        var nextRun = CrontabSchedule.Parse(backgroundTask.Schedule).GetNextOccurrence(referenceDate);

        return DateTime.UtcNow >= nextRun;
    }

    private async Task RunBackgroundTask(BackgroundTask backgroundTask, IServiceProvider serviceProvider, CancellationToken stoppingToken)
    {
        var taskName = backgroundTask.Name;
        var task = serviceProvider.GetServices<IBackgroundTask>().GetTaskByName(taskName);
        if (task is null)
        {
            return;
        }

        _logger.LogInformation("Start processing background task '{TaskName}'.", taskName);

        try
        {
            backgroundTask.Run();
            await task.DoWorkAsync(serviceProvider, stoppingToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while processing background task '{TaskName}'.", taskName);
        }
    }
}
