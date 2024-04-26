using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Health Checks",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.2.1",
    Description = "Provides health checks for the website.",
    Category = "Infrastructure"
)]
