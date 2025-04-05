using OrchardCore.ContentManagement;

namespace OrchardCoreContrib.UserGroups.Models;

public class UserGroupsListPart : ContentPart
{
    public string[] UserGroups { get; set; } = [];
}
