using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Tenants",
    Author = "The Orchard Core Contrib Team",
    Version = "1.0.0"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Tenants",
    Name = "Tenants",
    Description = "Provides a way to manage tenants from the admin.",
    Category = "Infrastructure",
    DefaultTenantOnly = true
)]
