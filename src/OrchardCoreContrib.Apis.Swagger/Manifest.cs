﻿using OrchardCore.Modules.Manifest;

[assembly: Module(
    Author = "Orchard Core Contrib",
    Website = "",
    Version = "1.0.0"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Apis.Swagger",
    Name = "Swagger",
    Category = "Api",
    Description = "Enables Swagger for OrchardCore APIs."
)]

[assembly: Feature(
    Id = "OrchardCore.Apis.Swagger.UI",
    Name = "Swagger API documentation",
    Category = "Api",
    Description = "Enables Swagger UI for OrchardCore APIs.",
    Dependencies = new[] { "OrchardCoreContrib.Apis.Swagger" }
)]
