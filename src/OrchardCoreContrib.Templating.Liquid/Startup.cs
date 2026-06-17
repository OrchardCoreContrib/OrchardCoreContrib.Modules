using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;

namespace OrchardCoreContrib.Templating.Liquid;

public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services) => services.AddLiquidTemplating();
}
