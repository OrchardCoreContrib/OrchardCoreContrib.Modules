using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Email.Hotmail
{
    /// <summary>
    /// Represents a permissions that will be applied into Html mailing module.
    /// </summary>
    public class Permissions : IPermissionProvider
    {
        /// <summary>
        /// Gets a permission for managing a Hotmail settings.
        /// </summary>
        public static readonly Permission ManageHotmailSettings = new Permission("ManageHotmailSettings", "Manage Hotmail Settings");

        /// <inheritdoc/>
        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                ManageHotmailSettings
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
                    Permissions = new[] { ManageHotmailSettings }
                },
            };
        }
    }
}
