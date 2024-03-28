using Microsoft.Extensions.Logging;
using Sympli.Core.Exceptions;
using Sympli.Core.Interfaces;
using Sympli.Core.Models;
using System.Net;
using System.Text.RegularExpressions;

namespace Sympli.Infrastructure.Services
{
    public class GoogleService : IGoogleService
    {
        private readonly ILogger<GoogleService> _logger;
        private readonly HttpClient _client;
        public GoogleService(ILogger<GoogleService> logger, HttpClient client)
        {
            _logger = logger;
            _client = client;
        }
        public async Task<List<SearchItem>> SearchAsync(SearchRequest searchRequest, CancellationToken token = default)
        {
            var searchUrl = $"search?num={searchRequest.Total + 1}&q={Uri.EscapeDataString(searchRequest.Keyword ?? "")}";
            var response = await _client.GetAsync(searchUrl, token);
            if(response.StatusCode != HttpStatusCode.OK)
            {
                throw new GoogleErrorException("Can not get Google result.");
            }

            var html = await response.Content.ReadAsStringAsync();

            return ExtractInformationFromHtml(html);
        }

        public List<SearchItem> ExtractInformationFromHtml(string html)
        {
            var searchResult = new List<SearchItem>();
            string pattern = @"<div[^>]*?>(.*?)</div>";
            MatchCollection matches = Regex.Matches(html, pattern, RegexOptions.Singleline);

            foreach (Match match in matches)
            {
                var itemHtml = match.Groups[1].Value.Trim();

                string hrefPattern = @"href=\""(.*?)\""";

                Match hrefMatch = Regex.Match(itemHtml, hrefPattern, RegexOptions.Singleline);


                string patternTitle = @"<h3[^>]*><div[^>]*>(.*?)<\/div>";
                var titleMatch = Regex.Match(match.Groups[0].Value.Trim(), patternTitle);
                if (titleMatch.Success && hrefMatch.Success)
                {
                    string href = hrefMatch.Groups[1].Value;
                    var titleHtml = titleMatch.Groups[1].Value;
                    var title = WebUtility.HtmlDecode(titleHtml);

                    var searchItem = new SearchItem()
                    {
                        Id = Guid.NewGuid(),
                        Title = title,
                        Link = href,
                    };
                    searchResult.Add(searchItem);
                }
            }

            return searchResult;
        }
    }
}
