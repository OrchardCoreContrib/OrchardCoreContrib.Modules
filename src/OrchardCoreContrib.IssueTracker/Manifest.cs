using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Issue Tracker",
    Author = "The Orchard Core Community",
    Website = "https://github.com/OrchardCoreContrib",
    Version = "0.0.1",
    Description = "Provides a set of features and services to manage and track issues.",
    Dependencies = new[] { "OrchardCore.Contents", "OrchardCore.Taxonomies", "OrchardCore.Media" },
    Category = "Content Management"
)]
