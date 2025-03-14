using OrchardCore.ContentManagement;

namespace OrchardCoreContrib.ContentPermissions.Models;

public class ContentPermissionsPart : ContentPart
{
    public string[] Roles { get; set; } = [];

    public string[] Users { get; set; } = [];
}
