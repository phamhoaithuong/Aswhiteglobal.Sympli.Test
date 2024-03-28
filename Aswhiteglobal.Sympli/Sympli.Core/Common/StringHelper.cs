using System.Text.RegularExpressions;

namespace Sympli.Core.Common
{
    public static class StringHelper
    {
        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }
    }
}
