using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Multitenancy",
    Author = "The Orchard Core Contrib Team",
    Version = "1.0.0",
    Description = "Provides a way to manage tenants from the admin.",
    Category = "Infrastructure",
    Dependencies = new [] { "OrchardCore.Tenants" },
    DefaultTenantOnly = true
)]
