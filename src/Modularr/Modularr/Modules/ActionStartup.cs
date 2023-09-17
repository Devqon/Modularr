using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Modularr.Modules;

internal class ActionStartup : IStartup
{
    private readonly Action<IApplicationBuilder, IEndpointRouteBuilder, IServiceProvider> _startupAction;

    public int Order { get; }

    public ActionStartup(Action<IApplicationBuilder, IEndpointRouteBuilder, IServiceProvider> startupAction, int order = 0)
    {
        _startupAction = startupAction;
    }

    public void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        _startupAction?.Invoke(builder, routes, serviceProvider);
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Nothing
    }
}
