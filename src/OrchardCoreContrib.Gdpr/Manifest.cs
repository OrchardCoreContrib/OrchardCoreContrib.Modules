using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "GDPR",
    Author = "The Orchard Core Contrib Team",
    Website = "",
    Version = "1.0.0",
    Description = "Supports EU General Data Protection Regulation (GDPR).",
    Dependencies = new string[] { "OrchardCore.Gdpr" },
    Category = "Security"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Gdpr",
    Name = "GDPR",
    Description = "Supports EU General Data Protection Regulation (GDPR).",
    Category = "Security"
)]
