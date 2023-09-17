using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Modularr.Mvc.Utilities;

public static class EndpointRouteBuilderExtensions
{
    public static ControllerActionEndpointConventionBuilder MapModuleControllerRoutes(this IEndpointRouteBuilder routes, string moduleName)
    {
        return routes.MapAreaControllerRoute(
            name: moduleName,
            areaName: moduleName,
            pattern: $"/{moduleName}/{{controller={moduleName}}}/{{action=Index}}/{{id?}}"
        );
    }
}
