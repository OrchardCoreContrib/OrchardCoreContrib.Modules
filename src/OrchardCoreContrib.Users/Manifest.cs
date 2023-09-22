using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Users",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.4.0",
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

