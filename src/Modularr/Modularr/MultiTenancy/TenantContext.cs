using Microsoft.AspNetCore.Http;
using Modularr.MultiTenancy.Extensions;

namespace Modularr.MultiTenancy;

/// <inheritdoc/>
internal class TenantContext : ITenantContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc/>
    public ITenant Current => _httpContextAccessor.HttpContext.GetCurrentTenant();
}
