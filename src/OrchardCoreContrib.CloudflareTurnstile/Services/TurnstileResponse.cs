namespace OrchardCoreContrib.CloudflareTurnstile.Services;

public record TurnstileResponse (bool Success, string[] ErrorCodes);
