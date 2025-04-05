using System.ComponentModel.DataAnnotations;

namespace OrchardCoreContrib.UserGroups.ViewModels;

public class CreateUserGroupViewModel
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }
}
