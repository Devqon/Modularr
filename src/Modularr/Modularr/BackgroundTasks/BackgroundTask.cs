namespace Modularr.BackgroundTasks;

public class BackgroundTask
{
    public BackgroundTask(string name, string schedule)
    {
        Name = name;
        Schedule = schedule;
    }

    public string Name { get; }

    public string Schedule { get; }

    public bool Enabled { get; internal set; }

    public DateTime? LastStartDate { get; private set; }

    public void Run()
    {
        LastStartDate = DateTime.UtcNow;
    }
}
