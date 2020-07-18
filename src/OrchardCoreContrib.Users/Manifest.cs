using OrchardCore.Modules.Manifest;

[assembly: Feature(
    Id = "OrchardCore.Users.Impersonation",
    Name = "Impersonate",
    Description = "This feature allow administrators to sign in with other user identity.",
    Dependencies = new[] { "OrchardCore.Users" },
    Category = "Security"
)]
