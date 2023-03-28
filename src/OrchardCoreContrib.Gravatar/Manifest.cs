using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Gravatar",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.2.0",
    Category = "Profile",
    Description = "The gravatar module enables user avatar using gravatar service.",
    Dependencies = new[] { "OrchardCore.Users" }
)]
