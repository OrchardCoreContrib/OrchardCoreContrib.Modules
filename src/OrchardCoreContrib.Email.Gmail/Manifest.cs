using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Gmail",
    Author = "Orchard Core Contrib",
    Website = "",
    Version = "1.1.0",
    Description = "Provides email settings configuration and a default email service based on Gmail service.",
    Dependencies = new string[] { "OrchardCore.Email" },
    Category = "Messaging"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Email.Gmail",
    Name = "Gmail",
    Description = "Allow you to send email(s) via Gmail service.",
    Category = "Messaging"
)]
