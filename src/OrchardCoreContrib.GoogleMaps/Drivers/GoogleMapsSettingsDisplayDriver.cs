using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;
using OrchardCoreContrib.GoogleMaps.ViewModels;
using System.Threading.Tasks;

namespace OrchardCoreContrib.GoogleMaps.Drivers
{
    /// <summary>
    /// Represents a display driver for <see cref="GoogleMapsSettings"/>.
    /// </summary>
    public class GoogleMapsSettingsDisplayDriver : SectionDisplayDriver<ISite, GoogleMapsSettings>
    {
        public const string GroupId = "google-maps";

        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;

        /// <summary>
        /// Initializes a new instance of <see cref="GoogleMapsSettingsDisplayDriver"/>.
        /// </summary>
        /// <param name="dataProtectionProvider">The <see cref="IDataProtectionProvider"/>.</param>
        /// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
        /// <param name="authorizationService">The <see cref="IAuthorizationService"/>.</param>
        public GoogleMapsSettingsDisplayDriver(
            IDataProtectionProvider dataProtectionProvider,
            IHttpContextAccessor httpContextAccessor,
            IAuthorizationService authorizationService)
        {
            _dataProtectionProvider = dataProtectionProvider;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
        }

        /// <inheritdoc/>
        public override async Task<IDisplayResult> EditAsync(GoogleMapsSettings section, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageGoogleMapsSettings))
            {
                return null;
            }

            return Initialize<GoogleMapsSettingsViewModel>("GoogleMapsSettings_Edit", model =>
            {
                model.ApiKey = section.ApiKey;
                model.Latitude = section.Latitude;
                model.Longitude = section.Longitude;
            }).Location("Content").OnGroup(GroupId);
        }

        /// <inheritdoc/>
        public override async Task<IDisplayResult> UpdateAsync(GoogleMapsSettings section, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authorizationService.AuthorizeAsync(user, Permissions.ManageGoogleMapsSettings))
            {
                return null;
            }

            if (context.GroupId == GroupId)
            {
                var model = new GoogleMapsSettingsViewModel();
                var previousApiKey = section.ApiKey;
                
                if (await context.Updater.TryUpdateModelAsync(model, Prefix, m => m.ApiKey, m => m.Latitude, m => m.Longitude))
                {
                    section.ApiKey = model.ApiKey;
                    section.Latitude = model.Latitude;
                    section.Longitude = model.Longitude;
                }

                if (string.IsNullOrWhiteSpace(section.ApiKey))
                {
                    section.ApiKey = previousApiKey;
                }
                else
                {
                    var protector = _dataProtectionProvider.CreateProtector(GroupId);

                    section.ApiKey = protector.Protect(section.ApiKey);
                }
            }

            return await EditAsync(section, context);
        }
    }
}
