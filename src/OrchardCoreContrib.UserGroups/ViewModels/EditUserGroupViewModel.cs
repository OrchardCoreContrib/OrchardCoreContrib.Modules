using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCoreContrib.UserGroups.Models;

namespace OrchardCoreContrib.UserGroups.ViewModels;

public class EditUserGroupViewModel
{
    public string Name { get; set; }

    public string Description { get; set; }

    [BindNever]
    public UserGroup UserGroup { get; set; }
}
