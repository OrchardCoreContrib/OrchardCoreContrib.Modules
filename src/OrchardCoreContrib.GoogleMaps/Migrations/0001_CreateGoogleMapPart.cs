using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCoreContrib.Data.Migrations;

namespace OrchardCoreContrib.GoogleMaps.Migrations;

[Migration(1)]
public class CreateGoogleMapPart(IContentDefinitionManager contentDefinitionManager) : Migration
{
    public override void Up()
    {
        contentDefinitionManager.AlterPartDefinitionAsync("GoogleMapPart", builder => builder
            .Attachable()
            .WithDescription("Provides a Google Map that you can use for your content item."));
    }

    public override void Down()
    {
        contentDefinitionManager.DeletePartDefinitionAsync("GoogleMapPart").GetAwaiter().GetResult();
    }
}
