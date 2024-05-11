using Garnet;

namespace OrchardCoreContrib.Garnet.Tests;

public abstract class TestBase : IAsyncLifetime
{
    private static readonly GarnetServer _garnetServer;

    static TestBase()
    {
        _garnetServer = new([]);
        _garnetServer.Start();
    }

    public virtual async Task InitializeAsync() => await Task.CompletedTask;

    public virtual async Task DisposeAsync() => await Task.CompletedTask;
}
