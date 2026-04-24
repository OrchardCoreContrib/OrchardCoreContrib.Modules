using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Data Localization",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.3.0",
    Category = "Internationalization",
    Description = "Provides localization for dynamic data.",
    Dependencies = new[] { "OrchardCore.Localization" }
)]

