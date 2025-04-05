using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.Data.Documents;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Navigation;
using OrchardCore.Routing;
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
    IHtmlLocalizer<AdminController> H) : Controller
{
    private const string SearchKey = "q";

    private readonly PagerOptions pagerOptions = pagerOptions.Value;

    public async Task<ActionResult> Index(string search, PagerParameters pagerParameters)
    {
        if (!await authorizationService.AuthorizeAsync(User, UserGroupsPermissions.ManageUserGroups))
        {
            return Forbid();
        }

        var userGroups = await userGroupsManager.GetUserGroupsAsync();

        var pager = new Pager(pagerParameters, pagerOptions.GetPageSize());
        var count = userGroups.Count();
        if (!string.IsNullOrWhiteSpace(search))
        {
            userGroups = userGroups.Where(group => group.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        userGroups = userGroups.OrderBy(group => group.Name)
            .Skip(pager.GetStartIndex())
            .Take(pager.PageSize);

        var routeData = new RouteData();
        if (!string.IsNullOrEmpty(search))
        {
            routeData.Values.TryAdd(SearchKey, search);
        }

        var pagerShape = await shapeFactory.PagerAsync(pager, count, routeData);

        var model = new UserGroupsIndexViewModel
        {
            UserGroups = userGroups,
            Search = search,
            Pager = pagerShape
        };

        return View(model);
    }

    [HttpPost]
    [ActionName(nameof(Index))]
    [FormValueRequired("submit.Filter")]
    public ActionResult IndexFilterPost(UserGroupsIndexViewModel model) => RedirectToAction(nameof(Index), new RouteValueDictionary
    {
        { SearchKey, model.Search }
    });

    [HttpPost]
    [ActionName(nameof(Index))]
    [FormValueRequired("submit.BulkAction")]
    public async Task<ActionResult> IndexBulkActionPost(IEnumerable<string> selectedGroupNames)
    {
        if (!await authorizationService.AuthorizeAsync(User, UserGroupsPermissions.ManageUserGroups))
        {
            return Forbid();
        }

        if (selectedGroupNames is not null)
        {
            foreach (var name in selectedGroupNames)
            {
                var result = await userGroupsManager.DeleteAsync(name);
                if (result == IdentityResult.Success)
                {
                    await notifier.SuccessAsync(H["User groups have been removed successfully."]);
                }
                else
                {
                    var error = result.Errors.First().Description;
                    await notifier.ErrorAsync(new LocalizedHtmlString(error, error));
                }
            }
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
            var result = await userGroupsManager.CreateAsync(model.Name, model.Description);
            if (result == IdentityResult.Success)
            {
                await notifier.SuccessAsync(H["User Group created successfully."]);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Errors.First().Description);

                await documentStore.CancelAsync();

                await notifier.ErrorAsync(H["Error occurs during user group creation."]);
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
            Name = userGroup.Name,
            Description = userGroup.Description
        };

        return View(model);
    }

    [HttpPost, ActionName(nameof(Edit))]
    public async Task<IActionResult> EditPost(EditUserGroupViewModel model)
    {
        if (!await authorizationService.AuthorizeAsync(User, UserGroupsPermissions.ManageUserGroups))
        {
            return Forbid();
        }

        var userGroup = await userGroupsManager.FindByNameAsync(model.Name);
        if (userGroup is null)
        {
            return NotFound();
        }

        userGroup.Description = model.Description;

        var result = await userGroupsManager.UpdateAsync(userGroup);
        if (result == IdentityResult.Success)
        {
            await notifier.SuccessAsync(H["User group updated successfully."]);
        }
        else
        {
            ModelState.AddModelError(string.Empty, result.Errors.First().Description);

            await documentStore.CancelAsync();

            await notifier.ErrorAsync(H["Could not update this user group."]);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string name)
    {
        if (!await authorizationService.AuthorizeAsync(User, UserGroupsPermissions.ManageUserGroups))
        {
            return Forbid();
        }

        var result = await userGroupsManager.DeleteAsync(name);
        if (result == IdentityResult.Success)
        {
            await notifier.SuccessAsync(H["User group deleted successfully."]);
        }
        else
        {
            await documentStore.CancelAsync();

            await notifier.ErrorAsync(H["Could not delete this user group."]);

            var error = result.Errors.First().Description;
            await notifier.ErrorAsync(new LocalizedHtmlString(error, error));
        }

        return RedirectToAction(nameof(Index));
    }
}
