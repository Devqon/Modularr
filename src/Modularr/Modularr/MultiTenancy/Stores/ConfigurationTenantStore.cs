using Microsoft.Extensions.Configuration;

namespace Modularr.MultiTenancy.Stores;

/// <summary>
/// Tenant store using the <see cref="IConfiguration"/>.
/// </summary>
internal class ConfigurationTenantStore : ITenantStore
{
    private const string TENANTS_SECTION = "Tenants";

    private readonly IConfiguration _configuration;

    public ConfigurationTenantStore(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<ITenant> GetTenantAsync(string tenantIdentifier)
    {
        var allTenants = await GetTenantsAsync();
        return allTenants.SingleOrDefault(t => t.Identifier.Equals(tenantIdentifier, StringComparison.OrdinalIgnoreCase));
    }

    public Task<IEnumerable<ITenant>> GetTenantsAsync()
    {
        var tenants = _configuration
            .GetSection(TENANTS_SECTION)
            .Get<IEnumerable<Tenant>>();

        return Task.FromResult<IEnumerable<ITenant>>(tenants);
    }
}
