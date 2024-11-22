using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Shell;
using OrchardCore.Settings;
using OrchardCoreContrib.Email.SendGrid.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Email.SendGrid.Drivers
{
    /// <summary>
    /// Represents a display driver for <see cref="SendGridSettings"/>.
    /// </summary>
    public class SendGridSettingsDisplayDriver : SectionDisplayDriver<ISite, SendGridSettings>
    {
        public const string GroupId = "sendgrid";

        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IShellHost _shellHost;
        private readonly ShellSettings _shellSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;

        /// <summary>
        /// Initializes a new instance of <see cref="SendGridSettingsDisplayDriver"/>.
        /// </summary>
        /// <param name="dataProtectionProvider">The <see cref="IDataProtectionProvider"/>.</param>
        /// <param name="shellHost">The <see cref="IShellHost"/>.</param>
        /// <param name="shellSettings">The <see cref="ShellSettings"/>.</param>
        /// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
        /// <param name="authorizationService">The <see cref="IAuthorizationService"/>.</param>
        public SendGridSettingsDisplayDriver(
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
        public override async Task<IDisplayResult> EditAsync(ISite model, SendGridSettings section, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageSendGridSettings))
            {
                return null;
            }

            var shapes = new List<IDisplayResult>
            {
                Initialize<SendGridSettings>("SendGridSettings_Edit", model =>
                {
                    model.DefaultSender = section.DefaultSender;
                    model.ApiKey = section.ApiKey;
                }).Location("Content:5").OnGroup(GroupId)
            };

            if (section?.DefaultSender != null)
            {
                shapes.Add(Dynamic("SendGridSettings_TestButton").Location("Actions").OnGroup(GroupId));
            }

            return Combine(shapes);
        }

        /// <inheritdoc/>
        public override async Task<IDisplayResult> UpdateAsync(ISite model, SendGridSettings section, UpdateEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageSendGridSettings))
            {
                return null;
            }

            if (context.GroupId == GroupId)
            {
                var previousApiKey = section.ApiKey;
                await context.Updater.TryUpdateModelAsync(section, Prefix);

                if (string.IsNullOrWhiteSpace(section.ApiKey))
                {
                    section.ApiKey = previousApiKey;
                }
                else
                {
                    var protector = _dataProtectionProvider.CreateProtector(nameof(SendGridSettingsConfiguration));
                    section.ApiKey = protector.Protect(section.ApiKey);
                }

                await _shellHost.ReleaseShellContextAsync(_shellSettings);
            }

            return await EditAsync(model, section, context);
        }
    }
}
