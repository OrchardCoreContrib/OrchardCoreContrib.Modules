# Content Permissions Module

This module allows you to protect your content items with permission(s).

## Version

1.1.0

## Category

Content Management

## Dependencies

This module has no dependencies.

## Features

|                  |                                                                                            |
|------------------|---------------------------------------------------------------|
| **Name**         | Content Permissions (`OrchardCoreContrib.ContentPermissions`) |
| **Description**  | Provides a control to access the contents via permission(s).  |
| **Dependencies** |	                               							   |

## NuGet Packages

| Name																													| Version     |
|-----------------------------------------------------------------------------------------------------------------------|-------------|
| [`OrchardCoreContrib.ContentPermissions`](https://www.nuget.org/packages/OrchardCoreContrib.ContentPermissions/1.1.0) | 1.1.0       |
| [`OrchardCoreContrib.ContentPermissions`](https://www.nuget.org/packages/OrchardCoreContrib.ContentPermissions/1.0.0) | 1.0.0       |

## Get Started

1. Install the [`OrchardCoreContrib.ContentPermissions`](https://www.nuget.org/packages/OrchardCoreContrib.ContentPermissions/) NuGet package to your Orchard Core host project.
2. Go to the admin site
3. Select **Configuration -> Features** menu.
4. Enable the `Content Permissions` feature.
5. Go to **Content > Content Definition > Content Types** menu.
6. Choose your content type that you need to apply the permission(s).
7. Click on the `Add Parts` button.
8. Choose the `Content Permissions` part.
9. Edit the part settings
	1. Check the `Enable Roles` if you want to control the access by roles.
	2. Check the `Enable Users` if you want to control the access by users.
10. Go to the **Content > Content Items** menu.
11. Edit the content item that you need to apply the permission(s).
12. Select the `Permissions` tab.
13. Choose the role(s) and / or users(s) which will access this content item.
