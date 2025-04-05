using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCoreContrib.UserGroups.Models;

namespace OrchardCoreContrib.UserGroups.ViewModels;

public class UserGroupsListPartEditViewModel
{
    [BindNever]
    public IEnumerable<UserGroup> UserGroups { get; set; }

    public string[] SelectedUserGroups { get; set; }
}
