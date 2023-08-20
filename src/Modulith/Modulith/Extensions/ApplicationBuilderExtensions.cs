using Microsoft.Extensions.DependencyInjection;
using Modulith.Modules;

namespace Microsoft.AspNetCore.Builder;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseModulith(this IApplicationBuilder app)
    {
        var moduleStartups = app.ApplicationServices.GetServices<IModuleStartup>().OrderBy(ms => ms.Order);

        app
            .UseRouting()
            .UseEndpoints(routes =>
        {
            foreach (var moduleStartup in moduleStartups)
            {
                moduleStartup.Configure(app, routes, app.ApplicationServices);
            }
        });

        return app;
    }
}
