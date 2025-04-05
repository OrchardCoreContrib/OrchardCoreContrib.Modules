using OrchardCore.Data.Documents;

namespace OrchardCoreContrib.UserGroups.Models;

public class UserGroupDocument : Document
{
    //public List<UserGroup> UserGroups { get; set; } = [];
    
    public Dictionary<string, UserGroup> UserGroups { get; init; } = new(StringComparer.OrdinalIgnoreCase);
}
