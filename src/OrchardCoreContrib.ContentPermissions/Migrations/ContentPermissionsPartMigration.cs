using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;

namespace OrchardCoreContrib.ContentPermissions.Migrations;

public class ContentPermissionsPartMigration(IContentDefinitionManager contentDefinitionManager) : DataMigration
{
    public async Task<int> CreateAsync()
    {
        await contentDefinitionManager.AlterPartDefinitionAsync(Constants.ContentPermissionsPartName, builder => builder
            .WithDisplayName("Content Permissions")
            .WithDescription("Controls the accessibilty for the content item by providing certain permission(s).")
            .Attachable()
        );

        return 1;
    }
}
