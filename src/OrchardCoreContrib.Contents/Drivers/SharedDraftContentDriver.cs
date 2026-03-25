using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Localization;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.DisplayManagement.Views;
using OrchardCoreContrib.Contents.Services;
using OrchardCoreContrib.Contents.ViewModels;

namespace OrchardCoreContrib.Contents.Drivers;

public sealed class SharedDraftContentDriver(
    IAuthorizationService authorizationService,
    IHttpContextAccessor httpContextAccessor,
    ISharedDraftLinkService linkService,
    INotifier notifier,
    IHtmlLocalizer<SharedDraftContentDriver> H) : ContentDisplayDriver
{
    public override async Task<IDisplayResult> DisplayAsync(ContentItem contentItem, BuildDisplayContext context)
    {
        var user = httpContextAccessor.HttpContext.User;

        if (!await authorizationService.AuthorizeAsync(user, ContentsPermissions.ShareDraftContent))
        {
            return null;
        }

        var model = new SharedDraftLinkViewModel();

        var link = await linkService.GetActiveLinkAsync(contentItem.ContentItemId);

        if (link is not null)
        {
            model.Link = link;
            model.GeneratedLink = linkService.GetGeneratedLink(link.Token);
        }

        return Shape("SharedDraftStatus_SummaryAdmin", model)
            .RenderWhen(() => Task.FromResult(!contentItem.Published))
            .Location("SummaryAdmin", "Meta:15");
    }

    public override async Task<IDisplayResult> EditAsync(ContentItem contentItem, BuildEditorContext context)
    {
        var user = httpContextAccessor.HttpContext.User;

        if (!await authorizationService.AuthorizeAsync(user, ContentsPermissions.ShareDraftContent))
        {
            return null;
        }

        var generatedLink = await linkService.GenerateLinkAsync(contentItem.ContentItemId);

        var link = await linkService.GetActiveLinkAsync(contentItem.ContentItemId);

        return Initialize<SharedDraftLinkViewModel>("Content_ShareDraftButton", model =>
        {
            model.Link = link;
            model.GeneratedLink = generatedLink;
        }).RenderWhen(() => Task.FromResult(!contentItem.Published))
        .Location("Actions:30");
    }

    public override async Task<IDisplayResult> UpdateAsync(ContentItem contentItem, UpdateEditorContext context)
    {
        var user = httpContextAccessor.HttpContext.User;

        if (!await authorizationService.AuthorizeAsync(user, ContentsPermissions.RevokeDraftContent))
        {
            return null;
        }

        var model = new SharedDraftLinkViewModel();

        await context.Updater.TryUpdateModelAsync(model, Prefix);

        if (httpContextAccessor.HttpContext.Request.Form["submit.Save"] == "Revoke")
        {
            if (await linkService.RevokeLinkAsync(contentItem.ContentItemId))
            {
                await notifier.SuccessAsync(H["The draft link has been successfully revoked."]);
            }
            else
            {
                await notifier.ErrorAsync(H["The draft link could not be revoked."]);
            }
        }

        return await EditAsync(contentItem, context);
    }
}
