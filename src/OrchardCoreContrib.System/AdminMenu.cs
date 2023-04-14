using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.Navigation;

namespace OrchardCoreContrib.System;

/// <summary>
/// Represents an admin menu for System module.
/// </summary>
public class AdminMenu : AdminNavigationProvider
{
    private readonly IStringLocalizer S;

    /// <summary>
    /// Initializes a new instance of <see cref="AdminMenu"/>.
    /// </summary>
    /// <param name="stringLocalizer">The <see cref="IStringLocalizer{AdminMenu}"/>.</param>
    public AdminMenu(IStringLocalizer<AdminMenu> stringLocalizer)
    {
        S = stringLocalizer;
    }

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
