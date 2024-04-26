using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Multitenancy",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.2.1",
    Description = "Provides a way to manage tenants from the admin.",
    Category = "Infrastructure",
    Dependencies = new [] { "OrchardCore.Tenants" },
    DefaultTenantOnly = true
)]
