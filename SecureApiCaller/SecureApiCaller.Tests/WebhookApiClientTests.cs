namespace SecureApiCaller.Tests
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Moq;
    using Moq.Protected;
    using Xunit;

    public class WebhookApiClientTests
    {
        [Fact]
        public async Task SendsPostRequest_AndReturnsResponseContent()
        {
            // Arrange
            var expectedResponse = "OK from webhook";

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(expectedResponse)
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);

            var client = new WebhookApiClient(httpClient);

            // Act
            var result = await client.SendTestRequestAsync();

            // Assert
            Assert.Equal(expectedResponse, result);

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Post &&
                    req.RequestUri!.ToString().Contains("webhook.site")
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public void ThisTestShouldFail()
        {
            Assert.Fail("Intentional failure to verify test runner behavior");
        }

    }

}
