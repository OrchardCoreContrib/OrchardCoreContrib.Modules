using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Elm Diagnostics",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.4.1",
    Description = "Provides services to handle errors using Elm.",
    Category = "Infrastructure"
)]
