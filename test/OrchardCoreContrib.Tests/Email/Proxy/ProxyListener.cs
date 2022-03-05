using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Email.Tests
{
    /// <summary>
    /// Reference: https://github.com/jstedfast/MailKit/blob/a5637f9d547f5d9a171f01c62ee753b13371a22c/UnitTests/Net/Proxy/ProxyListener.cs
    /// </summary>
    internal abstract class ProxyListener : IDisposable
    {
        CancellationTokenSource cancellationTokenSource;
        TcpListener listener;
        Task runningTask;
        bool disposed;

        protected ProxyListener()
        {
        }

        ~ProxyListener()
        {
            Dispose(false);
        }

        public bool IsRunning
        {
            get; private set;
        }

        public IPAddress IPAddress
        {
            get; private set;
        }

        public int Port
        {
            get; private set;
        }

        public void Start(IPAddress addr, int port)
        {
            listener = new TcpListener(addr, port);
            listener.Start(1);

            IPAddress = ((IPEndPoint)listener.LocalEndpoint).Address;
            Port = ((IPEndPoint)listener.LocalEndpoint).Port;

            cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;

            runningTask = Task.Run(() => AcceptProxyConnections(token), token);
            IsRunning = true;
        }

        public void Stop()
        {
            if (listener != null && IsRunning)
            {
                try
                {
                    cancellationTokenSource.Cancel();
                }
                catch
                {
                }

                try
                {
                    listener.Stop();
                }
                catch
                {
                }

                try
                {
                    runningTask.GetAwaiter().GetResult();
                }
                catch
                {
                }

                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
                IsRunning = false;
            }
        }

        protected abstract Task<Socket> ClientCommandReceived(Stream client, byte[] buffer, int length, CancellationToken cancellationToken);

        protected virtual Task<Stream> GetClientStreamAsync(Socket socket, CancellationToken cancellationToken)
        {
            Stream stream = new NetworkStream(socket, true);

            return Task.FromResult(stream);
        }

        async Task AcceptProxyConnection(Socket socket, CancellationToken cancellationToken)
        {
            using (var client = await GetClientStreamAsync(socket, cancellationToken).ConfigureAwait(false))
            {
                var buffer = new byte[4096];
                Socket remote = null;
                int nread;

                while ((nread = await client.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) > 0)
                {
                    try
                    {
                        remote = await ClientCommandReceived(client, buffer, nread, cancellationToken).ConfigureAwait(false);
                        if (remote != null)
                            break;
                    }
                    catch
                    {
                        return;
                    }
                }

                if (remote == null)
                    return;

                try
                {
                    using (var server = new NetworkStream(remote, true))
                    {
                        do
                        {
                            while (socket.Available > 0 || server.DataAvailable)
                            {
                                if (socket.Available > 0)
                                {
                                    if ((nread = await client.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) > 0)
                                        await server.WriteAsync(buffer, 0, nread, cancellationToken).ConfigureAwait(false);
                                }

                                if (server.DataAvailable)
                                {
                                    if ((nread = await server.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) > 0)
                                        await client.WriteAsync(buffer, 0, nread, cancellationToken).ConfigureAwait(false);
                                }
                            }

                            var checkRead = new ArrayList();
                            checkRead.Add(socket);
                            checkRead.Add(remote);

                            var checkError = new ArrayList();
                            checkError.Add(socket);
                            checkError.Add(remote);

                            Socket.Select(checkRead, null, checkError, 250000);

                            cancellationToken.ThrowIfCancellationRequested();
                        } while (socket.Connected && remote.Connected);
                    }
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                catch (SocketException)
                {
                    return;
                }
            }
        }

        async Task AcceptProxyConnections(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using (var socket = await listener.AcceptSocketAsync().ConfigureAwait(false))
                        await AcceptProxyConnection(socket, cancellationToken).ConfigureAwait(false);
                }
                catch (ObjectDisposedException)
                {
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                Stop();
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
