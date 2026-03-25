using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Contents",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.0.0",
    Category = "Content Management"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Contents.ShareDraftContent",
    Name = "Share Draft Content",
    Description = "Allows sharing of draft content items via a unique link.",
    Dependencies = ["OrchardCore.Contents"]
)]
