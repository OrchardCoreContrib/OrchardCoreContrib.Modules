using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Modules;
using OrchardCore.Settings;
using OrchardCore.Users;
using OrchardCoreContrib.Users.Models;

namespace OrchardCoreContrib.Users.Drivers
{
    /// <summary>
    /// Represents a display driver for the <see cref="ImpersonationSettings"/>.
    /// </summary>
    [Feature("OrchardCore.Users.Impersonation")]
    public class ImpersonationSettingsDisplayDriver : SectionDisplayDriver<ISite, ImpersonationSettings>
    {
        public const string GroupId = "ImpersonationSettings";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;

        /// <summary>
        /// Initializes a new instance of <see cref="ImpersonationSettingsDisplayDriver"/>.
        /// </summary>
        /// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
        /// <param name="authorizationService">The <see cref="IAuthorizationService"/>.</param>
        public ImpersonationSettingsDisplayDriver(
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
        }

        /// <inheritdoc/>
        public override async Task<IDisplayResult> EditAsync(ImpersonationSettings settings, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageUsers))
            {
                return null;
            }

            return Initialize<ImpersonationSettings>("ImpersonationSettings_Edit", model =>
            {
                model.EnableImpersonation = settings.EnableImpersonation;
                model.EndImpersonation = settings.EndImpersonation;
            }).Location("Content:5").OnGroup(GroupId);
        }

        /// <inheritdoc/>
        public override async Task<IDisplayResult> UpdateAsync(ImpersonationSettings settings, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageUsers))
            {
                return null;
            }

            if (context.GroupId == GroupId)
            {
                await context.Updater.TryUpdateModelAsync(settings, Prefix);
            }

            return await EditAsync(settings, context);
        }
    }
}
