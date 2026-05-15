using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.6.0",
    Category = "Api"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Apis.Swagger",
    Name = "Swagger",
    Description = "Enables Swagger for Orchard Core APIs."
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Apis.Swagger.UI",
    Name = "Swagger UI",
    Description = "Enables Swagger UI for Orchard Core APIs.",
    Dependencies = ["OrchardCoreContrib.Apis.Swagger"]
)]
