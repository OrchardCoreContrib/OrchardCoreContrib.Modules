using Microsoft.AspNetCore.Mvc;
using OrchardCore.Localization;
using OrchardCore.Modules;
using OrchardCoreContrib.ContentLocalization.ViewModels;

namespace OrchardCoreContrib.ContentLocalization.Controllers;

[Feature("OrchardCoreContrib.ContentLocalization.LocalizationMatrix")]
public class AdminController(
    IContentLocalizationManager contentLocalizationManager,
    ILocalizationService localizationService) : Controller
{
    public async Task<IActionResult> LocalizationMatrix()
    {
        var supportedCultures = await localizationService.GetSupportedCulturesAsync();

        var localizationSets = await contentLocalizationManager.GetSetsAsync();

        return View(new LocalizationMatrixViewModel
        {
            Cultures = supportedCultures,
            LocalizationSets = localizationSets
        });
    }
}
