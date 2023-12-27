using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCoreContrib.IssueTracker.Models;
using OrchardCoreContrib.IssueTracker.Settings;
using OrchardCoreContrib.IssueTracker.ViewModels;
using System.Threading.Tasks;

namespace OrchardCoreContrib.IssueTracker.Drivers;
public class IssuePartDisplayDriver : ContentPartDisplayDriver<IssuePart>
{
    private readonly IContentDefinitionManager _contentDefinitionManager;

    public IssuePartDisplayDriver(IContentDefinitionManager contentDefinitionManager)
    {
        _contentDefinitionManager = contentDefinitionManager;
    }

    public override IDisplayResult Display(IssuePart part, BuildPartDisplayContext context)
    {
        return Initialize<IssuePartViewModel>(GetDisplayShapeType(context), m => BuildViewModel(m, part, context))
            .Location("Detail", "Content:10")
            .Location("Summary", "Content:10")
            ;
    }

    public override IDisplayResult Edit(IssuePart part, BuildPartEditorContext context)
    {
        return Initialize<IssuePartViewModel>(GetEditorShapeType(context), model =>
        {
            model.Show = part.Show;
            model.ContentItem = part.ContentItem;
            model.IssuePart = part;
        });
    }

    public override async Task<IDisplayResult> UpdateAsync(IssuePart model, IUpdateModel updater)
    {
        await updater.TryUpdateModelAsync(model, Prefix, t => t.Show);

        return Edit(model);
    }

    private static void BuildViewModel(IssuePartViewModel model, IssuePart part, BuildPartDisplayContext context)
    {
        var settings = context.TypePartDefinition.GetSettings<IssuePartSettings>();

        model.ContentItem = part.ContentItem;
        model.MySetting = settings.MySetting;
        model.Show = part.Show;
        model.IssuePart = part;
        model.Settings = settings;
    }
}
