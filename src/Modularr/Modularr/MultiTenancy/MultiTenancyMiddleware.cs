using Microsoft.AspNetCore.Http;

namespace Modularr.MultiTenancy;

/// <summary>
/// Middleware for storing the current tenant in the <see cref="HttpContext.Items"/>.
/// </summary>
internal class MultiTenancyMiddleware : IMiddleware
{
    private readonly ITenantIdentificationStrategy _tenantIdentificationStrategy;
    private readonly ITenantStore _tenantStore;

    public MultiTenancyMiddleware(ITenantIdentificationStrategy tenantIdentificationStrategy, ITenantStore tenantStore)
    {
        _tenantIdentificationStrategy = tenantIdentificationStrategy;
        _tenantStore = tenantStore;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Items[Constants.HTTP_ITEMS_CURRENT_TENANT] is null)
        {
            var currentTenantIdentifier = await _tenantIdentificationStrategy.IdentifyAsync();
            if (!string.IsNullOrEmpty(currentTenantIdentifier))
            {
                var tenant = await _tenantStore.GetTenantAsync(currentTenantIdentifier);
                context.Items[Constants.HTTP_ITEMS_CURRENT_TENANT] = tenant;
            }
        }

        await next(context);
    }
}
