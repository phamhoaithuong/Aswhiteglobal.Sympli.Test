using Sympli.Core.Enums;
using Sympli.Core.Models;

namespace Sympli.Core.Extensions
{
    public static class QuerySEOExtensions
    {
        public static string ToKey(this QuerySEO querySEO, SearchEngineEnum searchEngine)
        {
            return $"{querySEO.Keyword}:{searchEngine}:{querySEO.Total}";
        }
    }
}
