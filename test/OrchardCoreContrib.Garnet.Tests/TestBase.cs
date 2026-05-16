using Garnet;
using System.Net;
using System.Net.Sockets;

namespace OrchardCoreContrib.Garnet.Tests;

public abstract class TestBase : IAsyncLifetime
{
    private static readonly GarnetServer _garnetServer;

    public static int Port { get; private set; }

    static TestBase()
    {
        const int maxAttempts = 5;

        for (var attempt = 0; attempt < maxAttempts; attempt++)
        {
            Port = FindFreePort();
            try
            {
                _garnetServer = new(["--port", Port.ToString()]);
                _garnetServer.Start();

                return;
            }
            catch (SocketException) when (attempt < maxAttempts - 1)
            {
                // Port may have been taken between FindFreePort and GarnetServer.Start; retry.
            }
        }

        throw new InvalidOperationException($"Unable to start GarnetServer after {maxAttempts} attempts.");
    }

    public virtual async Task InitializeAsync() => await Task.CompletedTask;

    public virtual async Task DisposeAsync() => await Task.CompletedTask;

    private static int FindFreePort()
    {
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();

        return port;
    }
}
