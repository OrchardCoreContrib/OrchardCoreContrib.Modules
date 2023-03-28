using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Elm Diagnostics",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.2.0",
    Description = "Provides services to handle errors using Elm.",
    Category = "Infrastructure"
)]
