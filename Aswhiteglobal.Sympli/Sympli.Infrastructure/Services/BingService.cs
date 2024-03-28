using Microsoft.Extensions.Logging;
using Sympli.Core.Common;
using Sympli.Core.Exceptions;
using Sympli.Core.Interfaces;
using Sympli.Core.Models;
using System.Net;
using System.Text.RegularExpressions;

namespace Sympli.Infrastructure.Services
{
    public class BingService : IBingService
    {
        private readonly ILogger<BingService> _logger;
        private readonly HttpClient _client;
        public BingService(ILogger<BingService> logger, HttpClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<List<SearchItem>> SearchAsync(SearchRequest searchRequest, CancellationToken token = default)
        {
            var searchResult = new List<SearchItem>();
            var resultsPerPage = 10;
            var currentPage = 1;
            var totalResults = searchRequest.Total;

            while (totalResults > 0)
            {
                string searchUrl = $"https://www.bing.com/search?q={Uri.EscapeDataString(searchRequest.Keyword ?? "")}&first={(currentPage - 1) * resultsPerPage + 1}";
                var response = await _client.GetAsync(searchUrl, token);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new BingErrorException("Can not get Bing result.");
                }

                var html = await response.Content.ReadAsStringAsync();
                var searchItem = ExtractInformationFromHtml(html);

                if (searchItem.Count == 0) break;

                searchResult.AddRange(ExtractInformationFromHtml(html));

                totalResults -= searchItem.Count;
                if (totalResults == 0) break;
            }

            return searchResult.Take(searchRequest.Total).ToList();
        }

        public List<SearchItem> ExtractInformationFromHtml(string html)
        {
            var searchResult = new List<SearchItem>();
            string pattern = @"<h2><a href=""([^""]+)""[^>]*>(.*?)<\/a><\/h2";
            MatchCollection matches = Regex.Matches(html, pattern);

            foreach (Match match in matches)
            {
                string title = StringHelper.StripTagsRegex(match.Groups[2].Value ?? "");
                string link = match.Groups[1].Value;

                var searchItem = new SearchItem()
                {
                    Id = Guid.NewGuid(),
                    Title = title,
                    Link = link,
                };
                searchResult.Add(searchItem);
            }

            return searchResult;
        }
    }
}