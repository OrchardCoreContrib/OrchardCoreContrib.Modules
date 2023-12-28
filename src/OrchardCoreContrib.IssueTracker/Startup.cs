using Fluid;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using OrchardCoreContrib.IssueTracker.Drivers;
using OrchardCoreContrib.IssueTracker.Handlers;
using OrchardCoreContrib.IssueTracker.Models;
using OrchardCoreContrib.IssueTracker.Settings;
using OrchardCoreContrib.IssueTracker.ViewModels;

namespace OrchardCoreContrib.IssueTracker;
public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<TemplateOptions>(o =>
        {
            o.MemberAccessStrategy.Register<IssuePartViewModel>();
        });

        services.AddContentPart<IssuePart>()
            .UseDisplayDriver<IssuePartDisplayDriver>()
            .AddHandler<IssuePartHandler>();

        services.AddScoped<IContentTypePartDefinitionDisplayDriver, IssuePartSettingsDisplayDriver>();
        services.AddDataMigration<Migrations>();
    }

    public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
    {
        routes.MapAreaControllerRoute(
            name: "Home",
            areaName: "OrchardCoreContrib.IssueTracker",
            pattern: "Home/Index",
            defaults: new { controller = "Home", action = "Index" }
        );
    }
}
