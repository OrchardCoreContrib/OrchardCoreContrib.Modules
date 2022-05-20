using Microsoft.AspNetCore.Mvc;
using OrchardCore.Localization;
using OrchardCore.Modules;
using OrchardCoreContrib.ContentLocalization.ViewModels;
using System.Threading.Tasks;

namespace OrchardCoreContrib.ContentLocalization.Controllers
{
    [Feature("OrchardCoreContrib.ContentLocalization.LocalizationMatrix")]
    public class AdminController : Controller
    {
        private readonly IContentLocalizationManager _contentLocalizationManager;
        private readonly ILocalizationService _localizationService;

        public AdminController(IContentLocalizationManager contentLocalizationManager, ILocalizationService localizationService)
        {
            _contentLocalizationManager = contentLocalizationManager;
            _localizationService = localizationService;
        }

        public async Task<IActionResult> LocalizationMatrix()
        {
            var supportedCultures = await _localizationService.GetSupportedCulturesAsync();

            var localizationSets = await _contentLocalizationManager.GetSetsAsync();

            return View(new LocalizationMatrixViewModel
            {
                Cultures = supportedCultures,
                LocalizationSets = localizationSets
            });
        }
    }
}
