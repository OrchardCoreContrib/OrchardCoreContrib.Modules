using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Gravatar",
    Author = "The Orchard Core Contrib Team",
    Version = "1.1.0",
    Category = "Profile",
    Description = "The gravatar module enables user avatar using gravatar service.",
    Dependencies = new[] { "OrchardCore.Users" }
)]
