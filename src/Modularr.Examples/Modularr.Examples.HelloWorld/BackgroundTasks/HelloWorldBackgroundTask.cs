using Modularr.BackgroundTasks;

namespace Modularr.Examples.HelloWorld.BackgroundTasks;

internal class HelloWorldBackgroundTask : IBackgroundTask
{
    private readonly IHelloWorldService _helloWorldService;

    public string Schedule => "*/1 * * * *";

    public HelloWorldBackgroundTask(IHelloWorldService helloWorldService)
    {
        _helloWorldService = helloWorldService;
    }

    public async Task DoWorkAsync(IServiceProvider serviceProvider)
    {
        var message = await _helloWorldService.SayHelloAsync();

        Console.WriteLine(message);
    }
}
