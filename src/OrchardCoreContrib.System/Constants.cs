namespace OrchardCoreContrib.System;

/// <summary>
/// Provides a constants for the the System module.
/// </summary>
public class Constants
{
    /// <summary>
    /// Provides constant values related to NuGet package sources and identifiers used for system updates.
    /// </summary>
    public static class SystemUpdates
    {
        /// <summary>
        /// Represents the default NuGet package source URL used for retrieving packages from the official NuGet repository.
        /// </summary>
        public const string NugetPackageSource = "https://api.nuget.org/v3/index.json";

        /// <summary>
        /// Represents the base URL for NuGet package listings on nuget.org.
        /// </summary>
        public const string NuGetPackageUrl = "https://www.nuget.org/packages";

        /// <summary>
        /// Represents the NuGet package identifier for the Orchard Core framework.
        /// </summary>
        public const string OrchardCorePackageId = "OrchardCore";
    }

    /// <summary>
    /// Provides constant values related to system maintenance mode, including the maintenance URL path and default
    /// maintenance page content.
    /// </summary>
    public static class SystemMaintenance
    {
        /// <summary>
        /// Represents the relative URL path used to indicate the application's maintenance mode.
        /// </summary>
        public const string MaintenancePath = "/maintenance";

        /// <summary>
        /// Provides the default HTML content for a maintenance page displayed when the website is offline.
        /// </summary>
        public const string DefaultMaintenancePageContent = @"<div style=""text-align:center;"">
    <h1>Under Maintenance</h1>
    <p>This website is currently offline for maintenance. Please try again later.</p>
</div>";
    }
}
