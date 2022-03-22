using OrchardCore.Modules.Manifest;

[assembly: Module(
    Author = "The Orchard Core Contrib Team",
    Version = "1.1.0",
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
