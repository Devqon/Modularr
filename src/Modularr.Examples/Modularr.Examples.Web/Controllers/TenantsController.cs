using Microsoft.AspNetCore.Mvc;
using Modularr.MultiTenancy;

namespace Modularr.Examples.Web.Controllers;

public class TenantsController : Controller
{
    private readonly ITenantContext _tenantContext;

    public TenantsController(ITenantContext tenantContext)
    {
        _tenantContext = tenantContext;
    }

    [HttpGet]
    public IActionResult Current()
    {
        var currentTenant = _tenantContext.Current;
        return View(currentTenant);
    }
}
