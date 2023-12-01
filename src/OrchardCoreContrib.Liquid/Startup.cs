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
        var environment = new Environment
        {
            IsDevelopment = BooleanValue.Create(_hostEnvironment.IsDevelopment()),
            IsStaging = BooleanValue.Create(_hostEnvironment.IsStaging()),
            IsProduction = BooleanValue.Create(_hostEnvironment.IsProduction())
        };

        services.Configure<TemplateOptions>(options =>
        {
            options.Scope.SetValue("Environment", new ObjectValue(environment));

            options.MemberAccessStrategy.Register<Environment>();
        });
    }
}
