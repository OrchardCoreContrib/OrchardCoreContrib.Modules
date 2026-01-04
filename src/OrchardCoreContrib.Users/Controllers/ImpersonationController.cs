using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.Modules;
using OrchardCore.Users;
using System.Security.Claims;

namespace OrchardCoreContrib.Users.Controllers;

[Authorize]
[Feature("OrchardCoreContrib.Users.Impersonation")]
public class ImpersonationController(
    SignInManager<IUser> signInManager,
    UserManager<IUser> userManager,
    IAuthorizationService authorizationService,
    IOptions<AdminOptions> adminOptions,
    ILogger<ImpersonationController> logger) : Controller
{
    private readonly AdminOptions _adminOptions = adminOptions.Value;

    public async Task<IActionResult> ImpersonateUser(string userId)
    {
        if (!await authorizationService.AuthorizeAsync(User, UsersPermissions.ManageImpersonationSettings))
        {
            return Forbid();
        }

        var currentUserId = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var impersonatedUser = await userManager.FindByIdAsync(userId);
        var impersonatedUserPrincipal = await signInManager.CreateUserPrincipalAsync(impersonatedUser);

        impersonatedUserPrincipal.Identities.First().AddClaim(new Claim(ClaimTypesExtended.ImpersonatorNameIdentifier, currentUserId));
        impersonatedUserPrincipal.Identities.First().AddClaim(new Claim(ClaimTypesExtended.IsImpersonating, "true"));

        await signInManager.SignOutAsync();
        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, impersonatedUserPrincipal);

        logger.LogInformation(1, "User '{0}' logged as '{1}'.", User.Identity.Name, impersonatedUser.UserName);

        return Redirect($"~/{_adminOptions.AdminUrlPrefix}");
    }

    public async Task<IActionResult> EndImpersonatation()
    {
        var isImpersonatingClaim = HttpContext.User.FindFirst(ClaimTypesExtended.IsImpersonating);
        if (isImpersonatingClaim == null || isImpersonatingClaim?.Value != "true")
        {
            return Forbid();
        }

        var impersonatorUserId = HttpContext.User.Claims.First(c => c.Type == ClaimTypesExtended.ImpersonatorNameIdentifier).Value;

        var impersonatedUserId = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var impersonatedUser = await userManager.FindByIdAsync(impersonatedUserId);
        var impersonatedUserPrincipal = await signInManager.CreateUserPrincipalAsync(impersonatedUser);

        await signInManager.SignOutAsync();

        var impersonaterUser = await userManager.FindByIdAsync(impersonatorUserId);
        var impersonaterUserPrincipal = await signInManager.CreateUserPrincipalAsync(impersonaterUser);

        await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, impersonaterUserPrincipal);

        logger.LogInformation(1, "Swicthed back to the '{0}' user.", impersonaterUser.UserName);

        return Redirect($"~/{_adminOptions.AdminUrlPrefix}");
    }
}
