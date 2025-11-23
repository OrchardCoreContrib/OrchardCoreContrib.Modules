using OrchardCore;
using OrchardCore.Data;
using OrchardCore.Environment.Shell;
using System.Reflection;
using System.Runtime.InteropServices;

namespace OrchardCoreContrib.System.Services;

/// <summary>
/// Provides access to key system and application information, including application name, version details, framework
/// version, operating system description, and database provider.
/// </summary>
public class SystemInformation(IShellHost shellHost)
{
    private readonly ShellSettings _defaultShellSettings = shellHost.GetSettings(ShellSettings.DefaultShellName);
    private readonly Assembly _executedAssembly = Assembly.GetEntryAssembly();

    /// <summary>
    /// Gets the name of the application as defined by the executed assembly.
    /// </summary>
    public string ApplicationName => _executedAssembly.GetName().Name;

    /// <summary>
    /// Gets the version number of the currently executing application.
    /// </summary>
    public Version ApplicationVersion => _executedAssembly.GetName().Version;

    /// <summary>
    /// Gets the version of the Orchard Core framework currently in use.
    /// </summary>
    public Version OrchardCoreVersion => typeof(IOrchardHelper).Assembly.GetName().Version;

    /// <summary>
    /// Gets the description of the ASP.NET Core runtime framework in use.
    /// </summary>
    public string AspNetCoreVersion => RuntimeInformation.FrameworkDescription;

    /// <summary>
    /// Gets the operating system version information for the current platform.
    /// </summary>
    public string OSVersion => RuntimeInformation.OSDescription;

    /// <summary>
    /// Gets the name of the database provider configured for the current shell settings.
    /// </summary>
    public string DatabaseProvider => _defaultShellSettings["DatabaseProvider"] switch
    {
        DatabaseProviderValue.Sqlite => "SQLite",
        DatabaseProviderValue.SqlConnection => "SQL Server",
        DatabaseProviderValue.MySql => "MySQL",
        DatabaseProviderValue.Postgres => "Postgres SQL",
        _ => "Unknown"
    };
}
