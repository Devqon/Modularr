using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Modularr.MultiTenancy.Stores;
using Modularr.MultiTenancy.Strategies;

namespace Modularr.MultiTenancy.Builder;

[ExcludeFromCodeCoverage]
public class MultiTenancyBuilder
{
    internal MultiTenancyBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public IServiceCollection Services { get; }

    /// <summary>
    /// Use the given tenant identification strategy for identifying tenants.
    /// </summary>
    public MultiTenancyBuilder UseIdentificationStrategy<TTenantIdentificationStrategy>()
        where TTenantIdentificationStrategy : class, ITenantIdentificationStrategy
    {
        Services.AddTransient<ITenantIdentificationStrategy, TTenantIdentificationStrategy>();
        return this;
    }

    /// <summary>
    /// Use the given tenant store for retrieving tenant information.
    /// </summary>
    public MultiTenancyBuilder UseTenantStore<TTenantStore>()
        where TTenantStore : class, ITenantStore
    {
        Services.AddTransient<ITenantStore, TTenantStore>();
        return this;
    }

    /// <summary>
    /// Uses defaults for the <see cref="ITenantIdentificationStrategy"/> and the <see cref="ITenantStore"/>.
    /// </summary>
    public MultiTenancyBuilder UseDefaults()
    {
        UseIdentificationStrategy<QueryBasedTenantIdentificationStrategy>();
        UseTenantStore<ConfigurationTenantStore>();
        return this;
    }
}
