﻿using Microsoft.Extensions.Localization;
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
        if (string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
        {
            builder.Add(S["System"], "100", info => info
                .AddClass("system").Id("system")
                .Add(S["Info"], S["Info"].PrefixPosition(), updates => updates
                    .AddClass("info").Id("info")
                    .Action("About", "Admin", "OrchardCoreContrib.System")
                    .LocalNav()));
        }

        return Task.CompletedTask;
    }
}
