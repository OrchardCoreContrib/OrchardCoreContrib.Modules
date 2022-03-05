using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Users",
    Author = "Orchard Core Contrib",
    Website = "",
    Version = "1.1.0",
    Category = "Security"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Users.Impersonation",
    Name = "Impersonation",
    Description = "This feature allow administrators to sign in with other user identity.",
    Dependencies = new[] { "OrchardCore.Users" },
    Category = "Security"
)]

