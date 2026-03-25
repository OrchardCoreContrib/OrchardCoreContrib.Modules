using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;

namespace OrchardCoreContrib.Contents.ViewModels;

public class ShareDraftViewModel
{
    [BindNever]
    public ContentItem ContentItem { get; set; }
}
