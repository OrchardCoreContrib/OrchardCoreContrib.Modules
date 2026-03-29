using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using OrchardCore.Environment.Shell;
using OrchardCoreContrib.Garnet.Tests;

namespace OrchardCoreContrib.Garnet.Services.Tests;

public class GarnetLockTests : TestBase
{
    private static GarnetLock _garnetLock;
    private static IGarnetService _garnetService;
    private static IOptions<GarnetOptions> _garnetOptions;
    private static ShellSettings _shellSettings;

    public override async Task InitializeAsync()
    {
        _garnetOptions = Options.Create(new GarnetOptions());

        _shellSettings = new ShellSettings
        {
            Name = ShellSettings.DefaultShellName
        };

        _garnetService = await Utilities.CreateGarnetServiceAsync();

        _garnetLock = new GarnetLock(
            _garnetService,
            _garnetOptions,
            _shellSettings,
            NullLogger<GarnetLock>.Instance);

        await Task.CompletedTask;
    }

    [Fact]
    public async Task IsLockAcquired_ReturnsFalse_IfLockNotAcquired()
    {
        // Act
        var isAcquired = await _garnetLock.IsLockAcquiredAsync("lockKey1");

        // Assert
        Assert.False(isAcquired);
    }

    [Fact]
    public async Task IsLockAcquired_ReturnsFalse_IfLockIsAcquired()
    {
        // Arrange
        var key = "lockKey2";

        var (_, locked) = await _garnetLock.TryAcquireLockAsync(key, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));

        Assert.True(locked);

        // Act
        var isAcquired = await _garnetLock.IsLockAcquiredAsync(key);

        // Assert
        Assert.True(isAcquired);
    }
}
