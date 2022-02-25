using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Html",
    Author = "Orchard Core Contrib",
    Website = "",
    Version = "1.0.0"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Html.GrapesJS",
    Name = "GrapesJS HTML Editor",
    Description = "Enables GrapesJS editor for HtmlBody content.",
    Dependencies = new[] { "OrchardCore.Html" },
    Category = "Content Management"
)]
