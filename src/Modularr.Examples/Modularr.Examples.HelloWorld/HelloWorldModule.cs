using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Modularr.Examples.HelloWorld.BackgroundTasks;
using Modularr.Modules;
using Modularr.Mvc.Utilities;

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

        services.AddBackgroundTask<HelloWorldBackgroundTask>();
    }

    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        routes.MapModuleControllerRoutes(
            moduleName: Name
        );
    }
}
