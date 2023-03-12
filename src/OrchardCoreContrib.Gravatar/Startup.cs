using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Liquid;
using OrchardCore.Modules;
using OrchardCoreContrib.Gravatar.Liquid;
using OrchardCoreContrib.Gravatar.Services;
using OrchardCoreContrib.Gravatar.TagHelpers;

namespace OrchardCoreContrib.Gravatar;

/// <summary>
/// Represents an entry point to register the user avatar required services.
/// </summary>
public class Startup : StartupBase
{
    private readonly IShellConfiguration _shellConfiguration;

    public Startup(IShellConfiguration shellConfiguration)
    {
        _shellConfiguration = shellConfiguration;
    }

    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IGravatarService, GravatarService>();

        services.AddTagHelpers<GravatarTagHelper>();

        services.AddLiquidFilter<GravatarFilter>("to_gravatar");

        services.Configure<GravatarOptions>(_shellConfiguration.GetSection("OrchardCoreContrib_Gravatar"));
    }
}
