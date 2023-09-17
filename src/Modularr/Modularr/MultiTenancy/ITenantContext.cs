namespace Modularr.MultiTenancy;

/// <summary>
/// Interface for retrieving the current tenant.
/// </summary>
public interface ITenantContext
{
    /// <summary>
    /// The current tenant for the this context.
    /// </summary>
    ITenant Current { get; }
}
