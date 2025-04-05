using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.Data.Documents;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Navigation;
using OrchardCore.Routing;
using OrchardCoreContrib.UserGroups.Models;
using OrchardCoreContrib.UserGroups.Services;
using OrchardCoreContrib.UserGroups.ViewModels;
using YesSql.Services;

namespace OrchardCoreContrib.UserGroups.Controllers;

[Admin("UserGroups/{action}/{name?}", "UserGroups{action}")]
public class AdminController(
    IDocumentStore documentStore,
    UserGroupsManager userGroupsManager,
    IAuthorizationService authorizationService,
    IOptions<PagerOptions> pagerOptions,
    IShapeFactory shapeFactory,
    INotifier notifier,
    IStringLocalizer<AdminController> S,
    IHtmlLocalizer<AdminController> H) : Controller
{
    private const string _optionsSearch = "Options.Search";

    private readonly PagerOptions pagerOptions = pagerOptions.Value;

    public async Task<ActionResult> Index(ContentOptions options, PagerParameters pagerParameters)
    {
        if (!await authorizationService.AuthorizeAsync(User, UserGroupsPermissions.ManageUserGroups))
        {
            return Forbid();
        }

        var userGroups = await userGroupsManager.GetUserGroupsAsync();

        var pager = new Pager(pagerParameters, pagerOptions.GetPageSize());
        var count = userGroups.Count();
        if (!string.IsNullOrWhiteSpace(options.Search))
        {
            userGroups = userGroups.Where(group => group.Name.Contains(options.Search, StringComparison.OrdinalIgnoreCase));
        }

        userGroups = userGroups.OrderBy(group => group.Name)
            .Skip(pager.GetStartIndex())
            .Take(pager.PageSize);

        var routeData = new RouteData();
        if (!string.IsNullOrEmpty(options.Search))
        {
            routeData.Values.TryAdd(_optionsSearch, options.Search);
        }

        //options.ContentsBulkAction =
        //[
        //    new SelectListItem(S["Delete"],
        //    nameof(ContentsBulkAction.Remove))
        //];

        var pagerShape = await shapeFactory.PagerAsync(pager, count, routeData);

        var model = new UserGroupsIndexViewModel
        {
            UserGroupEntries = userGroups.Select(group => new UserGroupEntry
            { 
                Name = group.Name,
                UserGroup = group
            }).ToList(),
            Options = options,
            Pager = pagerShape
        };

        return View(model);
    }

    [HttpPost]
    [ActionName(nameof(Index))]
    [FormValueRequired("submit.Filter")]
    public ActionResult IndexFilterPost(UserGroupsIndexViewModel model) => RedirectToAction(nameof(Index), new RouteValueDictionary
    {
        { _optionsSearch, model.Options.Search }
    });

    [HttpPost]
    [ActionName(nameof(Index))]
    [FormValueRequired("submit.BulkAction")]
    public async Task<ActionResult> IndexBulkActionPost(ContentOptions options, IEnumerable<string> selectedGroupNames)
    {
        if (!await authorizationService.AuthorizeAsync(User, UserGroupsPermissions.ManageUserGroups))
        {
            return Forbid();
        }

        switch (options.BulkAction)
        {
            case BulkAction.None:
                break;
            case BulkAction.Remove:
                if (selectedGroupNames is not null)
                {
                    foreach (var id in selectedGroupNames)
                    {
                        await userGroupsManager.DeleteAsync(id);
                    }

                    await notifier.SuccessAsync(H["User groups have been removed successfully."]);
                }

                break;
            default:
                return BadRequest();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Create()
    {
        if (!await authorizationService.AuthorizeAsync(User, UserGroupsPermissions.ManageUserGroups))
        {
            return Forbid();
        }

        var model = new CreateUserGroupViewModel();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserGroupViewModel model)
    {
        if (!await authorizationService.AuthorizeAsync(User, UserGroupsPermissions.ManageUserGroups))
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            model.Name = model.Name.Trim();

            if (model.Name.Any(c => Path.GetInvalidPathChars().Contains(c)))
            {
                ModelState.AddModelError(string.Empty, S["Invalid user group name."]);
            }

            if (await userGroupsManager.FindByNameAsync(model.Name) is not null)
            {
                ModelState.AddModelError(string.Empty, S["The user group is already used."]);
            }

            var group = new UserGroup
            {
                Name = model.Name,
                Description = model.Description,
            };

            try
            {
                await userGroupsManager.CreateAsync(group);

                await notifier.SuccessAsync(H["User Group created successfully."]);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                await notifier.ErrorAsync(H["Error occurs during user group creation."]);

                await documentStore.CancelAsync();

                ModelState.AddModelError(string.Empty, "Error occurs during user group creation.");
            }
        }

        return View(model);
    }

    public async Task<IActionResult> Edit(string name)
    {
        if (!await authorizationService.AuthorizeAsync(User, UserGroupsPermissions.ManageUserGroups))
        {
            return Forbid();
        }

        var userGroup = await userGroupsManager.FindByNameAsync(name);
        if (userGroup is null)
        {
            return NotFound();
        }

        var model = new EditUserGroupViewModel
        {
            UserGroup = userGroup,
            Name = userGroup.Name,
            Description = userGroup.Description
        };

        return View(model);
    }

    [HttpPost, ActionName(nameof(Edit))]
    public async Task<IActionResult> EditPost(string name, string description)
    {
        if (!await authorizationService.AuthorizeAsync(User, UserGroupsPermissions.ManageUserGroups))
        {
            return Forbid();
        }

        var userGroup = await userGroupsManager.FindByNameAsync(name);
        if (userGroup is null)
        {
            return NotFound();
        }

        userGroup.Description = description;

        await userGroupsManager.UpdateAsync(userGroup);

        await notifier.SuccessAsync(H["User group updated successfully."]);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string name)
    {
        if (!await authorizationService.AuthorizeAsync(User, UserGroupsPermissions.ManageUserGroups))
        {
            return Forbid();
        }

        if (await userGroupsManager.FindByNameAsync(name) is null)
        {
            return NotFound();
        }

        try
        {
            await userGroupsManager.DeleteAsync(name);

            await notifier.SuccessAsync(H["User group deleted successfully."]);
        }
        catch
        {
            await documentStore.CancelAsync();

            await notifier.ErrorAsync(H["Could not delete this user group."]);
        }

        return RedirectToAction(nameof(Index));
    }
}
