using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCoreContrib.ContentPermissions.Models;
using OrchardCoreContrib.ContentPermissions.ViewModels;

namespace OrchardCoreContrib.ContentPermissions.Drivers;

public class ContentPermissionsPartSettingsDisplayDriver : ContentTypePartDefinitionDisplayDriver<ContentPermissionsPart>
{
    public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition, BuildEditorContext context)
        => Initialize<ContentPermissionsPartSettingsViewModel>("ContentPermissionsPartSettings_Edit", model =>
        {
            var settings = contentTypePartDefinition.GetSettings<ContentPermissionsPartSettings>();

            model.EnableRoles = settings.EnableRoles;
            model.EnableUsers = settings.EnableUsers;
        }).Location("Content");

    public async override Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
    {
        var model = new ContentPermissionsPartSettingsViewModel();

        await context.Updater.TryUpdateModelAsync(model, Prefix);

        context.Builder.WithSettings(new ContentPermissionsPartSettings
        {
            EnableRoles = model.EnableRoles,
            EnableUsers = model.EnableUsers
        });

        return Edit(contentTypePartDefinition, context);
    }
}
