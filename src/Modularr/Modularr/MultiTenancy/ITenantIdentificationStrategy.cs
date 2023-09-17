namespace Modularr.MultiTenancy;

public interface ITenantIdentificationStrategy
{
    Task<string> IdentifyAsync();
}
