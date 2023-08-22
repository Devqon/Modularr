using Microsoft.AspNetCore.Mvc;
using Modularr.Examples.HelloWorld.ViewModels;

namespace Modularr.Examples.HelloWorld.Controllers;

public class HelloWorldController : Controller
{
    private readonly IHelloWorldService _helloWorldService;

    public HelloWorldController(IHelloWorldService helloWorldService)
    {
        _helloWorldService = helloWorldService;
    }

    public async Task<ActionResult> Index()
    {
        var message = await _helloWorldService.SayHelloAsync();
        var model = new HelloWorldViewModel
        {
            Message = message,
        };

        return View(model);
    }
}
