using NuGet.Versioning;

namespace OrchardCoreContrib.System.Models;

/// <summary>
/// Represents a system update identified by a specific NuGet package version.
/// </summary>
/// <param name="version">The NuGet package version that uniquely identifies the system update. Cannot be null.</param>
public class SystemUpdate(NuGetVersion version)
{
    /// <summary>
    /// Gets the version information for the current instance.
    /// </summary>
    public Version Version => version.Version;

    /// <inheritdoc/>
    public override string ToString() => version.ToFullString();
}
