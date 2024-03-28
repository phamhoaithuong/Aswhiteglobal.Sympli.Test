using Sympli.Core.Enums;

namespace Sympli.Core.Models
{
    public class SearchResult
    {
        public string Keyword { get; set; }
        public int Total { get; set; }
        public SearchEngineEnum SearchEngine { get; set; }
        public List<SearchItem> Items { get; set; } = new List<SearchItem>();
    }
}
