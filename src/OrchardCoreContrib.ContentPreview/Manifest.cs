using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Content Preview",
    Author = "Orchard Core Contrib",
    Website = "",
    Version = "1.0.0",
    Category = "Content Management"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.ContentPreview",
    Name = "Page Preview Bar",
    Description = "Shows a top bar that allows you to preview the current page in desktop, tablet and mobile.",
    Category = "Content Management",
    Dependencies = new[] { "OrchardCore.Resources" }
)]
