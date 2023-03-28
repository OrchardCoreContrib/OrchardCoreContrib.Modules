using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.2.0",
    Category = "Api"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Apis.Swagger",
    Name = "Swagger",
    Description = "Enables Swagger for OrchardCore APIs."
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Apis.Swagger.UI",
    Name = "Swagger UI",
    Description = "Enables Swagger UI for OrchardCore APIs.",
    Dependencies = new[] { "OrchardCoreContrib.Apis.Swagger" }
)]
