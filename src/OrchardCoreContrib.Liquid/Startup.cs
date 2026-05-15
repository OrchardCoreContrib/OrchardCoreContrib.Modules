using Fluid;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCoreContrib.DisplayManagement.Liquid;

namespace OrchardCoreContrib.Liquid;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        // Override the "Environment" value from OrchardCore.DisplayManagement.Liquid
        services.Configure<TemplateOptions>(options =>
            options.Scope.Delete(Constants.FluidValueNames.Environment));

        services.AddLiquidDisplayManagement();
    }
}
