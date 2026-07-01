namespace OrchardCoreContrib.Ban.Models;

public class BanSettings
{
    public string[] BannedIPs { get; set; } = [];
    
    public string? RedirectUrl { get; set; }
}
