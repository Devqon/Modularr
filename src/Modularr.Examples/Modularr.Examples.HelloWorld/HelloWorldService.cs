namespace Modularr.Examples.HelloWorld;

internal class HelloWorldService : IHelloWorldService
{
    public Task<string> SayHelloAsync()
    {
        return Task.FromResult($"Hello from HelloWorld module! The time is now {DateTime.UtcNow:dd-MM-yyyy HH:mm:ss}");
    }
}
