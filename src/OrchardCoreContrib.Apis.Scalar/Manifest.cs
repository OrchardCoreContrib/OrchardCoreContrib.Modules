using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Scalar UI",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.0.0",
    Description = "Enables Scalar UI for Orchard Core APIs.",
    Dependencies = ["OrchardCoreContrib.Apis.Swagger"]
)]
