using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace OrchardCoreContrib.ContentPreview;

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
            .DefineScript("page-preview-bar")
            .SetUrl("~/OrchardCoreContrib.ContentPreview/Scripts/PagePreviewBar.js")
            .SetVersion("1.0.0");

        _manifest
            .DefineStyle("page-preview-bar")
            .SetUrl("~/OrchardCoreContrib.ContentPreview/Styles/PagePreviewBar.css")
            .SetVersion("1.0.0");
    }

    /// <inheritdoc/>
    public void Configure(ResourceManagementOptions options)
    {
        options.ResourceManifests.Add(_manifest);
    }
}
