using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Email.SendGrid
{
    /// <summary>
    /// Represents a permissions that will be applied into SendGrid mailing module.
    /// </summary>
    public class Permissions : IPermissionProvider
    {
        /// <summary>
        /// Gets a permission for managing a SendGrid settings.
        /// </summary>
        public static readonly Permission ManageSendGridSettings = new Permission("ManageSendGridSettings", "Manage SendGrid Settings");

        /// <inheritdoc/>
        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[]
            {
                ManageSendGridSettings
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
                    Permissions = new[] { ManageSendGridSettings }
                },
            };
        }
    }
}
