using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace OrchardCoreContrib.Html;

/// <summary>
/// Configure the resources that will be used in the module.
/// </summary>
public class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
{
    private static readonly ResourceManifest _manifest;

    static ResourceManagementOptionsConfiguration()
    {
        _manifest = new ResourceManifest();

        _manifest
            .DefineScript("grapes")
            .SetUrl("~/OrchardCoreContrib.Html/Scripts/grapes.min.js")
            .SetVersion("1.0.0");

        _manifest
            .DefineScript("grapes-preset")
            .SetUrl("~/OrchardCoreContrib.Html/Scripts/grapesjs-preset-webpage.min.js")
            .SetVersion("1.0.0");

        _manifest
            .DefineScript("grapes-editor")
            .SetUrl("~/OrchardCoreContrib.Html/Scripts/grapes-editor.js")
            .SetVersion("1.0.0");

        _manifest
            .DefineStyle("grapes")
            .SetUrl("~/OrchardCoreContrib.Html/Styles/grapes.min.css")
            .SetVersion("1.0.0");

        _manifest
            .DefineStyle("grapes-preset")
            .SetUrl("~/OrchardCoreContrib.Html/Styles/grapesjs-preset-webpage.min.css")
            .SetVersion("1.0.0");

        _manifest
            .DefineStyle("grapes-editor")
            .SetUrl("~/OrchardCoreContrib.Html/Styles/grapes-editor.css")
            .SetVersion("1.0.0");
    }

    /// <inheritdoc/>
    public void Configure(ResourceManagementOptions options)
    {
        options.ResourceManifests.Add(_manifest);
    }
}
