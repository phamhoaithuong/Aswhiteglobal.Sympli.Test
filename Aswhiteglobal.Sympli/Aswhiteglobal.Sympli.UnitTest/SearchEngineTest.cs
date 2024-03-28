using Microsoft.Extensions.Logging;
using Moq;
using Sympli.Infrastructure.Services;

namespace Aswhiteglobal.Sympli.UnitTest
{
    public class SearchEngineTest
    {

        [Fact]
        public void Test_GoogleExtractInformationFromHtml_WhenHTMLResponse_ShouldCorrectly()
        {
            // Arrange
            var _logger = new Mock<ILogger<GoogleService>>();
            var _client = new Mock<HttpClient> ();

            var service = new GoogleService(_logger.Object, _client.Object);

            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(directory, "DataTest", "Google10.html");
            var html = File.ReadAllText(path);

            // Act
            var result = service.ExtractInformationFromHtml(html);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(10,result.Count());    
        }

        [Fact]
        public void Test_BingExtractInformationFromHtml_WhenHTMLResponse_ShouldCorrectly()
        {
            // Arrange
            var _logger = new Mock<ILogger<BingService>>();
            var _client = new Mock<HttpClient>();

            var service = new BingService(_logger.Object, _client.Object);

            var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var path = Path.Combine(directory, "DataTest", "Bing10.html");
            var html = File.ReadAllText(path);

            // Act
            var result = service.ExtractInformationFromHtml(html);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(10, result.Count());
        }
    }
}
