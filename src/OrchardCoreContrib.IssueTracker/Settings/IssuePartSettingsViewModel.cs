using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OrchardCoreContrib.IssueTracker.Settings;
public class IssuePartSettingsViewModel
{
    public string MySetting { get; set; }

    [BindNever]
    public IssuePartSettings IssuePartSettings { get; set; }
}
