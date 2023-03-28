using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "GDPR",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.0.0",
    Description = "Supports EU General Data Protection Regulation (GDPR).",
    Dependencies = new string[] { "OrchardCore.Gdpr" },
    Category = "Security"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Gdpr",
    Name = "GDPR",
    Description = "Supports EU General Data Protection Regulation (GDPR).",
    Category = "Security"
)]
