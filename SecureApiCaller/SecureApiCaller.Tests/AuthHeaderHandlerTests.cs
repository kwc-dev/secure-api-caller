namespace SecureApiCaller.Tests
{
    using Microsoft.Extensions.Configuration;
    using Moq;
    using Moq.Protected;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class AuthHeaderHandlerTests
    {
        [Fact]
        public async Task AddsAuthorizationHeader_FromConfiguration()
        {
            // Arrange
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c["api-token-kwc"]).Returns("test-token");

            var innerHandler = new Mock<HttpMessageHandler>();
            innerHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK))
                .Verifiable();

            var handler = new AuthHeaderHandler(configMock.Object)
            {
                InnerHandler = innerHandler.Object
            };

            var client = new HttpClient(handler);

            // Act
            var response = await client.GetAsync("https://example.com");

            // Assert
            innerHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Headers.Authorization != null &&
                    req.Headers.Authorization.Scheme == "Bearer" &&
                    req.Headers.Authorization.Parameter == "test-token"
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }
    }

}
