using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "SendGrid",
    Author = "Orchard Core Contrib",
    Website = "",
    Version = "1.0.0",
    Description = "Provides email settings configuration and a default email service based on SendGrid service.",
    Dependencies = new string[] { "OrchardCore.Email" },
    Category = "Messaging"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.SendGrid.Gmail",
    Name = "SendGrid",
    Description = "Allow you to send email(s) via SendGrid service.",
    Category = "Messaging"
)]
