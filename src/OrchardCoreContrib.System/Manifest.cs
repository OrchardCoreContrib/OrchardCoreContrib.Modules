using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "System",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.3.0",
    Category = "Utilities"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.System",
    Name = "System",
    Description = "Provides an information about currently running application.",
    DefaultTenantOnly = true
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.System.Updates",
    Name = "System Updates",
    Description = "Displays the available system updates.",
    Dependencies = new[] { "OrchardCoreContrib.System" },
    DefaultTenantOnly = true
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.System.Maintenance",
    Name = "System Maintenance",
    Description = "Put your site in maintenance mode while you're doing upgrades.",
    Dependencies = new[] { "OrchardCore.Autoroute" }
)]
