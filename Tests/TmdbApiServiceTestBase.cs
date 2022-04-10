using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tests.ApiResponses;
using TMDB_demo.Models.TMDB_models;

namespace Tests
{
    public class TmdbApiServiceTestBase
    {
        protected Mock<IHttpClientFactory> _httpClientFactoryMock;
        protected IConfiguration Configuration;

        [SetUp]
        public void Setup()
        { 
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            SetUpConfiguration();
        }

        protected void SetUpHttpClientFactoryMock<T>()
        {
            var httpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(ChooseTestData<T>())
                })
                .Verifiable();

            var httpClient = new HttpClient(httpMessageHandler.Object);
            httpClient.BaseAddress = new Uri("https://tmdb.org");
            _httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(httpClient).Verifiable();
        }

        private static string ChooseTestData<T>() {
            if (typeof(T) == typeof(TmdbMovie)) return MovieTestData.GetJson();

            throw new NotImplementedException($"Invalid type {typeof(T).Name}");
        }

        private void SetUpConfiguration() {
            var configuration = new Dictionary<string, string> {
                { "Tmdb:ApiVersion", "3"},
                { "Tmdb:ApiKey", "api_key"}
            };

            Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configuration)
                .Build();
        }
    }
}
