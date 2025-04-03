using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;

namespace OrchardCoreContrib.ViewCount;

public class Migrations(IContentDefinitionManager contentDefinitionManager) : DataMigration
{
    public async Task<int> CreateAsync()
    {
        await contentDefinitionManager.AlterPartDefinitionAsync("ViewCountPart", builder => builder
            .Attachable()
            .WithDescription("Allow to count the view number of your content item."));

        return 1;
    }
}
