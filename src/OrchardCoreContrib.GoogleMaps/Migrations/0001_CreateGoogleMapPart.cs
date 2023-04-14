using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCoreContrib.Data.Migrations;

namespace OrchardCoreContrib.GoogleMaps.Migrations;

[Migration(1)]
public class CreateGoogleMapPart : Migration
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public CreateGoogleMapPart(IContentDefinitionManager contentDefinitionManager)
    {
        _contentDefinitionManager = contentDefinitionManager;
    }

    public override void Up()
    {
        _contentDefinitionManager.AlterPartDefinition("GoogleMapPart", builder => builder
            .Attachable()
            .WithDescription("Provides a Google Map that you can use for your content item."));
    }

    public override void Down()
    {
        _contentDefinitionManager.DeletePartDefinition("GoogleMapPart");
    }
}
