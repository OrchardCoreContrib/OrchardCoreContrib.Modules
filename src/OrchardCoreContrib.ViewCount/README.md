# View Count Module

This module allows the user to track the content item's views count.

## Version

1.0.0

## Category

Content Management

## Dependencies

| Product     | Module                            |
|-------------|-----------------------------------|
| OrchardCore | Contents (`OrchardCore.Contents`) |

## Features

|                  |											|
|------------------|------------------------------------------------------------|
| **Name**         | View Count (`OrchardCoreContrib.ViewCount`)                |
| **Description**  | Allows you to track the number of views of a content item.	|
| **Dependencies** | `OrchardCore.Contents`                                     |

## NuGet Packages

| Name                                                                                                    | Version |
|---------------------------------------------------------------------------------------------------------|---------|
| [`OrchardCoreContrib.ViewCount`](https://www.nuget.org/packages/OrchardCoreContrib.ViewCount/1.0.0)	  | 1.0.0   |

## Get Started

1. Install the [`OrchardCoreContrib.ViewCount`](https://www.nuget.org/packages/OrchardCoreContrib.ViewCount/) NuGet package to your Orchard Core host project.
2. Go to the admin site
3. Select **Configuration -> Features** menu.
4. Enable the `View Count` feature.
5. Go to the **Contents -> Content Definitions -> Content Types**
6. Edit the content type that you want to track its number of views.
7. Click the `Add Part` button.
8. Choose `View Count` from the list.
