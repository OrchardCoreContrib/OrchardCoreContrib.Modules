using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "GMail",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.3.0",
    Description = "Provides email settings configuration for GMail service.",
    Category = "Messaging"
)]
