using OrchardCore.Modules.Manifest;

[assembly: Module(
    Author = "Orchard Core Contrib",
    Website = "",
    Version = "1.1.0"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Apis.Swagger",
    Name = "Swagger",
    Category = "Api",
    Description = "Enables Swagger for OrchardCore APIs."
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Apis.Swagger.UI",
    Name = "Swagger UI",
    Category = "Api",
    Description = "Enables Swagger UI for OrchardCore APIs.",
    Dependencies = new[] { "OrchardCoreContrib.Apis.Swagger" }
)]
