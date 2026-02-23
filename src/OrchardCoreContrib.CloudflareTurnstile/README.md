# Cloudflare Turnstile Module

This module allows you to protect your forms by Cloudflare Turnstile Captcha.

## Version

1.0.0

## Category

Security

## Dependencies

This module has no dependencies.

## Features

|                  |                                                                                            |
|------------------|---------------------------------------------------------------|
| **Name**         | Cloudflare Turnstile (`OrchardCoreContrib.CloudflareTurnstile`) |
| **Description**  | Provides Cloudflare Turnstile Captcha.  |
| **Dependencies** |	                               							   |

## NuGet Packages

| Name																													| Version     |
|-----------------------------------------------------------------------------------------------------------------------|-------------|
| [`OrchardCoreContrib.CloudflareTurnstile`](https://www.nuget.org/packages/OrchardCoreContrib.CloudflareTurnstile/1.0.0) | 1.0.0       |

## Get Started

1. Install the [`OrchardCoreContrib.CloudflareTurnstile`](https://www.nuget.org/packages/OrchardCoreContrib.CloudflareTurnstile/) NuGet package to your Orchard Core host project.
2. Go to the admin site
3. Select **Configuration -> Features** menu.
4. Enable the `Cloudflare Turnstile` feature.
5. Now you can protect your forms as the following:

    5.1. In your view place the `Turnstile` shape to render the Cloudflare Turnstile widget

    ```html
    <form method="post">
        @Html.AntiForgeryToken()
        <input type="text" name="Email" type="email" class="form-control mb-2" />
        <shape Type="Turnstile"></shape>
        <button type="submit" class="btn btn-primary mt-2">Submit</button>
    </form>
    ```

    5.2.  In the controller you need to inject the `TurnstileService` to validate the token, that comes from the form

    ```csharp
    [Route("/subscribe")]
    [HttpPost]
    public async Task<IActionResult> Index(
        [FromForm(Name = Constants.TurnstileServerResponseHeaderName)] string token,
        [FromServices] TurnstileService captcha)
    {
        if (!await captcha.ValidateAsync(token))
        {
            return BadRequest("Captcha failed");
        }
    
        // Proceed with form logic
    
        return Ok("Success");
    }
    ```
