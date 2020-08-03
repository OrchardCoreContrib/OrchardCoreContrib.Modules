using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Shell;
using OrchardCore.Settings;

namespace OrchardCoreContrib.Gdpr.Drivers
{
    /// <summary>
    /// Represents a display driver for <see cref="GdprSettings"/>.
    /// </summary>
    public class GdprSettingsDisplayDriver : SectionDisplayDriver<ISite, GdprSettings>
    {
        public const string GroupId = "gdpr";

        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IShellHost _shellHost;
        private readonly ShellSettings _shellSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;

        /// <summary>
        /// Initializes a new instance of <see cref="GdprSettingsDisplayDriver"/>.
        /// </summary>
        /// <param name="dataProtectionProvider">The <see cref="IDataProtectionProvider"/>.</param>
        /// <param name="shellHost">The <see cref="IShellHost"/>.</param>
        /// <param name="shellSettings">The <see cref="ShellSettings"/>.</param>
        /// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
        /// <param name="authorizationService">The <see cref="IAuthorizationService"/>.</param>
        public GdprSettingsDisplayDriver(
            IDataProtectionProvider dataProtectionProvider,
            IShellHost shellHost,
            ShellSettings shellSettings,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService)
        {
            _dataProtectionProvider = dataProtectionProvider;
            _shellHost = shellHost;
            _shellSettings = shellSettings;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
        }

        /// <inheritdoc/>
        public override async Task<IDisplayResult> EditAsync(GdprSettings section, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageGdprSettings))
            {
                return null;
            }

            var shapes = new List<IDisplayResult>
            {
                Initialize<GdprSettings>("GdprSettings_Edit", model =>
                {
                    model.Summary = section.Summary;
                    model.Detail = section.Detail;
                }).Location("Content:5").OnGroup(GroupId)
            };

            return Combine(shapes);
        }

        /// <inheritdoc/>
        public override async Task<IDisplayResult> UpdateAsync(GdprSettings section, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageGdprSettings))
            {
                return null;
            }

            if (context.GroupId == GroupId)
            {
                await context.Updater.TryUpdateModelAsync(section, Prefix);
                await _shellHost.ReleaseShellContextAsync(_shellSettings);
            }

            return await EditAsync(section, context);
        }
    }
}
