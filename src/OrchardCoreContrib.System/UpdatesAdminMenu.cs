using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;

namespace OrchardCoreContrib.System;

/// <summary>
/// Represents an admin menu for System Updates feature.
/// </summary>
public class UpdatesAdminMenu : INavigationProvider
{
    private readonly IStringLocalizer S;

    /// <summary>
    /// Initializes a new instance of <see cref="AdminMenu"/>.
    /// </summary>
    /// <param name="stringLocalizer">The <see cref="IStringLocalizer{AdminMenu}"/>.</param>
    public UpdatesAdminMenu(IStringLocalizer<AdminMenu> stringLocalizer)
    {
        S = stringLocalizer;
    }

    /// <inheritdoc/>
    public Task BuildNavigationAsync(string name, NavigationBuilder builder)
    {
        if (string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
        {
            //builder.Add(S["System Info"], "100", info => info
            //    .AddClass("info").Id("info")
            //    .Add(S["Updates"], S["Updates"].PrefixPosition(), updates => updates
            //        .AddClass("updates").Id("updates")
            //        .Action("Updates", "Admin", "OrchardCoreContrib.System")
            //        .LocalNav()));
            builder.Add(S["System"], "100", info => info
                .AddClass("system").Id("system")
                .Add(S["Info"], S["Info"].PrefixPosition(), updates => updates
                    .AddClass("info").Id("info")
                    .Action("About", "Admin", "OrchardCoreContrib.System")
                    .LocalNav())
                .Add(S["Updates"], S["Updates"].PrefixPosition(), updates => updates
                    .AddClass("updates").Id("updates")
                    .Action("Updates", "Admin", "OrchardCoreContrib.System")
                    .LocalNav()));
        }

        return Task.CompletedTask;
    }
}
