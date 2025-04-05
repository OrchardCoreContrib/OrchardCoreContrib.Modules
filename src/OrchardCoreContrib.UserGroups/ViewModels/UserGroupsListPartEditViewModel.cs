using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace OrchardCoreContrib.UserGroups.ViewModels;

public class UserGroupsListPartEditViewModel
{
    [BindNever]
    public IEnumerable<string> UserGroups { get; set; }

    public string[] SelectedUserGroups { get; set; }
}
