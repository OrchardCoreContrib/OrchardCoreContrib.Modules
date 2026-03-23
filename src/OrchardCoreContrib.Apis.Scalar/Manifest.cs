using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Scalar UI",
    Description = "Enables Scalar UI for Orchard Core APIs.",
    Dependencies = ["OrchardCoreContrib.Apis.Swagger"]
)]
