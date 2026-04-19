using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCoreContrib.Ban.Drivers;
using OrchardCoreContrib.Ban.Services;

namespace OrchardCoreContrib.Ban;

public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IIPBanService, IPBanService>();

        services.AddSiteDisplayDriver<BanSettingsDisplayDriver>();

        services.AddPermissionProvider<Permissions>();
        services.AddNavigationProvider<AdminMenu>();
    }

    public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        => app.UseMiddleware<IPBanMiddleware>();
}

