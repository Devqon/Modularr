using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Modularr.Examples.HelloWorld;
using Modularr.Web.Models;

namespace Modularr.Web.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHelloWorldService _helloWorldService;

    public HomeController(ILogger<HomeController> logger, IHelloWorldService helloWorldService)
    {
        _logger = logger;
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
