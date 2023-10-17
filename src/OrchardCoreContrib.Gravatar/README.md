# Gravatar Module

This module enables user avatar using gravatar service.

## Version

1.3.0

## Category

Profile

## Dependencies

| Product     | Module                      |
|-------------|-----------------------------|
| OrchardCore | Users (`OrchardCore.Users`) |

## Features

|                  |                                             |
|------------------|---------------------------------------------|
| **Name**         | Gravatar (`OrchardCoreContrib.Gravatar`)    |
| **Description**  | Displays the user avatar on the admin menu. |
| **Dependencies** | `OrchardCore.Users`                         |

## NuGet Packages

| Name                                                                                              | Version |
|---------------------------------------------------------------------------------------------------|---------|
| [`OrchardCoreContrib.Gravatar`](https://www.nuget.org/packages/OrchardCoreContrib.Gravatar/1.3.0) | 1.3.0   |
| [`OrchardCoreContrib.Gravatar`](https://www.nuget.org/packages/OrchardCoreContrib.Gravatar/1.2.0) | 1.2.0   |
| [`OrchardCoreContrib.Gravatar`](https://www.nuget.org/packages/OrchardCoreContrib.Gravatar/1.1.0) | 1.1.0   |
| [`OrchardCoreContrib.Gravatar`](https://www.nuget.org/packages/OrchardCoreContrib.Gravatar/1.0.0) | 1.0.0   |

## Get Started

1. Install the [`OrchardCoreContrib.Gravatar`](https://www.nuget.org/packages/OrchardCoreContrib.Gravatar/) NuGet package to your Orchard Core host project.
2. Go to the admin site
3. Select **Configuration -> Features** menu.
4. Enable the `Gravatar` feature.
5. User avatar should be displayed on the user menu.

### Liquid Filters

The following filters allow for gravatar manipulation:

`gravatar_url`

Returns the URL of a gravatar, based on the email and optional size.

**Input**

```
{{ 'hishamco_2007@hotmail.com' | gravatar_url }}

{{ 'hishamco_2007@hotmail.com' | gravatar_url: size:32 }}
```

**Output**

```
http://www.gravatar.com/avatar/cbf0a05ad7eead6355a35843adc9d1c9?s=24&r=PG

http://www.gravatar.com/avatar/cbf0a05ad7eead6355a35843adc9d1c9?s=32&r=PG
```

#Video
[![Watch the video](https://img.youtube.com/vi/5gZ47lj2y2c/maxresdefault.jpg)](https://youtu.be/5gZ47lj2y2c)
