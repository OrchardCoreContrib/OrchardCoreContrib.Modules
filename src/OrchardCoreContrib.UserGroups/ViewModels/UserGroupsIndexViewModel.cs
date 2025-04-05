using OrchardCoreContrib.UserGroups.Models;

namespace OrchardCoreContrib.UserGroups.ViewModels;

public class UserGroupsIndexViewModel
{
    public List<UserGroupEntry> UserGroupEntries { get; set; } = [];

    public ContentOptions Options { get; set; } = new ContentOptions();

    public dynamic Pager { get; set; }
}

public class UserGroupEntry
{
    public string Name { get; set; }

    public UserGroup UserGroup { get; set; }

    public bool Selected { get; set; }
}

public class ContentOptions
{
    public string Search { get; set; }
    
    public BulkAction BulkAction { get; set; }
}

public enum BulkAction
{
    None,
    Remove
}
