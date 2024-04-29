using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Gravatar",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.4.1",
    Category = "Profile",
    Description = "The gravatar module enables user avatar using gravatar service.",
    Dependencies = new[] { "OrchardCore.Users" }
)]
