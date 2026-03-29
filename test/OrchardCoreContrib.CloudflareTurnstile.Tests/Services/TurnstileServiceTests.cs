using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using OrchardCoreContrib.CloudflareTurnstile.Configuration;
using System.Net;
using System.Text;
using System.Text.Json;

namespace OrchardCoreContrib.CloudflareTurnstile.Services.Tests;

public class TurnstileServiceTests
{
    private static readonly IOptions<TurnstileOptions> _turnstileOptions = Options.Create(new TurnstileOptions
    {
        SecretKey = "secret"
    });

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task ValidateAsync_ReturnsBasedOnResponseSuccessResult(bool success)
    {
        // Arrange
        var content = JsonSerializer.Serialize(new TurnstileResponse { Success = success });

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(content , Encoding.UTF8, "application/json")
        };

        var service = new TurnstileService(CreateHttpClientFactory(responseMessage), _turnstileOptions);

        // Act
        var result = await service.ValidateAsync("token");

        // Assert
        Assert.Equal(success, result);
    }

    [Fact]
    public async Task ValidateAsync_ReturnsFalse_WhenApiReturnsNonSuccessStatusCode()
    {
        // Arrange
        var service = new TurnstileService(CreateHttpClientFactory(HttpStatusCode.BadRequest), _turnstileOptions);

        // Act
        var result = await service.ValidateAsync("token");

        // Assert
        Assert.False(result);
    }

    private sealed class CaptureHandler(HttpResponseMessage response) : HttpMessageHandler
    {
        public HttpRequestMessage Request { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Request = request;
            
            return Task.FromResult(response);
        }
    }

    private static IHttpClientFactory CreateHttpClientFactory(HttpResponseMessage httpReponseMessage)
    {
        var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

        httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpReponseMessage);

        var client = new HttpClient(httpMessageHandlerMock.Object);

        var httpClientFactoryMock = new Mock<IHttpClientFactory>();

        httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
            .Returns(client);

        return httpClientFactoryMock.Object;
    }

    private static IHttpClientFactory CreateHttpClientFactory(HttpStatusCode httpStatusCode)
        => CreateHttpClientFactory(new HttpResponseMessage(httpStatusCode));
}
