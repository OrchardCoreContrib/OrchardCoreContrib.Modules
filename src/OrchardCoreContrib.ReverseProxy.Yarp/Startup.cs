using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Modules;
using System;

namespace OrchardCoreContrib.ReverseProxy.Yarp;

public class Startup(IShellConfiguration shellConfiguration) : StartupBase
{
    public override int Order => -1;

    public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        routes.MapReverseProxy();
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services
            .AddReverseProxy()
            .LoadFromConfig(shellConfiguration.GetSection(Constants.ConfigurationSectionName));
    }
}
