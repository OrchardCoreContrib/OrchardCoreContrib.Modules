# Users Module

This module provides features for users management.

## Version

1.1.0

## Category

Security

## Dependencies

| Product | Module |
| --- | --- |
| OrchardCore | Users (`OrchardCore.Users`) |

## Features

| Name | Description |
| --- | --- |
| Users Impersonation (`OrchardCoreContrib.Users.Impersonation`) | Allows the administrators to sign in with other user identity. |

## NuGet Packages

| Name | Version |
| --- | --- |
| [`OrchardCoreContrib.Users`](https://www.nuget.org/packages/OrchardCoreContrib.Users/1.1.0) | 1.1.0 |
| [`OrchardCoreContrib.Users`](https://www.nuget.org/packages/OrchardCoreContrib.Users/1.0.0) | 1.0.0 |

## Get Started

1. Install the [`OrchardCoreContrib.Users`](https://www.nuget.org/packages/OrchardCoreContrib.Users/) NuGet package to your Orchard Core host project.
2. Go to the admin site
3. Select **Configuration -> Features** menu.
4. Enable the `Impersonation` feature.
5. Select **Security -> Users** menu.
6. `Impersonate` button will show up for every user in the list.
7. Click the `Impersonate` button for the user that you want to log with his account. After that you will be redirected to the admin site with newly credentials. 
8. Select **Security -> End Impersonation** menu to end the current session.

## Video

[![Watch the video](https://img.youtube.com/vi/gXC3mDPy7LA/maxresdefault.jpg)](https://youtu.be/gXC3mDPy7LA)
