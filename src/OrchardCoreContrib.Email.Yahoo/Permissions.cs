using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Email.Yahoo
{
    /// <summary>
    /// Represents a permissions that will be applied into Yahoo mailing module.
    /// </summary>
    public class Permissions : IPermissionProvider
    {
        /// <summary>
        /// Gets a permission for managing a Yahoo settings.
        /// </summary>
        public static readonly Permission ManageYahooSettings = new Permission("ManageYahooSettings", "Manage Yahoo Settings");

        /// <inheritdoc/>
        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                ManageYahooSettings
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
                    Permissions = new[] { ManageYahooSettings }
                },
            };
        }
    }
}
