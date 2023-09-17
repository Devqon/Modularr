using Microsoft.AspNetCore.Http;

namespace Modularr.MultiTenancy.Strategies;

/// <summary>
/// Tenant identification strategy by using the 'tenant' query.
/// </summary>
/// <example>
/// http://example.com?tenant=1
/// </example>
internal class QueryBasedTenantIdentificationStrategy : ITenantIdentificationStrategy
{
    private const string TENANT_QUERY_NAME = "tenant";

    private readonly IHttpContextAccessor _httpContextAccessor;

    public QueryBasedTenantIdentificationStrategy(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<string> IdentifyAsync()
    {
        Task<string> Resolve(string tenantId = null)
        {
            return Task.FromResult(tenantId);
        }

        if (_httpContextAccessor.HttpContext is null)
        {
            return Resolve();
        }

        var tenantQuery = _httpContextAccessor.HttpContext.Request.Query[TENANT_QUERY_NAME];
        return Resolve(tenantQuery);
    }
}
