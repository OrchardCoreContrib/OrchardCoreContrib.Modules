using Microsoft.AspNetCore.Mvc;
using OrchardCore.Modules;

namespace OrchardCoreContrib.ContentPreview.Controllers
{
    [Feature("OrchardCoreContrib.ContentPreview.PagePreviewBar")]
    public class PreviewController : Controller
    {
        public IActionResult Index() => View();
    }
}
