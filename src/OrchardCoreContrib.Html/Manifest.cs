using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Html",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.2.1"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Html.GrapesJS",
    Name = "GrapesJS HTML Editor",
    Description = "Enables GrapesJS editor for HtmlBody content.",
    Dependencies = new[] { "OrchardCore.Html" },
    Category = "Content Management"
)]
