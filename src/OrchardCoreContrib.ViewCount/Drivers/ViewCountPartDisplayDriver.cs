using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.Views;
using OrchardCoreContrib.ViewCount.Models;
using OrchardCoreContrib.ViewCount.Services;
using OrchardCoreContrib.ViewCount.ViewModels;

namespace OrchardCoreContrib.ViewCount.Drivers;

public sealed class ViewCountPartDisplayDriver(IViewCountService viewCountService) : ContentPartDisplayDriver<ViewCountPart>
{
    public async override Task<IDisplayResult> DisplayAsync(ViewCountPart part, BuildPartDisplayContext context)
    {
        if (context.DisplayType == "Detail")
        {
            await viewCountService.ViewAsync(part.ContentItem);
        }

        return Initialize<ViewCountPartViewModel>(GetDisplayShapeType(context), model => model.Count = part.Count)
            .Location("Detail", "Content:10");
    }
}
