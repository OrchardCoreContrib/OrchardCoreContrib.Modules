using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Yet Another Reverse Proxy (YARP)",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.4.0",
    Description = "Enables configuration of hosting scenarios with a reverse proxy using YARP",
    Category = "Infrastructure"
)]
