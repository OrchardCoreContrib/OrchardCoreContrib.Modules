using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCoreContrib.Gravatar.TagHelpers;

namespace OrchardCoreContrib.Gravatar;

/// <summary>
/// Represents an entry point to register the user avatar required services.
/// </summary>
public class Startup : StartupBase
{
    /// <inheritdoc/>
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTagHelpers<GravatarTagHelper>();
    }
}
