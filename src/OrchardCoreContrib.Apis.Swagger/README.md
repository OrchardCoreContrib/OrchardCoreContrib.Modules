# Swagger Module

This module allows you to create APIs documentations using Swagger.

## Version

1.2.0

## Category

Api

## Dependencies

This module has no dependencies.

## Features

| Name | Description |
| --- | --- |
| Swagger (`OrchardCoreContrib.Apis.Swagger`)  | Enables Swagger for OrchardCore APIs. |
| Swagger UI (`OrchardCoreContrib.Apis.Swagger.UI`) | Enables Swagger UI for OrchardCore APIs. |

## NuGet Packages

| Name | Version |
| --- | --- |
| [`OrchardCoreContrib.Apis.Swagger`](https://www.nuget.org/packages/OrchardCoreContrib.Apis.Swagger/1.2.0) | 1.2.0 |
| [`OrchardCoreContrib.Apis.Swagger`](https://www.nuget.org/packages/OrchardCoreContrib.Apis.Swagger/1.1.0) | 1.1.0 |
| [`OrchardCoreContrib.Apis.Swagger`](https://www.nuget.org/packages/OrchardCoreContrib.Apis.Swagger/1.0.1) | 1.0.1 |
| [`OrchardCoreContrib.Apis.Swagger`](https://www.nuget.org/packages/OrchardCoreContrib.Apis.Swagger/1.0.0) | 1.0.0 |

## Get Started

1. Install the [`OrchardCoreContrib.Apis.Swagger`](https://www.nuget.org/packages/OrchardCoreContrib.Apis.Swagger/) NuGet package to your Orchard Core host project.
2. There are two features included in this module:

    - Swagger Feature
        1. Go to the admin site
        2. Select **Configuration -> Features** menu.
        3. Enable the `Swagger` feature.
        4. Go to the site
        5. Visit the swagger end-point by append `/swagger/v1.0.0/swagger.json` to the URL.
    - Swagger UI Feature
        1. Go to the admin site
        2. Select **Configuration -> Features** menu.
        3. Enable the `Swagger UI` feature.
        4. Go to the site
        5. Visit the swagger end-point by append `/swagger/index.html` to the URL.

## How to get my APIs into Swagger docs?

Adding your APIs is not swagger docs is not a difficult task, simply you need:
1. Add `ApiController`.
2. Annotate the controller with `[Route("api/{module}")]`.
3. Add your `HttpGet` or `HttpPost` methods into your contoller.
4. Then your APIs should be displayed into the swagger docs.

So, If you tried toenable `Lucene` or `Queries` modules for instance, their APIs will show up immediately in the swagger docs. That's because every module defined an `ApiController` in the same way that mentioned above.

For more information about swagger, please refer to [ASP.NET Core web API documentation with Swagger / OpenAPI](https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger).

## Video

[![Watch the video](https://img.youtube.com/vi/Sa6qy_YnGSU/maxresdefault.jpg)](https://youtu.be/Sa6qy_YnGSU)
