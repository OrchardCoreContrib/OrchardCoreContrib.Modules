using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Modules;
using System;

namespace OrchardCoreContrib.ReverseProxy.Yarp;

public class Startup : StartupBase
{
    private readonly IShellConfiguration _shellConfiguration;

    public Startup(IShellConfiguration shellConfiguration)
    {
        _shellConfiguration = shellConfiguration;
    }

    public override int Order => -1;

    public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        routes.MapReverseProxy();
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services
            .AddReverseProxy()
            .LoadFromConfig(_shellConfiguration.GetSection("OrchardCoreContrib_Yarp"));
    }
}
