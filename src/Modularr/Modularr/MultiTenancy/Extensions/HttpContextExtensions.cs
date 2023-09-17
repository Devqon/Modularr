using Microsoft.AspNetCore.Http;

namespace Modularr.MultiTenancy.Extensions;

public static class HttpContextExtensions
{
    /// <summary>
    /// Retrieve the current tenant from the http context.
    /// </summary>
    public static ITenant GetCurrentTenant(this HttpContext httpContext)
    {
        return httpContext.Items[Constants.HTTP_ITEMS_CURRENT_TENANT] as ITenant;
    }
}
