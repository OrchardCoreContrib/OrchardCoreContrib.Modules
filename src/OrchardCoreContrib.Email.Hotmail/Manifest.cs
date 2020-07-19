using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Hotmail",
    Author = "Orchard Core Contrib",
    Website = "",
    Version = "1.0.0",
    Description = "Provides email settings configuration and a default email service based on Hotmail service.",
    Dependencies = new string[] { "OrchardCore.Email" },
    Category = "Messaging"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Email.Hotmail",
    Name = "Hotmail",
    Description = "Allow you to send email(s) via Hotmail service.",
    Category = "Messaging"
)]
