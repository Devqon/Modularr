using System.Diagnostics.CodeAnalysis;
using Modularr.Builder;

namespace Modularr.MultiTenancy.Extensions;

[ExcludeFromCodeCoverage]
public static class ModularrBuilderExtensions
{
    /// <summary>
    /// Adds support for multitenancy to Modularr.
    /// </summary>
    public static ModularrBuilder WithMultiTenancy(this ModularrBuilder modularrBuilder)
    {
        modularrBuilder.Services.AddMultiTenancy();
        modularrBuilder.Configure(builder =>
        {
            builder.UseMultiTenancy();
        });

        return modularrBuilder;
    }
}
