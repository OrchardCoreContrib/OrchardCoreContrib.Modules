using Fluid;
using Fluid.Values;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrchardCore.Modules;

namespace OrchardCoreContrib.Liquid;
public class Startup : StartupBase
{
    private readonly IHostEnvironment _hostEnvironment;

    public Startup(IHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }
    
    public override void ConfigureServices(IServiceCollection services)
    {
        services.Configure<TemplateOptions>(o =>
        {
            o.Scope.SetValue("Environment", new ObjectValue(new LiquidEnvironmentAccessor()));
            o.MemberAccessStrategy.Register<LiquidEnvironmentAccessor, FluidValue>((obj, name, context) =>
            {
                FluidValue result = name switch
                {
                    "IsDevelopment" => BooleanValue.Create(_hostEnvironment.IsDevelopment()),
                    "IsStaging" => BooleanValue.Create(_hostEnvironment.IsStaging()),
                    "IsProduction" => BooleanValue.Create(_hostEnvironment.IsProduction()),
                    _ => NilValue.Instance
                };

                return result;
            });
        });
    }
}
