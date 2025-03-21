# Users Module

This module provides features for users management.

## Version

1.5.1

## Category

Security

## Dependencies

| Product     | Module                      |
|-------------|-----------------------------|
| OrchardCore | Users (`OrchardCore.Users`) |

## Features

|                  |                                                                |
|------------------|----------------------------------------------------------------|
| **Name**         | Users Impersonation (`OrchardCoreContrib.Users.Impersonation`) |
| **Description**  | Allows the administrators to sign in with other user identity. |
| **Dependencies** | `OrchardCore.Users`                                            |

|                  |                                                 |
|------------------|-------------------------------------------------|
| **Name**         | User Avatar (`OrchardCoreContrib.Users.Avatar`) |
| **Description**  | Displays the user avatar on the admin menu.     |
| **Dependencies** | `OrchardCore.Users`                             |

## NuGet Packages

| Name                                                                                        | Version |
|---------------------------------------------------------------------------------------------|---------|
| [`OrchardCoreContrib.Users`](https://www.nuget.org/packages/OrchardCoreContrib.Users/1.6.0) | 1.6.0   |
| [`OrchardCoreContrib.Users`](https://www.nuget.org/packages/OrchardCoreContrib.Users/1.5.1) | 1.5.1   |
| [`OrchardCoreContrib.Users`](https://www.nuget.org/packages/OrchardCoreContrib.Users/1.5.0) | 1.5.0   |
| [`OrchardCoreContrib.Users`](https://www.nuget.org/packages/OrchardCoreContrib.Users/1.4.0) | 1.4.0   |
| [`OrchardCoreContrib.Users`](https://www.nuget.org/packages/OrchardCoreContrib.Users/1.3.0) | 1.3.0   |
| [`OrchardCoreContrib.Users`](https://www.nuget.org/packages/OrchardCoreContrib.Users/1.2.0) | 1.2.0   |
| [`OrchardCoreContrib.Users`](https://www.nuget.org/packages/OrchardCoreContrib.Users/1.1.0) | 1.1.0   |
| [`OrchardCoreContrib.Users`](https://www.nuget.org/packages/OrchardCoreContrib.Users/1.0.0) | 1.0.0   |

## Get Started

1. Install the [`OrchardCoreContrib.Users`](https://www.nuget.org/packages/OrchardCoreContrib.Users/) NuGet package to your Orchard Core host project.
2. Go to the admin site
3. Select **Configuration -> Features** menu.

### Impersonation

4. Enable the `Impersonation` feature.
5. Select **Security -> Users** menu.
6. `Impersonate` button will show up for every user in the list.
7. Click the `Impersonate` button for the user that you want to log with his account. After that you will be redirected to the admin site with newly credentials. 
8. Select **Security -> End Impersonation** menu to end the current session.

### User Avatar

4. Enable the `User Avatar` feature.
5. User avatar should be displayed on the user menu.

## Video

[![Watch the video](https://img.youtube.com/vi/gXC3mDPy7LA/maxresdefault.jpg)](https://youtu.be/gXC3mDPy7LA)
