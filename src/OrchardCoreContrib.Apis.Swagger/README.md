# Swagger Module

This module allows you to create APIs documentations using Swagger.

## Version

1.4.1

## Category

Api

## Dependencies

This module has no dependencies.

## Features

|                  |                                             |
|------------------|---------------------------------------------|
| **Name**         | Swagger (`OrchardCoreContrib.Apis.Swagger`) |
| **Description**  | Enables Swagger for OrchardCore APIs.       |
| **Dependencies** |                                             |

|                  |                                                |
|------------------|------------------------------------------------|
| **Name**         | Swagger (`OrchardCoreContrib.Apis.Swagger.UI`) |
| **Description**  | Enables Swagger UI for OrchardCore APIs.       |
| **Dependencies** | `OrchardCoreContrib.Apis.Swagger`              |

## NuGet Packages

| Name                                                                                                      | Version |
|-----------------------------------------------------------------------------------------------------------|---------|
| [`OrchardCoreContrib.Apis.Swagger`](https://www.nuget.org/packages/OrchardCoreContrib.Apis.Swagger/1.5.0) | 1.5.0   |
| [`OrchardCoreContrib.Apis.Swagger`](https://www.nuget.org/packages/OrchardCoreContrib.Apis.Swagger/1.4.1) | 1.4.1   |
| [`OrchardCoreContrib.Apis.Swagger`](https://www.nuget.org/packages/OrchardCoreContrib.Apis.Swagger/1.4.0) | 1.4.0   |
| [`OrchardCoreContrib.Apis.Swagger`](https://www.nuget.org/packages/OrchardCoreContrib.Apis.Swagger/1.3.0) | 1.3.0   |
| [`OrchardCoreContrib.Apis.Swagger`](https://www.nuget.org/packages/OrchardCoreContrib.Apis.Swagger/1.2.0) | 1.2.0   |
| [`OrchardCoreContrib.Apis.Swagger`](https://www.nuget.org/packages/OrchardCoreContrib.Apis.Swagger/1.1.0) | 1.1.0   |
| [`OrchardCoreContrib.Apis.Swagger`](https://www.nuget.org/packages/OrchardCoreContrib.Apis.Swagger/1.0.1) | 1.0.1   |
| [`OrchardCoreContrib.Apis.Swagger`](https://www.nuget.org/packages/OrchardCoreContrib.Apis.Swagger/1.0.0) | 1.0.0   |

## Get Started

1. Install the [`OrchardCoreContrib.Apis.Swagger`](https://www.nuget.org/packages/OrchardCoreContrib.Apis.Swagger/) NuGet package to your Orchard Core host project.
2. Go to the admin site
3. Select **Configuration -> Features** menu.

### Swagger

4. Enable the `Swagger` feature.
5. Go to the `{tenant-URL}/swagger/v1.0.0/swagger.json`, you should see all the APIs listed in JSON format.

### Swagger UI

4. Enable the `Swagger UI` feature.
5. Go to the `{tenant-URL}/swagger/index.html`, you should see all the APIs listed in a pretty styled page.

## How to get my APIs into Swagger docs?

Adding your APIs is not swagger docs is not a difficult task, simply you need:
1. Add `ApiController`.
2. Annotate the controller with `[Route("api/{module}")]`.
3. Add your `HttpGet` or `HttpPost` methods into your contoller.
4. Then your APIs should be displayed into the swagger docs.

So, If you tried to enable `Lucene` or `Queries` modules for instance, their APIs will show up immediately in the swagger docs. That's because every module defined an `ApiController` in the same way that mentioned above.

For more information about swagger, please refer to [ASP.NET Core web API documentation with Swagger / OpenAPI](https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger).

## Video

[![Watch the video](https://img.youtube.com/vi/Sa6qy_YnGSU/maxresdefault.jpg)](https://youtu.be/Sa6qy_YnGSU)
