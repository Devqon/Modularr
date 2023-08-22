using Microsoft.AspNetCore.Mvc;

namespace Modularr.Mvc.Utilities;

public static class ControllerExtensions
{
    public static string ControllerName(this Type controllerType)
    {
        if (!typeof(Controller).IsAssignableFrom(controllerType))
        {
            throw new ArgumentException("The specified type must inherit from " + nameof(Controller), nameof(controllerType));
        }

        return controllerType.Name.EndsWith(nameof(Controller), StringComparison.OrdinalIgnoreCase)
            ? controllerType.Name[..^nameof(Controller).Length]
            : controllerType.Name;
    }
}
