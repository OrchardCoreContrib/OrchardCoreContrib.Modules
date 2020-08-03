using Microsoft.AspNetCore.Mvc;
using OrchardCore.Entities;
using OrchardCore.Settings;

namespace OrchardCoreContrib.Gdpr.Controllers
{
    public class HomeController : Controller
    {
        private readonly GdprSettings _gdprSettings;

        public HomeController(ISiteService site)
        {
            _gdprSettings = site.GetSiteSettingsAsync()
                .GetAwaiter().GetResult()
                .As<GdprSettings>();
        }

        [HttpGet]
        public IActionResult Privacy() => View(_gdprSettings);
    }
}
