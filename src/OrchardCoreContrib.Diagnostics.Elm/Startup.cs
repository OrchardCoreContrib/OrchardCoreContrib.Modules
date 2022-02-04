using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using System;

namespace OrchardCoreContrib.Diagnostics.Elm
{
    /// <summary>
    /// Represensts a startup point to register the required services by Elm diagnostics module.
    /// </summary>
    public class Startup : StartupBase
    {
        /// <inheritdoc/>
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddElm(options => options.Path = new PathString("/elm"));
        }

        /// <inheritdoc/>
        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            builder.UseElmCapture();
            builder.UseElmPage();
        }
    }
}
