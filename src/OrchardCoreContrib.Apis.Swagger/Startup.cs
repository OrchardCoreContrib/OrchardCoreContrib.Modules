using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCoreContrib.OpenApi.Abstractions;

namespace OrchardCoreContrib.Apis.Swagger
{
    /// <summary>
    /// Represensts a startup point to register the required services by Swagger module.
    /// </summary>
    public class Startup : StartupBase
    {
        /// <inheritdoc/>
        public override void ConfigureServices(IServiceCollection services)
        {
            var swaggerApiDefinition = new SwaggerApiDefinition();
            
            services.AddTransient<IOpenApiDefinition>(sp => swaggerApiDefinition);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(swaggerApiDefinition.Version, swaggerApiDefinition.Info);
            });
        }

        /// <inheritdoc/>
        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
            => builder.UseSwagger();
    }
}
