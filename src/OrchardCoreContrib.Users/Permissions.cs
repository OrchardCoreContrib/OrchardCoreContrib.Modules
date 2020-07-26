using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrchardCore.Modules;
using OrchardCore.Security;
using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Users
{
    /// <summary>
    /// Represents a permissions that will be applied into users module.
    /// </summary>
    [Feature("OrchardCore.Users.Impersonation")]
    public class Permissions : IPermissionProvider
    {
        /// <summary>
        /// Gets a permission for managing a impersonation settings.
        /// </summary>
        public static readonly Permission ManageImpersonationSettings = new Permission("ManageImpersonationSettings", "Manage Impersonation Settings", isSecurityCritical: true);

        /// <inheritdoc/>
        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                ManageImpersonationSettings
            }
            .AsEnumerable());
        }

        /// <inheritdoc/>
        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[] { ManageImpersonationSettings, StandardPermissions.SiteOwner }
                },
            };
        }
    }
}
