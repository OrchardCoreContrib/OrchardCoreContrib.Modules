using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCoreContrib.IssueTracker.Models;
using OrchardCoreContrib.IssueTracker.Settings;

namespace OrchardCoreContrib.IssueTracker.ViewModels;
public class IssuePartViewModel
{
    public string MySetting { get; set; }

    public bool Show { get; set; }

    [BindNever]
    public ContentItem ContentItem { get; set; }

    [BindNever]
    public IssuePart IssuePart { get; set; }

    [BindNever]
    public IssuePartSettings Settings { get; set; }
}
