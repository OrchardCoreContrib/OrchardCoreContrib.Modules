using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Health Checks",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.5.0",
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
    Description = "Restricts access to health checks endpoints by IP address.",
    Dependencies = [ "OrchardCoreContrib.HealthChecks" ]
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.HealthChecks.RateLimiting",
    Name = "Health Checks Rate Limiting",
    Description = "Limits requests to health checks endpoints to prevent DOS attacks.",
    Dependencies = ["OrchardCoreContrib.HealthChecks"]
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.HealthChecks.BlockingRateLimiting",
    Name = "Health Checks Blocking Rate Limiting",
    Description = "Adds blocking behavior to the health checks rate limiter. Clients exceeding the limit are temporarily blocked to prevent DoS attacks.",
    Dependencies = new[] { "OrchardCoreContrib.HealthChecks.RateLimiting" }
)]
