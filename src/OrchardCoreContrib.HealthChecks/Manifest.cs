using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Health Checks",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.4.0",
    Category = "Infrastructure"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.HealthChecks",
    Name = "Health Checks",
    Description = "Provides health checks for the website."
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.HealthChecks.IPRestriction",
    Name = "Health Checks IP Restriction",
    Description = "Restricts access to health check endpoints by IP address.",
    Dependencies = [ "OrchardCoreContrib.HealthChecks" ]
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.HealthCheck.RateLimiting",
    Name = "Health Check Rate Limiting",
    Description = "Limits requests to health check endpoints to prevent DOS attacks.",
    Dependencies = ["OrchardCoreContrib.HealthChecks"]
)]
