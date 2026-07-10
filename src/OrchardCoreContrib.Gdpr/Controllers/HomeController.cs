using Microsoft.AspNetCore.Mvc;
using OrchardCore.Settings;

namespace OrchardCoreContrib.Gdpr.Controllers;

public class HomeController(ISiteService site) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Privacy()
    {
        var siteSettings = await site.GetSiteSettingsAsync();

        siteSettings.TryGet<GdprSettings>(out var gdprSettings);

        return View(gdprSettings);
    }
}
