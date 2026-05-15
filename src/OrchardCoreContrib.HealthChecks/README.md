# Health Checks Module

This module provides a health checks for the website.

## Version

1.5.0

## Category

Infrastructure

## Dependencies

## Features

|                  |                                                  |
|------------------|--------------------------------------------------|
| **Name**         | Health Checks (`OrchardCoreContrib.HealthChecks`) |
| **Description**  | Provides a health checks for the website.        |
| **Dependencies** |                                                  |

|                  |                                                  |
|------------------|--------------------------------------------------|
| **Name**         | Health Checks IP Restriction (`OrchardCoreContrib.HealthChecks.IPRestriction`) |
| **Description**  | Restricts access to health checks endpoints by IP address. |
| **Dependencies** | Health Checks `OrchardCoreContrib.HealthChecks` |

|                  |                                                  |
|------------------|--------------------------------------------------|
| **Name**         | Health Checks Rate Limiting (`OrchardCoreContrib.HealthChecks.RateLimiting`) |
| **Description**  | Limits requests to health checks endpoints to prevent DOS attacks. |
| **Dependencies** | Health Checks `OrchardCoreContrib.HealthChecks` |

|                  |                                                  |
|------------------|--------------------------------------------------|
| **Name**         | Health Checks Blocking Rate Limiting (`OrchardCoreContrib.HealthChecks.BlockingRateLimiting`) |
| **Description**  | Adds blocking behavior to the health checks rate limiter. Clients exceeding the limit are temporarily blocked to prevent DoS attacks. |
| **Dependencies** | Health Checks Rate Limiting `OrchardCoreContrib.RateLimiting` |

## NuGet Packages

| Name                                                                                                      | Version |
|-----------------------------------------------------------------------------------------------------------|---------|
| [`OrchardCoreContrib.HealthChecks`](https://www.nuget.org/packages/OrchardCoreContrib.HealthChecks/1.5.0) | 1.5.0   |
| [`OrchardCoreContrib.HealthChecks`](https://www.nuget.org/packages/OrchardCoreContrib.HealthChecks/1.4.0) | 1.4.0   |
| [`OrchardCoreContrib.HealthChecks`](https://www.nuget.org/packages/OrchardCoreContrib.HealthChecks/1.3.0) | 1.3.0   |
| [`OrchardCoreContrib.HealthChecks`](https://www.nuget.org/packages/OrchardCoreContrib.HealthChecks/1.2.1) | 1.2.1   |
| [`OrchardCoreContrib.HealthChecks`](https://www.nuget.org/packages/OrchardCoreContrib.HealthChecks/1.2.0) | 1.2.0   |
| [`OrchardCoreContrib.HealthChecks`](https://www.nuget.org/packages/OrchardCoreContrib.HealthChecks/1.1.0) | 1.1.0   |
| [`OrchardCoreContrib.HealthChecks`](https://www.nuget.org/packages/OrchardCoreContrib.HealthChecks/1.0.0) | 1.0.0   |

## Get Started

1. Install the [`OrchardCoreContrib.HealthChecks`](https://www.nuget.org/packages/OrchardCoreContrib.HealthChecks/) NuGet package to your Orchard Core host project.
2. Go to the admin site
3. Select **Configuration -> Features** menu.

#### Health Checks

4. Enable the `Health Checks` feature.
5. Navigate to `{tenant-url}/health`, you should see the application health status for your website.

#### Health Checks IP Resriction

4. Enable the `Health Checks IP Resriction` feature.
5. Navigate to `{tenant-url}/health`, you should see the application health status for your website if your IP address is allowed.

#### Health Checks Rate Limiting

4. Enable the `Health Checks Rate Limiting` feature.
5. Navigate to `{tenant-url}/health`, you should see the application health status for your website if the rate limit has not been exceeded.

#### Health Checks Blocking Rate Limiting

4. Enable the `Health Checks Blocking Rate Limiting` feature.
5. Navigate to `{tenant-url}/health`, you should see the application health status for your website if the blocking rate limit has not been exceeded.

#Video
[![Watch the video](https://img.youtube.com/vi/M7xXGJNSdvg/maxresdefault.jpg)](https://youtu.be/M7xXGJNSdvg)
