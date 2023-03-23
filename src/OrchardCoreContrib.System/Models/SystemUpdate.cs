using NuGet.Versioning;

namespace OrchardCoreContrib.System.Models;

public class SystemUpdate
{
    private readonly NuGetVersion _version;
    
    public SystemUpdate(NuGetVersion version)
    {
        _version = version;
    }

    public Version Version => _version.Version;

    public override string ToString() => _version.ToFullString();
}
