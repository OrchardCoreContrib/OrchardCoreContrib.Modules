# Content Localization Module

This module provides features for localizing content.

## Version

1.3.1

## Category

Internationalization

## Dependencies

This module has no dependencies.

## Features

|                  |                                                                                   |
|------------------|-----------------------------------------------------------------------------------|
| **Name**         | Localization Matrix (`OrchardCoreContrib.ContentLocalization.LocalizationMatrix`) |
| **Description**  | Shows a matix for localized content per culture.                                  |
| **Dependencies** | `OrchardCoreContrib.ContentLocalization`<br/> `OrchardCore.ContentLocalization`   |

|                  |                                                                                                 |
|------------------|-------------------------------------------------------------------------------------------------|
| **Name**         | Transliteration (`OrchardCoreContrib.ContentLocalization.Transliteration`)                      |
| **Description**  | Provides a type of conversion of a text from one script to another that involves swapping letters. |
| **Dependencies** | `OrchardCore.ContentLocalization`                                                               |

## NuGet Packages

| Name                                                                                                                    | Version |
|-------------------------------------------------------------------------------------------------------------------------|---------|
| [`OrchardCoreContrib.ContentLocalization`](https://www.nuget.org/packages/OrchardCoreContrib.ContentLocalization/1.3.1) | 1.3.1   |
| [`OrchardCoreContrib.ContentLocalization`](https://www.nuget.org/packages/OrchardCoreContrib.ContentLocalization/1.3.0) | 1.3.0   |
| [`OrchardCoreContrib.ContentLocalization`](https://www.nuget.org/packages/OrchardCoreContrib.ContentLocalization/1.2.0) | 1.2.0   |
| [`OrchardCoreContrib.ContentLocalization`](https://www.nuget.org/packages/OrchardCoreContrib.ContentLocalization/1.1.0) | 1.1.0   |
| [`OrchardCoreContrib.ContentLocalization`](https://www.nuget.org/packages/OrchardCoreContrib.ContentLocalization/1.0.0) | 1.0.0   |

## Get Started

1. Install the [`OrchardCoreContrib.ContentLocalization`](https://www.nuget.org/packages/OrchardCoreContrib.ContentLocalization/) NuGet package to your Orchard Core host project.
2. Go to the admin site
3. Select **Configuration -> Features** menu.

### Localization Matrix

4. Enable the `Localization Matrix` feature.
5. Select **Configuration -> Settings -> Localization -> Localization Matrix** menu.
6. Then you can check the localized content per culture, also you can edit the missing localized content from there.

### Transliteration

4. Enable the `Transliteration` feature.
5. Now you can inject the `ITransliterationService`, then use `Transliterate()` method to transliterate from one script to another.

### Transliteration Liquid
4. Enable the `Liquid` feature.
5. Now you can use the transliteration liquid filters: `cyr_to_lat` and `arab_to_lat`.
6. Usage: `{{ "Иванович" | cyr_to_lat }}`

## Video

[![Watch the video](https://img.youtube.com/vi/14X8fmmnOL8/maxresdefault.jpg)](https://youtu.be/14X8fmmnOL8)

[![Watch the video](https://img.youtube.com/vi/MEmNL5tzezA/maxresdefault.jpg)](https://youtu.be/MEmNL5tzezA)
