using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Health Checks",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.1.0",
    Description = "Provides health checks for the website.",
    Category = "Infrastructure"
)]
