using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "SendGrid",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.4.0",
    Description = "Provides email settings configuration for SendGrid service.",
    Category = "Messaging"
)]
