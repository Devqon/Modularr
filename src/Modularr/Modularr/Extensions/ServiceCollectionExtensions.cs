using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Modularr.BackgroundTasks;
using Modularr.Builder;
using Modularr.Modules;
using Modularr.Modules.Repositories;
using Modularr.Mvc;

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

    public static IServiceCollection AddBackgroundTask<TBackgroundTask>(this IServiceCollection services)
        where TBackgroundTask : class, IBackgroundTask
    {
        services.AddSingleton<IBackgroundTask, TBackgroundTask>();
        return services;
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

        services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        services.Configure<RazorViewEngineOptions>(options =>
        {
            options.ViewLocationExpanders.Add(new ModuleViewLocationExpander());
        });
        services.AddControllersWithViews();

        services.AddRazorPages();
    }

    private static void AddModularrServices(ModularrBuilder builder)
    {
        var services = builder.Services;

        services.AddTransient<IModuleManager, ModuleManager>();
        services.AddTransient<IModuleRepository, ModuleRepository>();

        services.AddHostedService<BackgroundTasksExecutor>();
        services.AddSingleton<IBackgroundTaskManager, BackgroundTaskManager>();
    }
}
