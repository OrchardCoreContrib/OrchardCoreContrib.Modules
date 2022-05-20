using OrchardCore.Security.Permissions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrchardCoreContrib.GoogleMaps
{
    /// <summary>
    /// Represents a permissions that will be applied into GoogleMaps module.
    /// </summary>
    public class Permissions : IPermissionProvider
    {
        /// <summary>
        /// Gets a permission for managing a Gmail settings.
        /// </summary>
        public static readonly Permission ManageGoogleMapsSettings = new Permission("ManageGoogleMapsSettings", "Manage Google Maps Settings");

        /// <inheritdoc/>
        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                ManageGoogleMapsSettings
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
                    Permissions = new[] { ManageGoogleMapsSettings }
                },
            };
        }
    }
}
