using Modularr.Builder;
using Modularr.Modules;
using Modularr.Modules.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static ModularrBuilder AddModularr(this IServiceCollection services, Action<ModularrBuilder> configure)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        var builder = services.LastOrDefault(sd => sd.ServiceType == typeof(ModularrBuilder))?.ImplementationInstance as ModularrBuilder;
        var shouldCreate = builder is null;

        if (shouldCreate)
        {
            builder = new ModularrBuilder(services);
            services.AddSingleton(builder);
        }

        configure?.Invoke(builder);

        if (shouldCreate)
        {
            AddDefaultServices(builder);
            AddFrameworks(builder);
            AddModularrServices(builder);
        }

        return builder;
    }

    private static void AddDefaultServices(ModularrBuilder builder)
    {
        var services = builder.Services;

        services.AddLogging();
        services.AddOptions();

        services.AddLocalization();

        services.AddHttpContextAccessor();
    }

    private static void AddFrameworks(ModularrBuilder builder)
    {
        var services = builder.Services;

        services.AddControllersWithViews();
        services.AddRazorPages();
    }

    private static void AddModularrServices(ModularrBuilder builder)
    {
        var services = builder.Services;

        services.AddTransient<IModuleManager, ModuleManager>();
        services.AddTransient<IModuleRepository, ModuleRepository>();
    }
}
