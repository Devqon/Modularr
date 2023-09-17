using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Modularr.Modules;

namespace Modularr.Builder;

public class ModularrBuilder
{
    internal ModularrBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public IServiceCollection Services { get; }

    public ModularrBuilder AddModule<TModule>()
        where TModule : Module
    {
        var moduleInstance = (TModule) Activator.CreateInstance(typeof(TModule));
        Services.AddSingleton<TModule>(moduleInstance);
        Services.AddSingleton<IModule>(moduleInstance);
        AddStartup(moduleInstance);

        moduleInstance.ConfigureServices(Services);

        Services
            .AddControllers()
            // MAke Controllers, Razor pages, taghelpers etc. discoverable for the .NET framework
            .AddApplicationPart(typeof(TModule).Assembly);

        return this;
    }

    public ModularrBuilder Configure(Action<IApplicationBuilder, IEndpointRouteBuilder, IServiceProvider> startupAction, int order = 0)
    {
        var actionStartup = new ActionStartup(startupAction, order);
        AddStartup(actionStartup);
        return this;
    }

    public ModularrBuilder Configure(Action<IApplicationBuilder> startupAction, int order = 0)
    {
        var actionStartup = new ActionStartup((builder, _, _) => startupAction(builder), order);
        AddStartup(actionStartup);
        return this;
    }

    public ModularrBuilder Configure(Action<IServiceProvider> startupAction, int order = 0)
    {
        var actionStartup = new ActionStartup((_, _, serviceProvider) => startupAction(serviceProvider), order);
        AddStartup(actionStartup);
        return this;
    }

    public ModularrBuilder AddStartup(IStartup startup)
    {
        Services.AddSingleton<IStartup>(startup);
        return this;
    }
}
