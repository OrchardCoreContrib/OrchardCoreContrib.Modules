using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Content Preview",
    Author = "The Orchard Core Contrib Team",
    Website = "",
    Version = "1.0.1",
    Category = "Content Management"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.ContentPreview.PagePreviewBar",
    Name = "Page Preview Bar",
    Description = "Shows a top bar that allows you to preview the current page in desktop, tablet and mobile.",
    Dependencies = new[] { "OrchardCore.Resources" }
)]
