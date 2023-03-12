# SendGrid Module

This module allows you to send emails using SendGrid service.

## Version

1.2.0

## Category

Messaging

## Dependencies

| Product | Module |
| --- | --- |
| OrchardCore | Email (`OrchardCore.Email`) |

## Features

| Name | Description |
| --- | --- |
| SendGrid (`OrchardCoreContrib.Email.SendGrid`) | Allow you to send email(s) via SendGrid service. |

## NuGet Packages

| Name | Version |
| --- | --- |
| [`OrchardCoreContrib.Email.SendGrid`](https://www.nuget.org/packages/OrchardCoreContrib.Email.SendGrid/1.3.0) | 1.3.0 |
| [`OrchardCoreContrib.Email.SendGrid`](https://www.nuget.org/packages/OrchardCoreContrib.Email.SendGrid/1.2.0) | 1.2.0 |
| [`OrchardCoreContrib.Email.SendGrid`](https://www.nuget.org/packages/OrchardCoreContrib.Email.SendGrid/1.1.0) | 1.1.0 |
| [`OrchardCoreContrib.Email.SendGrid`](https://www.nuget.org/packages/OrchardCoreContrib.Email.SendGrid/1.0.0) | 1.0.0 |

## Get Started

1. Install the [`OrchardCoreContrib.Email.SendGrid`](https://www.nuget.org/packages/OrchardCoreContrib.Email.SendGrid/) NuGet package to your Orchard Core host project.
2. Go to the admin site
3. Select **Configuration -> Features** menu.
4. Enable the `SendGrid` feature.
5. Configure the SendGrid settings by selecting **Configuration -> Settings -> SendGrid** menu.

### Health Checks

1. Go to the admin site
2. Select **Configuration -> Features** menu.
3. Enable the `HealthChecks` feature.
4. Navigate to `/{tenant}/health/sendgrid`, now you will be able to check whether the SendGrid service with the supplied settings is healthy or not.
