using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;

namespace OrchardCoreContrib.System;

/// <summary>
/// Represents an admin menu for System module.
/// </summary>
public class AdminMenu : INavigationProvider
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
    public Task BuildNavigationAsync(string name, NavigationBuilder builder)
    {
        if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
        {
            return Task.CompletedTask;
        }

        builder.Add(S["System Info"], "100", info => info.AddClass("info").Id("info")
            .Action("About", "Admin", new { area = "OrchardCoreContrib.System" })
            .LocalNav());

        return Task.CompletedTask;
    }
}
