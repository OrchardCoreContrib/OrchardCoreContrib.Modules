using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OrchardCoreContrib.ContentPermissions.ViewModels;

public class ContentPermissionsPartViewModel
{
    [BindNever]
    public IEnumerable<string> Roles { get; set; }

    public string[] SelectedRoles { get; set; }

    [BindNever]
    public IEnumerable<string> Users { get; set; }

    public string[] SelectedUsers { get; set; }

    public bool EnableRoles => Roles.Any();

    public bool EnableUsers => Users.Any();
}
