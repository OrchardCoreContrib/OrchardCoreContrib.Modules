using OrchardCore.Modules.Manifest;
using OrchardCoreContrib.ContentPreview;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Content Preview",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.3.1",
    Category = "Content Management"
)]

[assembly: Feature(
    Id = Constants.PagePreviewBarFeatureId,
    Name = "Page Preview Bar",
    Description = "Shows a top bar that allows you to preview the current page in desktop, tablet and mobile.",
    Dependencies = new[] { "OrchardCore.Resources" }
)]
