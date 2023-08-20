using Microsoft.Extensions.DependencyInjection;
using Modularr.Modules;

namespace Modularr.Examples.HelloWorld;

public class HelloWorldModule : Module
{
    public override string Name => "HelloWorld";

    public override string Description => "A module for printing Hello World messages";

    public override string Version => "0.0.1";

    public override string Category => "Examples";

    public override string[] Tags => new[] { "Demo", "HelloWorld" };

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IHelloWorldService, HelloWorldService>();
    }
}
