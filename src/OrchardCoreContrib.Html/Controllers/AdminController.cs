using Microsoft.AspNetCore.Mvc;

namespace OrchardCoreContrib.Html.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Index() => View();
    }
}
