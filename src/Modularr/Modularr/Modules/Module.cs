using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Modularr.Modules;

public abstract class Module : IModule, IModuleStartup
{
    /// <inheritdoc/>
    public abstract string Name { get; }

    /// <inheritdoc/>
    public abstract string Description { get; }

    /// <inheritdoc/>
    public abstract string Version { get; }

    /// <inheritdoc/>
    public virtual string Category { get; } = "None";

    /// <inheritdoc/>
    public virtual string Author { get; } = "";

    /// <inheritdoc/>
    public virtual string AuthorUrl { get; } = "";

    /// <inheritdoc/>
    public virtual string[] Dependencies { get; } = Array.Empty<string>();

    /// <inheritdoc/>
    public virtual string[] Tags { get; } = Array.Empty<string>();

    /// <inheritdoc/>
    public virtual int Order => 0;

    /// <inheritdoc/>
    public virtual void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
    }

    /// <inheritdoc/>
    public virtual void ConfigureServices(IServiceCollection services)
    {
    }
}
