using Sympli.Core.Enums;

namespace Sympli.Core.Models
{
    public class QuerySEO : SearchRequest
    {
        public string URL { get; set; }
        public List<SearchEngineEnum> SearchEngineTypes { get; set; } = new List<SearchEngineEnum>();
    }
}
