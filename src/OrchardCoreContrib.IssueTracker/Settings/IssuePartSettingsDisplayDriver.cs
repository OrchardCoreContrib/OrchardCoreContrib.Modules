using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCoreContrib.IssueTracker.Models;
using System;
using System.Threading.Tasks;

namespace OrchardCoreContrib.IssueTracker.Settings;
public class IssuePartSettingsDisplayDriver : ContentTypePartDefinitionDisplayDriver
{
    public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition, IUpdateModel updater)
    {
        if(!String.Equals(nameof(IssuePart), contentTypePartDefinition.PartDefinition.Name))
        {
            return null;
        }

        return Initialize<IssuePartSettingsViewModel>("IssuePartSettings_Edit", model =>
        {
            var settings = contentTypePartDefinition.GetSettings<IssuePartSettings>();

            model.MySetting = settings.MySetting;
            model.IssuePartSettings = settings;
        }).Location("Content");
    }

    public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
    {
        if(!String.Equals(nameof(IssuePart), contentTypePartDefinition.PartDefinition.Name))
        {
            return null;
        }

        var model = new IssuePartSettingsViewModel();

        if(await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.MySetting))
        {
            context.Builder.WithSettings(new IssuePartSettings { MySetting = model.MySetting });
        }

        return Edit(contentTypePartDefinition, context.Updater);
    }
}
