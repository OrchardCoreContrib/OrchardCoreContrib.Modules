namespace OrchardCoreContrib.CloudflareTurnstile.Settings;

public class TurnstileSettings
{
    public string SiteKey { get; set; }

    public string SecretKey { get; set; }

    /// <summary>
    /// Theme options: "light", "dark", "auto"
    /// </summary>
    public string Theme { get; set; } = "light";

    /// <summary>
    /// Size options: "normal", "compact"
    /// </summary>
    public string Size { get; set; } = "normal";
}

