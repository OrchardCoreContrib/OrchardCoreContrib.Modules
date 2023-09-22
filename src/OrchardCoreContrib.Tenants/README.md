# Multitenancy Module

This module provides features to manage tenants from the Admin UI.

## Version

1.1.0

## Category

Infrastructure

## Dependencies

| Product     | Module                          |
|-------------|---------------------------------|
| OrchardCore | Tenants (`OrchardCore.Tenants`) |

## Features

|                  |                                                  |
|------------------|--------------------------------------------------|
| **Name**         | Multitenancy (`OrchardCoreContrib.Tenants`)      |
| **Description**  | Provides a way to manage tenants from the admin. |
| **Dependencies** | `OrchardCore.Tenants`                            |

## NuGet Packages

| Name                                                                                            | Version |
|-------------------------------------------------------------------------------------------------|---------|
| [`OrchardCoreContrib.Tenants`](https://www.nuget.org/packages/OrchardCoreContrib.Tenants/1.1.0) | 1.1.0   |
| [`OrchardCoreContrib.Tenants`](https://www.nuget.org/packages/OrchardCoreContrib.Tenants/1.0.0) | 1.0.0   |

## Get Started

1. Install the [`OrchardCoreContrib.Tenants`](https://www.nuget.org/packages/OrchardCoreContrib.Tenants/) NuGet package to your Orchard Core host project.
2. Add `OrchardCoreContrib.Tenants` as setup feature on your web host application.
3. Navigate to `/{health-checks-url}/tenants`, you should see `Healthy` status whenever all the tenants are running, otherwise `Unhealthy`.
