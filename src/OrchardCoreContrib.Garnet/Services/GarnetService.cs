using Garnet.client;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;

namespace OrchardCoreContrib.Garnet.Services;

/// <summary>
/// Represents the Garnet service.
/// </summary>
/// <param name="factory">The <see cref="IGarnetClientFactory"/>.</param>
/// <param name="options">The <see cref="IOptions{GarnetOptions}"/>.</param>
public class GarnetService(IGarnetClientFactory factory, IOptions<GarnetOptions> options) : ModularTenantEvents, IGarnetService
{
    private readonly GarnetOptions _garnetOptions = options.Value;

    /// <inheritdoc/>
    public GarnetClient Client { get; private set; }

    /// <inheritdoc/>
    public string InstancePrefix => _garnetOptions.InstancePrefix;

    /// <inheritdoc/>
    public async Task ConnectAsync() => Client ??= await factory.CreateAsync(_garnetOptions);

    /// <inheritdoc/>
    public override async Task ActivatingAsync() => await ConnectAsync();
}
