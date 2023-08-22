using Microsoft.AspNetCore.Mvc.Razor;

namespace Modularr.Mvc;

internal class ModuleViewLocationExpander : IViewLocationExpander
{
    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
        if (string.IsNullOrEmpty(context.AreaName))
        {
            return viewLocations;
        }

        return viewLocations.Concat(new[]
        {
            "/Views/{1}/{0}.cshtml",
            "/Views/Shared/{0}.cshtml",
            "/Views/{2}/{1}/{0}.cshtml",
            "/Views/{2}/Shared/{0}.cshtml"
        });
    }

    public void PopulateValues(ViewLocationExpanderContext context)
    {
    }
}
