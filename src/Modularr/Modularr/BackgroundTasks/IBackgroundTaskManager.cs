namespace Modularr.BackgroundTasks;

public interface IBackgroundTaskManager
{
    Task<IEnumerable<BackgroundTask>> GetBackgroundTasksAsync();

    Task Enable(BackgroundTask backgroundTask);

    Task Enable(string taskName);

    Task Disable(BackgroundTask backgroundTask);

    Task Disable(string taskName);

}
