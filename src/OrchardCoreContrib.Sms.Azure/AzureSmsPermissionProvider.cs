using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Sms.Azure;

public class AzureSmsPermissionProvider : IPermissionProvider
{
    private static readonly IEnumerable<Permission> _permissions = [AzureSmsPermissions.ManageAzureSmsSettings];

    public Task<IEnumerable<Permission>> GetPermissionsAsync() => Task.FromResult(_permissions);

    public IEnumerable<PermissionStereotype> GetDefaultStereotypes() =>
    [
        new PermissionStereotype
        {
            Name = "Administrator",
            Permissions = _permissions,
        }
    ];
}
