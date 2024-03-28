using Sympli.Core.Models;

namespace Sympli.Core.Interfaces
{
    public interface ISearchService
    {
        Task<List<SEOResult>> QuerySEOAsync(QuerySEO query, CancellationToken token = default);
    }
}
