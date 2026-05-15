using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.Elm;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Modules;

namespace OrchardCoreContrib.Diagnostics.Elm;

/// <summary>
/// Represensts a startup point to register the required services by Elm diagnostics module.
/// </summary>
public class Startup(IShellConfiguration shellConfiguration) : StartupBase
{
    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<ElmOptions>(shellConfiguration.GetSection(Constants.ConfigurationKey));

        services.AddElm();
    }

    /// <inheritdoc/>
    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        builder.UseElmPage();
    }
}
