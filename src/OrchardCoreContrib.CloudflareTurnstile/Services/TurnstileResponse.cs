namespace OrchardCoreContrib.CloudflareTurnstile.Services;

public class TurnstileResponse
{
    public bool Success { get; set; }

    public string[] ErrorCodes { get; set; }
}
