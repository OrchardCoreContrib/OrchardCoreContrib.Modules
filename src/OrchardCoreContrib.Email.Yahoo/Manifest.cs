using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Yahoo",
    Author = "Orchard Core Contrib",
    Website = "",
    Version = "1.1.0",
    Description = "Provides email settings configuration and a default email service based on Yahoo service.",
    Dependencies = new string[] { "OrchardCore.Email" },
    Category = "Messaging"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Email.Yahoo",
    Name = "Yahoo",
    Description = "Allow you to send email(s) via Yahoo service.",
    Category = "Messaging"
)]
