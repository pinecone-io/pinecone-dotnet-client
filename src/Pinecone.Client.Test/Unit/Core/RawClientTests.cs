using System.Net;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using Pinecone.Client.Core;

namespace Pinecone.Client.Test.Unit.Core
{
    [TestFixture]
    public class RawClientTests
    {
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private HttpClient _httpClient;
        private ClientOptions _clientOptions;
        private Dictionary<string, string> _headers;
        private Dictionary<string, Func<string>> _headerSuppliers;
        private RawClient _rawClient;

        [SetUp]
        public void SetUp()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://api.example.com")
            };
            _clientOptions = new ClientOptions
            {
                HttpClient = _httpClient,
                BaseUrl = "https://api.example.com"
            };
            _headers = new Dictionary<string, string> { { "Authorization", "Bearer token" } };
            _headerSuppliers = new Dictionary<string, Func<string>>();
            _rawClient = new RawClient(_headers, _headerSuppliers, _clientOptions);
        }

        [Test]
        public async Task MakeRequestAsync_ShouldReturnResponse_ForJsonApiRequest()
        {
            var request = new RawClient.JsonApiRequest
            {
                BaseUrl = _clientOptions.BaseUrl,
                Method = HttpMethod.Post,
                Path = "/test",
                Body = new { Name = "Test" }
            };

            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"message\":\"success\"}")
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(httpResponse);

            var response = await _rawClient.MakeRequestAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(response.Raw, Is.EqualTo(httpResponse));
        }

        [Test]
        public async Task MakeRequestAsync_ShouldReturnResponse_ForStreamApiRequest()
        {
            var request = new RawClient.StreamApiRequest
            {
                BaseUrl = _clientOptions.BaseUrl,
                Method = HttpMethod.Put,
                Path = "/upload",
                Body = new MemoryStream(new byte[] { 1, 2, 3 })
            };

            var httpResponse = new HttpResponseMessage(HttpStatusCode.Created);

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(httpResponse);

            var response = await _rawClient.MakeRequestAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(response.Raw, Is.EqualTo(httpResponse));
        }

        [Test]
        public async Task MakeRequestAsync_ShouldAddGlobalHeaders()
        {
            var request = new RawClient.JsonApiRequest
            {
                BaseUrl = _clientOptions.BaseUrl,
                Method = HttpMethod.Get,
                Path = "/headers"
            };

            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK);

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(msg =>
                        msg.Headers.Contains("Authorization")
                        && msg.Headers.GetValues("Authorization").Contains("Bearer token")
                    ),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(httpResponse);

            var response = await _rawClient.MakeRequestAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(response.Raw, Is.EqualTo(httpResponse));
        }

        [Test]
        public async Task MakeRequestAsync_ShouldBuildCorrectUrl_WithQueryParameters()
        {
            var request = new RawClient.JsonApiRequest
            {
                BaseUrl = _clientOptions.BaseUrl,
                Method = HttpMethod.Get,
                Path = "/test",
                Query = new Dictionary<string, object>
                {
                    { "param1", "value1" },
                    { "param2", "value2" }
                }
            };

            var httpResponse = new HttpResponseMessage(HttpStatusCode.OK);

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(msg =>
                        msg.RequestUri != null
                        && msg.RequestUri.ToString()
                            == "https://api.example.com/test?param1=value1&param2=value2"
                    ),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(httpResponse);

            var response = await _rawClient.MakeRequestAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(response.Raw, Is.EqualTo(httpResponse));
        }
    }
}
