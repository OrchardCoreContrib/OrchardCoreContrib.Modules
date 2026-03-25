using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCoreContrib.Contents.ViewModels;

namespace OrchardCoreContrib.Contents.Drivers;

public sealed class ShareDraftContentDriver(IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor) : ContentDisplayDriver
{
    public override async Task<IDisplayResult> EditAsync(ContentItem contentItem, BuildEditorContext context)
    {
        var user = httpContextAccessor.HttpContext.User;

        if (!await authorizationService.AuthorizeAsync(user, ContentsPermissions.ShareDraftContent))
        {
            return null;
        }

        return Initialize<ShareDraftViewModel>("Content_ShareDraftButton", model => model.ContentItem = contentItem)
            .RenderWhen(() => Task.FromResult(!contentItem.Published))
            .Location("Actions:30");
    }
}
