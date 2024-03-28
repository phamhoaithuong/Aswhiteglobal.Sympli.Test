using Sympli.Core.Models;

namespace Sympli.Core.Interfaces
{
    public interface ISearchEngine
    {
        List<SearchItem> ExtractInformationFromHtml(string html);
        Task<List<SearchItem>> SearchAsync(SearchRequest searchRequest, CancellationToken token = default);
    }
}
