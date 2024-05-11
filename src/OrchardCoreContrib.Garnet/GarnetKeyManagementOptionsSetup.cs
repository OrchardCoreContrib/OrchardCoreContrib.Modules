using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Options;
using OrchardCore.Environment.Shell;
using OrchardCoreContrib.Garnet.Services;

namespace OrchardCoreContrib.Garnet;

/// <summary>
/// Represents a setup to configure the <see cref="KeyManagementOptions"/> for Garnet data protection feature.
/// </summary>
/// <param name="garnetService">The <see cref="IGarnetService"/>.</param>
/// <param name="shellSettings">The <see cref="ShellSettings"/>.</param>
public class GarnetKeyManagementOptionsSetup(IGarnetService garnetService, ShellSettings shellSettings)
    : IConfigureOptions<KeyManagementOptions>
{
    private readonly string _tenant = shellSettings.Name;

    /// <inheritdoc/>
    public void Configure(KeyManagementOptions options)
    {
        options.XmlRepository = new GarnetXmlRepository(() =>
        {
            if (garnetService.Client == null)
            {
                garnetService.ConnectAsync().GetAwaiter().GetResult();
            }

            return garnetService.Client;
        }, $"({garnetService.InstancePrefix}{_tenant}:DataProtection-Keys");
    }
}