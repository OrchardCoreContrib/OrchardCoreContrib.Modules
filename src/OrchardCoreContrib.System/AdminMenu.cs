using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;

namespace OrchardCoreContrib.System;

using OrchardCoreContrib.Navigation;

/// <summary>
/// Represents an admin menu for System module.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="AdminMenu"/>.
/// </remarks>
/// <param name="S">The <see cref="IStringLocalizer{AdminMenu}"/>.</param>
public class AdminMenu(IStringLocalizer<AdminMenu> S) : AdminNavigationProvider
{
    /// <inheritdoc/>
    public override void BuildNavigation(NavigationBuilder builder)
    {
        builder.Add(S["System"], "100", info => info
            .AddClass("system").Id("system")
            .Add(S["Info"], S["Info"].PrefixPosition(), updates => updates
                .AddClass("info").Id("info")
                .Action("About", "Admin", "OrchardCoreContrib.System")
                .LocalNav())
            );
    }
}
