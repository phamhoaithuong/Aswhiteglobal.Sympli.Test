using Microsoft.Extensions.DependencyInjection;
using Sympli.Core.Enums;
using Sympli.Core.Interfaces;

namespace Sympli.Infrastructure.Services
{
    public class SearchEngineFactory : ISearchEngineFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SearchEngineFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ISearchEngine CreateSearchEngine(SearchEngineEnum searchEngineEnum)
        {
            switch (searchEngineEnum)
            {
                case SearchEngineEnum.Google:
                    return _serviceProvider.GetRequiredService<IGoogleService>();
                case SearchEngineEnum.Bing:
                    return _serviceProvider.GetRequiredService<IBingService>();

                default:
                    throw new ArgumentException($"Unknown product type: {searchEngineEnum}", nameof(SearchEngineEnum));
            }
        }
    }
}
