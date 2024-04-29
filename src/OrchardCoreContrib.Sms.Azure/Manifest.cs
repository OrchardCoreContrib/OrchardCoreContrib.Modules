using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Azure SMS",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.0.1",
    Description = "Provides settings and services to send SMS messages using Azure Communication Service.",
    Category = "Messaging"
)]
