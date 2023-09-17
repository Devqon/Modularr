using Microsoft.AspNetCore.Http;
using Modularr.MultiTenancy;
using Modularr.TestHelpers;

namespace Modularr.Tests.MultiTenancy;

public class MultiTenancyMiddlewareTests
{
    [Theory]
    [ModularrAutoData]
    internal async Task InvokeAsync_Should_AddFoundTenantToHttpContextItems(
        HttpContext httpContext,
        RequestDelegate next,
        [Frozen] ITenantIdentificationStrategy tenantIdentificationStrategyMock,
        [Frozen] ITenantStore tenantStoreMock,
        ITenant tenantMock,
        
        MultiTenancyMiddleware sut)
    {
        // Arrange
        const string tenantIdentifier = "TENANT_IDENTIFIER";
        tenantMock.Identifier.Returns(tenantIdentifier);

        tenantIdentificationStrategyMock.IdentifyAsync().Returns(tenantIdentifier);
        tenantStoreMock.GetTenantAsync(tenantIdentifier).Returns(tenantMock);

        // Act
        await sut.InvokeAsync(httpContext, next);

        // Assert
        httpContext.Items[Constants.HTTP_ITEMS_CURRENT_TENANT].Should().Be(tenantMock);
    }
}
