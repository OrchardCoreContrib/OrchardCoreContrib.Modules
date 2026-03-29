using Microsoft.AspNetCore.Mvc;
using OrchardCore.Settings;

namespace OrchardCoreContrib.Gdpr.Controllers;

public class HomeController(ISiteService site) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Privacy()
    {
        var gdprSettings = (await site.GetSiteSettingsAsync()).As<GdprSettings>();

        return View(gdprSettings);
    }
}
