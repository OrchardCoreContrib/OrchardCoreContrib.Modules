using Microsoft.AspNetCore.Mvc;

namespace OrchardCoreContrib.ContentPreview.Controllers
{
    public class PreviewController : Controller
    {
        public IActionResult Index() => View();
    }
}
