using Microsoft.AspNetCore.Mvc;

namespace Modularr.Examples.HelloWorld.Controllers.Api;

[ApiController]
[Route("api/HelloWorld")]
public class HelloWorldApiController : ControllerBase
{
    private readonly IHelloWorldService _helloWorldService;

    public HelloWorldApiController(IHelloWorldService helloWorldService)
    {
        _helloWorldService = helloWorldService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var message = await _helloWorldService.SayHelloAsync();
        return Ok(new { message });
    }
}
