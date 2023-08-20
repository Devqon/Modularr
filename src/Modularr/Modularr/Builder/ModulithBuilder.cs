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
        Services.AddSingleton<IModuleStartup>(moduleInstance);

        moduleInstance.ConfigureServices(Services);

        Services
            .AddControllers()
            // MAke Controllers, Razor pages, taghelpers etc. discoverable for the .NET framework
            .AddApplicationPart(typeof(TModule).Assembly);

        return this;
    }
}
