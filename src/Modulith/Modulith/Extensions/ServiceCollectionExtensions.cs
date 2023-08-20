using Modulith.Builder;
using Modulith.Modules;
using Modulith.Modules.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static ModulithBuilder AddModulith(this IServiceCollection services, Action<ModulithBuilder> configure)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        var builder = services.LastOrDefault(sd => sd.ServiceType == typeof(ModulithBuilder))?.ImplementationInstance as ModulithBuilder;
        var shouldCreate = builder is null;

        if (shouldCreate)
        {
            builder = new ModulithBuilder(services);
            services.AddSingleton(builder);
        }

        configure?.Invoke(builder);

        if (shouldCreate)
        {
            AddDefaultServices(builder);
            AddFrameworks(builder);
            AddModulithServices(builder);
        }

        return builder;
    }

    private static void AddDefaultServices(ModulithBuilder builder)
    {
        var services = builder.Services;

        services.AddLogging();
        services.AddOptions();

        services.AddLocalization();

        services.AddHttpContextAccessor();
    }

    private static void AddFrameworks(ModulithBuilder builder)
    {
        var services = builder.Services;

        services.AddControllersWithViews();
        services.AddRazorPages();
    }

    private static void AddModulithServices(ModulithBuilder builder)
    {
        var services = builder.Services;

        services.AddTransient<IModuleManager, ModuleManager>();
        services.AddTransient<IModuleRepository, ModuleRepository>();
    }
}
