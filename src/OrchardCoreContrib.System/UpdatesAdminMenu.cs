using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.Navigation;

namespace OrchardCoreContrib.System;

using OrchardCoreContrib.Navigation;

/// <summary>
/// Represents an admin menu for System Updates feature.
/// </summary>
/// <remarks>
/// Initializes a new instance of <see cref="AdminMenu"/>.
/// </remarks>
/// <param name="S">The <see cref="IStringLocalizer{AdminMenu}"/>.</param>
public class UpdatesAdminMenu(IStringLocalizer<AdminMenu> S) : AdminNavigationProvider
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
            .Add(S["Updates"], S["Updates"].PrefixPosition(), updates => updates
                .AddClass("updates").Id("updates")
                .Action("Updates", "Admin", "OrchardCoreContrib.System")
                .LocalNav())
            );
    }
}
