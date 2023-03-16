using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Users",
    Author = "The Orchard Core Contrib Team",
    Version = "1.3.0",
    Category = "Security"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Users.Avatar",
    Name = "User Avatar",
    Description = "This feature allow to display a user avatar on the admin menu.",
    Dependencies = new[] { "OrchardCore.Users" }
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Users.Impersonation",
    Name = "Users Impersonation",
    Description = "This feature allow administrators to sign in with other user identity.",
    Dependencies = new[] { "OrchardCore.Users" }
)]

