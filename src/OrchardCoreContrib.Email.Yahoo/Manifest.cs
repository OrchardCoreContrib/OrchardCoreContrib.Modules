using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Yahoo",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.2.0",
    Description = "Provides email settings configuration for Yahoo service.",
    Category = "Messaging"
)]
