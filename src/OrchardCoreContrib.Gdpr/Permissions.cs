using OrchardCore.Security.Permissions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Gdpr
{
    /// <summary>
    /// Represents a permissions that will be applied into GDPR module.
    /// </summary>
    public class Permissions : IPermissionProvider
    {
        /// <summary>
        /// Gets a permission for managing a GDPR settings.
        /// </summary>
        public static readonly Permission ManageGdprSettings = new Permission("ManageGdprSettings", "Manage GDPR Settings");

        /// <inheritdoc/>
        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                ManageGdprSettings
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
                    Permissions = new[] { ManageGdprSettings }
                },
            };
        }
    }
}
