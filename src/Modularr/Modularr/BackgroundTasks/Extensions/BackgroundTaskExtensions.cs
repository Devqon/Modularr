namespace Modularr.BackgroundTasks.Extensions;

public static class BackgroundTaskExtensions
{
    public static string TaskName(this IBackgroundTask task)
    {
        return task.GetType().FullName;
    }

    public static IBackgroundTask GetTaskByName(this IEnumerable<IBackgroundTask> backgroundTasks, string name)
    {
        return backgroundTasks.LastOrDefault(bt => bt.TaskName() == name);
    }
}
