using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Settings;
using OrchardCoreContrib.GoogleMaps.ViewModels;

namespace OrchardCoreContrib.GoogleMaps.Drivers;

/// <summary>
/// Represents a display driver for <see cref="GoogleMapsSettings"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="GoogleMapsSettingsDisplayDriver"/>.
/// </remarks>
/// <param name="dataProtectionProvider">The <see cref="IDataProtectionProvider"/>.</param>
/// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
/// <param name="authorizationService">The <see cref="IAuthorizationService"/>.</param>
public class GoogleMapsSettingsDisplayDriver(
    IDataProtectionProvider dataProtectionProvider,
    IHttpContextAccessor httpContextAccessor,
    IAuthorizationService authorizationService) : SectionDisplayDriver<ISite, GoogleMapsSettings>
{
    public const string GroupId = "google-maps";

    /// <inheritdoc/>
    public override async Task<IDisplayResult> EditAsync(ISite model, GoogleMapsSettings section, BuildEditorContext context)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (!await authorizationService.AuthorizeAsync(user, GoogleMapsPermissions.ManageGoogleMapsSettings))
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
    public override async Task<IDisplayResult> UpdateAsync(ISite model, GoogleMapsSettings section, UpdateEditorContext context)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (!await authorizationService.AuthorizeAsync(user, GoogleMapsPermissions.ManageGoogleMapsSettings))
        {
            return null;
        }

        if (context.GroupId == GroupId)
        {
            var viewModel = new GoogleMapsSettingsViewModel();
            var previousApiKey = section.ApiKey;
            
            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix, m => m.ApiKey, m => m.Latitude, m => m.Longitude))
            {
                section.ApiKey = viewModel.ApiKey;
                section.Latitude = viewModel.Latitude;
                section.Longitude = viewModel.Longitude;
            }

            if (string.IsNullOrWhiteSpace(section.ApiKey))
            {
                section.ApiKey = previousApiKey;
            }
            else
            {
                var protector = dataProtectionProvider.CreateProtector(GroupId);

                section.ApiKey = protector.Protect(section.ApiKey);
            }
        }

        return await EditAsync(model, section, context);
    }
}
