using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell;
using OrchardCore.Modules;
using OrchardCoreContrib.OpenApi.Abstractions;

namespace OrchardCoreContrib.Apis.Swagger
{
    /// <summary>
    /// Represensts a startup point to register the required services by Swagger UI feature.
    /// </summary>
    [Feature("OrchardCore.Apis.Swagger.UI")]
    public class SwaggerUIStratup : StartupBase
    {
        /// <inheritdoc/>
        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            builder.UseSwaggerUI(options =>
            {
                var swaggerApiDefinition = serviceProvider.GetService<IOpenApiDefinition>();
                var shellSettings = serviceProvider.GetService<ShellSettings>();
                var tenantUrlPrefix = String.IsNullOrEmpty(shellSettings.RequestUrlPrefix)
                    ? shellSettings.RequestUrlPrefix
                    : shellSettings.RequestUrlPrefix + "/";

                options.SwaggerEndpoint($"/{tenantUrlPrefix}swagger/{swaggerApiDefinition.Version}/swagger.json", $"{swaggerApiDefinition.Name} {swaggerApiDefinition.Version}");
            });
        }
    }
}
