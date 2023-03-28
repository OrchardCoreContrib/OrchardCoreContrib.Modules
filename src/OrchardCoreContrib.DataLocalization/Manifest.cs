using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Data Localization",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.0.0-beta4",
    Category = "Internationalization",
    Dependencies = new[] { "OrchardCore.Localization" }
)]

