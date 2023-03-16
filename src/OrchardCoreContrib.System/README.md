# System Module

Provides an information about currently running application.

## Version

1.2.0

## Category

Utilities

## Dependencies

## Features

| Name | Description |
| --- | --- |
| System (`OrchardCoreContrib.System`) | Provides an information about currently running application. |
| System Updates (`OrchardCoreContrib.System.Updates`) | Displays the available system updates. |
| System Maintenance (`OrchardCoreContrib.System.Maintenance`) | Put your site in maintenance mode while you do upgrades. |

## NuGet Packages

| Name | Version |
| --- | --- |
| [`OrchardCoreContrib.System`](https://www.nuget.org/packages/OrchardCoreContrib.System/1.2.0) | 1.2.0 |
| [`OrchardCoreContrib.System`](https://www.nuget.org/packages/OrchardCoreContrib.System/1.0.0) | 1.0.0 |

## Get Started

1. Install the [`OrchardCoreContrib.System`](https://www.nuget.org/packages/OrchardCoreContrib.System/) NuGet package to your Orchard Core host project.
2. Go to the admin site
3. Select **Configuration -> Features** menu.

### System

4. Enable the `System` feature.
5. `Info` submenu should be displayed on the `System` menu.
6. This page display an information about the currently running application.

### System Updates

4. Enable the `System Update` feature.
5. `Updates` submenu should shows up underneath the `System` menu.
6. If there'are a new updates a list of available updates should shows up.

### System Maintenance

4. Enable the `System Maintenance` feature.
5. Select **Configuration -> Settings -> System -> Maintenance** submenu.
6. Check `Allow maintenance mode` settings to let the website enters into the maintenance mode.