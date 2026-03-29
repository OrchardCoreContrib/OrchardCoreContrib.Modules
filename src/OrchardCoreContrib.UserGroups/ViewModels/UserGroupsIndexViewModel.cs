using OrchardCoreContrib.UserGroups.Models;

namespace OrchardCoreContrib.UserGroups.ViewModels;

public class UserGroupsIndexViewModel
{
    public IEnumerable<UserGroup> UserGroups { get; set; } = [];

    public string Search { get; set; }

    public dynamic Pager { get; set; }
}
