using OrchardCore.Modules;
using OrchardCoreContrib.Testing;
using ReflectionAssembly = System.Reflection.Assembly;

namespace OrchardCoreContrib.HealthChecks.Tests;

internal sealed class HealthChecksModuleNamesProvider : IModuleNamesProvider
{
    private static readonly string[] DefaultExcludedModulePrefixes = ["OrchardCoreContrib.Email."];
    private readonly ModuleNamesProvider _moduleNamesProvider;
    private readonly IReadOnlyList<string> _excludedModulePrefixes;

    public HealthChecksModuleNamesProvider(ReflectionAssembly assembly, IReadOnlyList<string> excludedModulePrefixes = null)
    {
        _moduleNamesProvider = new ModuleNamesProvider(assembly);
        _excludedModulePrefixes = excludedModulePrefixes ?? DefaultExcludedModulePrefixes;
    }

    public IEnumerable<string> GetModuleNames()
        // Orchard Core 3 removed ISmtpService; excluding legacy email modules avoids TypeLoadException in unrelated test hosts.
        => _moduleNamesProvider.GetModuleNames().Where(name => !_excludedModulePrefixes.Any(name.StartsWith));
}
