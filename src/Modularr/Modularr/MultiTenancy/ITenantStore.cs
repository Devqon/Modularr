namespace Modularr.MultiTenancy;

/// <summary>
/// A store for tenants.
/// </summary>
public interface ITenantStore
{
    /// <summary>
    /// Get the tenant with the given identifier.
    /// </summary>
    /// <param name="tenantIdentifier">The identifier of the tenant.</param>
    /// <returns>A tenant if found.</returns>
    Task<ITenant> GetTenantAsync(string tenantIdentifier);

    /// <summary>
    /// Get all tenants.
    /// </summary>
    /// <returns>A collection of <see cref="ITenant"></see></returns>
    Task<IEnumerable<ITenant>> GetTenantsAsync();
}
