using System.ComponentModel.DataAnnotations;

namespace OrchardCoreContrib.CloudflareTurnstile.ViewModels;

public class TurnstileSettingsViewModel
{
    [Required]
    public string SiteKey { get; set; }

    [Required]
    public string SecretKey { get; set; }

    public string Theme { get; set; }

    public string Size { get; set; }
}

