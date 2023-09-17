using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Modularr.MultiTenancy.Builder;

namespace Modularr.MultiTenancy;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the multitenancy services to the collection.
    /// </summary>
    /// <param name="options">An optional configuration action for the <see cref="MultiTenancyBuilder"/>.</param>
    public static IServiceCollection AddMultiTenancy(this IServiceCollection services, Action<MultiTenancyBuilder> options = null)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<ITenantContext, TenantContext>();
        services.AddTransient<MultiTenancyMiddleware>();

        var builder = new MultiTenancyBuilder(services);
        options?.Invoke(builder);

        if (HasIncompleteConfiguration(services))
        {
            builder.UseDefaults();
        }

        return services;
    }

    /// <summary>
    /// Adds the multitenancy middleware to the application's request pipeline.
    /// </summary>
    public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<MultiTenancyMiddleware>();
        return builder;
    }

    private static bool HasIncompleteConfiguration(IServiceCollection services)
    {
        return
            !services.Any(s => s.ServiceType == typeof(ITenantStore))
            || !services.Any(s => s.ServiceType == typeof(ITenantIdentificationStrategy));
    }
}
