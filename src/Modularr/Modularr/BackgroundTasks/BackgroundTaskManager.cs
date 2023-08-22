using System.Collections.Concurrent;
using Modularr.BackgroundTasks.Extensions;

namespace Modularr.BackgroundTasks;

internal class BackgroundTaskManager : IBackgroundTaskManager
{
    private bool _initialized = false;
    private readonly ConcurrentDictionary<string, BackgroundTask> _tasks = new();

    private readonly IEnumerable<IBackgroundTask> _backgroundTasks;

    public BackgroundTaskManager(IEnumerable<IBackgroundTask> backgroundTasks)
    {
        _backgroundTasks = backgroundTasks;
    }

    public Task<IEnumerable<BackgroundTask>> GetBackgroundTasksAsync()
    {
        EnsureInitialised();

        return Task.FromResult<IEnumerable<BackgroundTask>>(_tasks.Values.ToList());
    }

    public Task Enable(BackgroundTask backgroundTask) => Enable(backgroundTask.Name);

    public Task Enable(string taskName)
    {
        EnsureInitialised();

        lock (_tasks)
        {
            if (_tasks.ContainsKey(taskName))
            {
                _tasks[taskName].Enabled = true;
            }
        }

        return Task.CompletedTask;
    }

    public Task Disable(BackgroundTask backgroundTask) => Disable(backgroundTask.Name);

    public Task Disable(string taskName)
    {
        EnsureInitialised();

        lock (_tasks)
        {
            if (_tasks.ContainsKey(taskName))
            {
                _tasks[taskName].Enabled = false;
            }
        }

        return Task.CompletedTask;
    }

    private void EnsureInitialised()
    {
        if (_initialized)
        {
            return;
        }

        lock (_tasks)
        {
            foreach (var task in _backgroundTasks)
            {
                var taskName = task.TaskName();
                var backgroundTask = new BackgroundTask(taskName, task.Schedule)
                {
                    Enabled = true,
                };

                _tasks[taskName] = backgroundTask;
            }

            _initialized = true;
        }
    }
}
