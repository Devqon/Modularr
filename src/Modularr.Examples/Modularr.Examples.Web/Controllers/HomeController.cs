using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Modularr.Examples.HelloWorld;
using Modularr.Modules;
using Modularr.Web.Models;

namespace Modularr.Web.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IModuleManager _moduleManager;
    private readonly IHelloWorldService _helloWorldService;

    public HomeController(ILogger<HomeController> logger, IModuleManager moduleManager, IHelloWorldService helloWorldService)
    {
        _logger = logger;
        _moduleManager = moduleManager;
        _helloWorldService = helloWorldService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> Modules()
    {
        var modules = await _moduleManager.GetModulesAsync();
        return View(modules);
    }

    public async Task<IActionResult> Hello()
    {
        var helloMessage = await _helloWorldService.SayHelloAsync();
        return View(model: helloMessage);   
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
