# Liquid Module

This module provides a list of useful Liquid filters.

## Version

1.2.1

## Category

Content Management

## Dependencies

There's no dependencies.

## Features

|                  |                                      |
|------------------|--------------------------------------|
| **Name**         | Liquid (`OrchardCoreContrib.Liquid`) |
| **Description**  | Provides a list of liquid filters.   |
| **Dependencies** |                                      |

## NuGet Packages

| Name                                                                                                | Version      |
|-----------------------------------------------------------------------------------------------------|--------------|
| [`OrchardCoreContrib.Liquid`](https://www.nuget.org/packages/OrchardCoreContrib.Liquid/1.2.1)       | 1.2.1        |
| [`OrchardCoreContrib.Liquid`](https://www.nuget.org/packages/OrchardCoreContrib.Liquid/1.2.0)       | 1.2.0        |
| [`OrchardCoreContrib.Liquid`](https://www.nuget.org/packages/OrchardCoreContrib.Liquid/1.1.0)       | 1.1.0        |
| [`OrchardCoreContrib.Liquid`](https://www.nuget.org/packages/OrchardCoreContrib.Liquid/1.0.0)       | 1.0.0        |
| [`OrchardCoreContrib.Liquid`](https://www.nuget.org/packages/OrchardCoreContrib.Liquid/1.0.0-beta1) | 1.0.0-beta1  |

## Get Started

1. Install the [`OrchardCoreContrib.Liquid`](https://www.nuget.org/packages/OrchardCoreContrib.Liquid/) NuGet package to your Orchard Core host project.
2. Go to the admin site
3. Select **Configuration -> Features** menu.
4. Enable the `Liquid` feature.
5. User avatar should be displayed on the user menu.

### Liquid Objects

The following objects are accessible from within the liquid template:

`Environment`

Allows you to access the hosting enviornment object. The `Environment` object exposes three properties `IsDevelopment`, `IsStaging` and `IsProduction`.

```
{% if Environment.IsProduction %}
    <p>Your site is running on LIVE environment.</p>
{% endif %}
```
