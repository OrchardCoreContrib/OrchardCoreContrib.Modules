using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement.Display;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.Modules;
using OrchardCoreContrib.Contents.Services;

namespace OrchardCoreContrib.Contents.Controllers;

[Feature("OrchardCoreContrib.Contents.ShareDraftContent")]
public class SharedDraftController(
    ISharedDraftLinkService linkService,
    IContentItemDisplayManager contentItemDisplayManager,
    IUpdateModelAccessor updateModelAccessor) : Controller
{
    [HttpGet("share/{token}")]
    public async Task<IActionResult> Preview(string token)
    {
        var contentItem = await linkService.GetDraftContentAsync(token);

        if (contentItem is null)
        {
            return NotFound("This link is invalid or expired.");
        }

        var shape = await contentItemDisplayManager.BuildDisplayAsync(contentItem, updateModelAccessor.ModelUpdater, "Detail");

        return View(shape);
    }
}

