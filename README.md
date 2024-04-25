# OrchardCoreContrib

This repository contains a set of features and APIs for [Orchard Core CMS](https://github.com/OrchardCMS/OrchardCore) that driven by the community members who love Orchard Core.

This will encourage all the passionate developers to add and develop the other necessary core features that aren't included in Orchard Core. Such feature and APIs may necessary to drive other [modules](https://github.com/OrchardCoreContrib/OrchardCoreContrib.Modules) or [themes](https://github.com/OrchardCoreContrib/OrchardCoreContrib.Themes) for Orchard Core Contrib.

## Build Status

[![Build status](https://github.com/OrchardCoreContrib/OrchardCoreContrib/actions/workflows/build.yml/badge.svg)](https://github.com/OrchardCoreContrib/OrchardCoreContrib/actions?query=workflow%3A%22Orchard+Core+Contrib%22)

## Goals

There are many goals for creating this repository:

1. Grow the Orchard Core community.

2. Design & develop a crazy ideas that may or could help the Orchard Core team to consider some of these in the future releases, if they are frequently asked by the community.

3. Implement features that may not included in the official release, which may help the community in the way or other.

## Documentations

The `OrchardCoreContrib` repository consists of the following projects:

| Name | Namespace | NuGet |
| --- | --- | --- |
| [Orchard Core Contrib Implementation APIs](src/OrchardCoreContrib/README.md) | `OrchardCoreContrib` ||
| [Orchard Core Contrib Abstractions APIs](src/OrchardCoreContrib.Abstractions/README.md) | `OrchardCoreContrib.Abstractions` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.Abstractions.svg)](https://www.nuget.org/packages/OrchardCoreContrib.Abstractions) |
| [Content Localization Abstractions APIs](src/OrchardCoreContrib.ContentLocalization.Abstractions/README.md) | `OrchardCoreContrib.ContentLocalization.Abstractions` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.ContentLocalization.Abstractions.svg)](https://www.nuget.org/packages/OrchardCoreContrib.ContentLocalization.Abstractions) |
| [Content Localization Implementations APIs](src/OrchardCoreContrib.ContentLocalization.Core/README.md) | `OrchardCoreContrib.ContentLocalization.Core` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.ContentLocalization.Core.svg)](https://www.nuget.org/packages/OrchardCoreContrib.ContentLocalization.Core) |
| [Data Abstractions APIs](src/OrchardCoreContrib.Data.Abstractions/README.md) | `OrchardCoreContrib.Data.Abstractions` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.Data.Abstractions.svg)](https://www.nuget.org/packages/OrchardCoreContrib.Data.Abstractions) |
| [Data Implementations APIs](src/OrchardCoreContrib.Data/README.md) | `OrchardCoreContrib.Data` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.Data.svg)](https://www.nuget.org/packages/OrchardCoreContrib.Data) |
| [YesSQL Data APIs](src/OrchardCoreContrib.Data.YesSql/README.md) | `OrchardCoreContrib.Data.YesSql` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.Data.YesSql.svg)](https://www.nuget.org/packages/OrchardCoreContrib.Data.YesSql) |
| [Email APIs](src/OrchardCoreContrib.Email/README.md) | `OrchardCoreContrib.Email` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.Email.svg)](https://www.nuget.org/packages/OrchardCoreContrib.Email) |
| [Email Abstractions](src/OrchardCoreContrib.Email.Abstractions/README.md) | `OrchardCoreContrib.Email.Abstractions` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.Email.Abstractions.svg)](https://www.nuget.org/packages/OrchardCoreContrib.Email.Abstractions) |
| [Health Checks Abstractions](src/OrchardCoreContrib.HealthChecks.Abstractions/README.md) | `OrchardCoreContrib.HealthChecks.Abstractions` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.HealthChecks.Abstractions.svg)](https://www.nuget.org/packages/OrchardCoreContrib.HealthChecks.Abstractions) |
| [Infrastructure Abstractions APIs](src/OrchardCoreContrib.Infrastructure.Abstractions/README.md) | `OrchardCoreContrib.Infrastructure.Abstractions` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.Infrastructure.Abstractions.svg)](https://www.nuget.org/packages/OrchardCoreContrib.Infrastructure.Abstractions) |
| [Infrastructure Implementation APIs](src/OrchardCoreContrib.Infrastructure/README.md) | `OrchardCoreContrib.Infrastructure` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.Infrastructure.svg)](https://www.nuget.org/packages/OrchardCoreContrib.Infrastructure) |
| [LINQ to Orchard Core](src/OrchardCoreContrib.Linq/README.md) | `OrchardCoreContrib.Linq` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.Linq.svg)](https://www.nuget.org/packages/OrchardCoreContrib.Linq) |
| [Localization Implementation APIs](src/OrchardCoreContrib.Localization/README.md) | `OrchardCoreContrib.Localization` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.Localization.svg)](https://www.nuget.org/packages/OrchardCoreContrib.Localization) |
| [Localization Abstractions APIs](src/OrchardCoreContrib.Localization.Abstractions/README.md) | `OrchardCoreContrib.Localization.Abstractions` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.Localization.Abstractions.svg)](https://www.nuget.org/packages/OrchardCoreContrib.Localization.Abstractions) |
| [JSON Localization APIs](src/OrchardCoreContrib.Localization.Json/README.md) | `OrchardCoreContrib.Localization.Json` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.Localization.Json.svg)](https://www.nuget.org/packages/OrchardCoreContrib.Localization.Json) |
| [XLIFF Localization APIs](src/OrchardCoreContrib.Localization.Xliff/README.md) | `OrchardCoreContrib.Localization.Xliff` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.Localization.Xliff.svg)](https://www.nuget.org/packages/OrchardCoreContrib.Localization.Xliff) |
| [Navigation Abstractions APIs](src/OrchardCoreContrib.Navigation.Abstractions/README.md) | `OrchardCoreContrib.Navigation.Abstractions` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.Navigation.Abstractions.svg)](https://www.nuget.org/packages/OrchardCoreContrib.Navigation.Abstractions) |
| [Navigation Implementations APIs](src/OrchardCoreContrib.Navigation.Core/README.md) | `OrchardCoreContrib.Navigation.Core` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.Navigation.Core.svg)](https://www.nuget.org/packages/OrchardCoreContrib.Navigation.Core) |
| [OpenApi Abstractions APIs](src/OrchardCoreContrib.OpenApi.Abstractions/README.md) | `OrchardCoreContrib.OpenApi.Abstractions` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.OpenApi.Abstractions.svg)](https://www.nuget.org/packages/OrchardCoreContrib.OpenApi.Abstractions) |
| [Shortcodes Abstractions APIs](src/OrchardCoreContrib.Shortcodes.Abstractions/README.md) | `OrchardCoreContrib.Shortcodes.Abstractions` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.Shortcodes.Abstractions.svg)](https://www.nuget.org/packages/OrchardCoreContrib.Shortcodes.Abstractions) |
| [Shortcodes Implementation APIs](src/OrchardCoreContrib.Shortcodes.Core/README.md) | `OrchardCoreContrib.Shortcodes.Core` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.Shortcodes.Core.svg)](https://www.nuget.org/packages/OrchardCoreContrib.Shortcodes.Core) |
| [SMS Abstractions APIs](src/OrchardCoreContrib.Sms.Abstractions/README.md) | `OrchardCoreContrib.Sms.Abstractions` | [![NuGet](https://img.shields.io/nuget/v/OrchardCoreContrib.Sms.Abstractions.svg)](https://www.nuget.org/packages/OrchardCoreContrib.Sms.Abstractions) |
