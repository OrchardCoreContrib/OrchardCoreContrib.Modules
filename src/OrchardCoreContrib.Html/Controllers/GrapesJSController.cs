using Microsoft.AspNetCore.Mvc;

namespace OrchardCoreContrib.Html.Controllers
{
    public class GrapesJSController : Controller
    {
        [HttpGet]
        public ActionResult Index() => View();
        
    }
}
