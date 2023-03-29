using Fluid;
using Fluid.Values;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrchardCore.Liquid;
using OrchardCore.Modules;

namespace OrchardCoreContrib.Liquid;
public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<TemplateOptions>(o =>
        {
            o.Scope.SetValue("Environment", new ObjectValue(new LiquidEnvironmentAccessor()));
            o.MemberAccessStrategy.Register<LiquidEnvironmentAccessor, FluidValue>((obj, name, context) =>
            {
                var hostEnvironment = ((LiquidTemplateContext)context).Services.GetRequiredService<IHostEnvironment>();

                FluidValue result = name switch
                {
                    "IsDevelopment" => BooleanValue.Create(hostEnvironment.IsDevelopment()),
                    "IsStaging" => BooleanValue.Create(hostEnvironment.IsStaging()),
                    "IsProduction" => BooleanValue.Create(hostEnvironment.IsProduction()),
                    _ => NilValue.Instance
                };

                return result;
            });
        });
    }
}
