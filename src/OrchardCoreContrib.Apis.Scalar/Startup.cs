using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCoreContrib.OpenApi.Abstractions;
using Scalar.AspNetCore;

namespace OrchardCoreContrib.Apis.Scalar;

public sealed class Startup : StartupBase
{
    public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        var swaggerApiDefinition = serviceProvider.GetService<IOpenApiDefinition>();

        routes.MapScalarApiReference(options =>
            options.OpenApiRoutePattern = $"/swagger/{swaggerApiDefinition.Version}/swagger.json");
    }
}
