using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;

namespace OrchardCoreContrib.UserGroups;

public class Migrations(IContentDefinitionManager contentDefinitionManager) : DataMigration
{
    public async Task<int> CreateAsync()
    {
        await contentDefinitionManager.AlterPartDefinitionAsync("UserGroupsListPart", builder => builder
            .Attachable()
            .WithDescription("Provides a way to add user group(s) for your content item."));

        return 1;
    }
}
