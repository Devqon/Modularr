using Microsoft.Extensions.DependencyInjection;
using Modulith.Modules;

namespace Modulith.Builder;

public class ModulithBuilder
{
    internal ModulithBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public IServiceCollection Services { get; }

    public ModulithBuilder AddModule<TModule>()
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
