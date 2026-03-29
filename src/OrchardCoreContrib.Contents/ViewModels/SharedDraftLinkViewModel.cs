using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCoreContrib.Contents.Models;

namespace OrchardCoreContrib.Contents.ViewModels;

public class SharedDraftLinkViewModel : ShapeViewModel
{
    [BindNever]
    public SharedDraftLink Link { get; set; }

    [BindNever]
    public string GeneratedLink { get; set; }
}
