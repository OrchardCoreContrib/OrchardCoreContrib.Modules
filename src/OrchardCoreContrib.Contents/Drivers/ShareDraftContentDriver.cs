using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCoreContrib.Contents.ViewModels;

namespace OrchardCoreContrib.Contents.Drivers;

public sealed class ShareDraftContentDriver : ContentDisplayDriver
{
    public override async Task<IDisplayResult> EditAsync(ContentItem contentItem, BuildEditorContext context)
        => Initialize<ShareDraftViewModel>("Content_ShareDraftButton", model => model.ContentItem = contentItem)
            .Location("Actions:30");
}
