namespace Modularr.MultiTenancy;

public interface ITenant
{
    /// <summary>
    /// A display friendly name of this tenant.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// The unique identifier for this tenant.
    /// </summary>
    string Identifier { get; }

    /// <summary>
    /// Optional extra properties for this tenant.
    /// </summary>
    public Dictionary<string, object> Properties { get; }
}
