using Aswhiteglobal.Sympli.IntegrationTests.Extensions;
using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Sympli.Core.Enums;
using Sympli.Core.Models;
using System.Net.Mime;
using System.Text;

namespace Aswhiteglobal.Sympli.IntegrationTests
{
    public class SearchControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly IFixture _fixture;
        public SearchControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _fixture = new Fixture(); 
        }

        [Fact]
        public async Task GivenQuerySEORequest_WhenQuerySEO_ThenReturnSuccess()
        {
            // Arrange
            HttpClient client = _factory.CreateClient();
            var url = $"{client.BaseAddress}api/Search";
            var command = _fixture.Create<QuerySEO>();
            string stringContent = JsonConvert.SerializeObject(command);

            // Act
            HttpResponseMessage responseMessage = await client.PostAsync(url, new StringContent(stringContent, Encoding.UTF8, MediaTypeNames.Application.Json));

            // Assert
            Assert.True(responseMessage.IsSuccessStatusCode);
        }


        [Theory]
        [InlineData(SearchEngineEnum.Google)]
        [InlineData(SearchEngineEnum.Bing)]
        public async Task GivenQuerySEOWithEngineEngineRequest_WhenQuerySEO_ThenReturnSuccess(SearchEngineEnum searchEngine)
        {
            // Arrange
            HttpClient client = _factory.CreateClient();
            var url = $"{client.BaseAddress}api/Search";
            var command = _fixture.Create<QuerySEO>();
            command.SearchEngineTypes = [searchEngine];
            
            string stringContent = JsonConvert.SerializeObject(command);

            // Act
            HttpResponseMessage responseMessage = await client.PostAsync(url, new StringContent(stringContent, Encoding.UTF8, MediaTypeNames.Application.Json));

            // Assert
            Assert.True(responseMessage.IsSuccessStatusCode);

            var results = await responseMessage.DeserializeContentAsync<List<SEOResult>>();
            Assert.NotNull(results);
            Assert.Single(results);
            Assert.Equal(searchEngine, results[0].SearchType);
        }


    }
}
