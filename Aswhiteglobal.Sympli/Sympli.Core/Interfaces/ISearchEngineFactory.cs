using Sympli.Core.Enums;

namespace Sympli.Core.Interfaces
{
    public interface ISearchEngineFactory
    {
        ISearchEngine CreateSearchEngine(SearchEngineEnum searchEngineEnum);
    }
}
