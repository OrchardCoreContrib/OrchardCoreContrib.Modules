using Microsoft.AspNetCore.Mvc;
using OrchardCore.Modules;

namespace OrchardCoreContrib.Html.Controllers
{
    [Feature("OrchardCoreContrib.Html.GrapesJS")]
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Index() => View();
    }
}
