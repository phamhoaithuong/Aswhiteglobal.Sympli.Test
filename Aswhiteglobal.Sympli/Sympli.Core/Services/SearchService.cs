using Sympli.Core.Common.Constants;
using Sympli.Core.Extensions;
using Sympli.Core.Interfaces;
using Sympli.Core.Models;

namespace Sympli.Core.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchEngineFactory _searchEngineFactory;
        private readonly ICacheManager _cacheManager;
        public SearchService(ISearchEngineFactory searchEngineFactory, ICacheManager cacheManager)
        {
            _searchEngineFactory = searchEngineFactory;
            _cacheManager = cacheManager;
        }

        public async Task<List<SEOResult>> QuerySEOAsync(QuerySEO query, CancellationToken token = default)
        {
            var result = new List<SEOResult>();


            foreach (var type in query.SearchEngineTypes)
            {
                var cacheEntry = await _cacheManager.GetDataAsync<SearchResult>(query.ToKey(type), token);
                if (cacheEntry == null)
                {
                    cacheEntry = new SearchResult();
                    var searchEngine = _searchEngineFactory.CreateSearchEngine(type);
                    var searchItems = await searchEngine.SearchAsync(query, token);

                    if (searchItems != null && searchItems.Count > 0)
                    {
                        var searchResult = new SearchResult()
                        {
                            Items = searchItems,
                            Keyword = query.Keyword,
                            SearchEngine = type,
                            Total = query.Total,
                        };

                        await _cacheManager.SetDataAsync<SearchResult>(query.ToKey(type), searchResult, ApplicationConstants.CacheTimeDurationInMinutes, token);
                        cacheEntry = searchResult;
                    }
                }

                var numberResult = cacheEntry.Items.Where(p => p.Link.Contains(query.URL)).Count();

                var seoResult = new SEOResult()
                {
                    SearchType = type,
                    NumberResult = numberResult,
                };
                result.Add(seoResult);
            }

            return result;

        }
    }
}
