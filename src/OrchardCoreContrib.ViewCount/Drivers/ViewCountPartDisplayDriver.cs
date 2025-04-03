using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using OrchardCoreContrib.ViewCount.Models;
using OrchardCoreContrib.ViewCount.Services;
using OrchardCoreContrib.ViewCount.ViewModels;

namespace OrchardCoreContrib.ViewCount.Drivers;

public sealed class ViewCountPartDisplayDriver(IViewCountService viewCountService) : ContentPartDisplayDriver<ViewCountPart>
{
    public override IDisplayResult Display(ViewCountPart part, BuildPartDisplayContext context) => Initialize<ViewCountPartViewModel>(GetDisplayShapeType(context),
        async model =>
        {
            await viewCountService.ViewAsync(part.ContentItem);

            model.Count = part.Count;
        }).Location("Detail", "Content:10");
}
