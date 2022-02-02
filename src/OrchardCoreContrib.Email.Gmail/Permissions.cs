using OrchardCore.Security.Permissions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Email.Gmail
{
    /// <summary>
    /// Represents a permissions that will be applied into Gmail mailing module.
    /// </summary>
    public class Permissions : IPermissionProvider
    {
        /// <summary>
        /// Gets a permission for managing a Gmail settings.
        /// </summary>
        public static readonly Permission ManageGmailSettings = new Permission("ManageGmailSettings", "Manage Gmail Settings");

        /// <inheritdoc/>
        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                ManageGmailSettings
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
                    Permissions = new[] { ManageGmailSettings }
                },
            };
        }
    }
}
