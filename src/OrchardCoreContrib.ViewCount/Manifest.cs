using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "View Count",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.0.0",
    Description = "Allows to count the content item views.",
    Dependencies = ["OrchardCore.Contents"],
    Category = "Content Management"
)]
