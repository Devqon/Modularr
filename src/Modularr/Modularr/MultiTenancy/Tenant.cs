namespace Modularr.MultiTenancy;

/// <summary>
/// A default implementation of the <see cref="ITenant"/>.
/// </summary>
public class Tenant : ITenant
{
    public string Name { get; set; }

    public string Identifier { get; set; }

    public Dictionary<string, object> Properties { get; set; } = new();
}
