using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Google Maps",
    Author = "The Orchard Core Contrib Team",
    Version = "1.0.0",
    Description = "Displays Google maps.",
    Dependencies = new[] { "OrchardCore.Contents" },
    Category = "Content Management"
)]
