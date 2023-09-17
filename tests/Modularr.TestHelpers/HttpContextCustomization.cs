using AutoFixture;
using Microsoft.AspNetCore.Http;

namespace Modularr.TestHelpers;

internal class HttpContextCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register<HttpContext>(() => new DefaultHttpContext());
        fixture.Register<RequestDelegate>(() => (HttpContext hc) => Task.CompletedTask);
    }
}
