using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Gravatar",
    Author = "The Orchard Core Contrib Team",
    Version = "1.0.0",
    Category = "Profile"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Gravatar",
    Name = "Gravatar",
    Description = "The gravatar module enables user avatar using gravatar service.",
    Dependencies = new[] { "OrchardCore.Users" }
)]

