using Microsoft.AspNetCore.Mvc;

namespace OrchardCoreContrib.Gdpr.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Privacy() => View();
    }
}
