using OrchardCore;
using OrchardCore.Data;
using OrchardCore.Environment.Shell;
using System.Reflection;
using System.Runtime.InteropServices;

namespace OrchardCoreContrib.System.Services;

public class SystemInformation
{
    private readonly ShellSettings _defaultShellSettings;
    private readonly Assembly _executedAssembly;

    public SystemInformation(IShellHost shellHost)
    {
        _executedAssembly = Assembly.GetEntryAssembly();
        _defaultShellSettings = shellHost.GetSettings(ShellHelper.DefaultShellName);
    }

    public string ApplicationName => _executedAssembly.GetName().Name;

    public Version ApplicationVersion => _executedAssembly.GetName().Version;

    public Version OrchardCoreVersion => typeof(IOrchardHelper).Assembly.GetName().Version;

    public string AspNetCoreVersion => RuntimeInformation.FrameworkDescription;

    public string OSVersion => RuntimeInformation.OSDescription;

    public string DatabaseProvider => _defaultShellSettings["DatabaseProvider"] switch
    {
        DatabaseProviderValue.Sqlite => "SQLite",
        DatabaseProviderValue.SqlConnection => "SQL Server",
        DatabaseProviderValue.MySql => "MySQL",
        DatabaseProviderValue.Postgres => "Postgres SQL",
        _ => "Unknown"
    };
}
