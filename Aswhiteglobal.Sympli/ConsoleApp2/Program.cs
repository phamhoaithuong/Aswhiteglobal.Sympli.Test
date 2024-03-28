using System.Net;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main()
        {
            string searchQuery = "openai";
            int resultsPerPage = 10;
            int currentPage = 1;
            int totalResults = 100;
            var index = 1;
            WebClient client = new WebClient();
            //for (int i = 1; i <= numResults; i += 10)
            //{
            //    string bingUrl = $"https://www.bing.com/search?q={Uri.EscapeDataString(query)}&first={i}";

            //    // Tải nội dung của trang
            //    string html = client.DownloadString(bingUrl);

            //    // Biểu thức chính quy để tìm các kết quả tìm kiếm trong HTML của trang Bing 
            //    string regexPattern = @"<h2><a href=""([^""]+)""[^>]*>(.*?)<\/a><\/h2>";

            //    // Tìm các kết quả tìm kiếm bằng regex
            //    MatchCollection matches = Regex.Matches(html, regexPattern);

            //    // In ra các kết quả tìm kiếm
            //    foreach (Match match in matches)
            //    {
            //        string title = match.Groups[2].Value;
            //        string link = match.Groups[1].Value;

            //        Console.WriteLine("Title: " + WebUtility.HtmlDecode(title));
            //        Console.WriteLine("URL: " + link);
            //        Console.WriteLine("===============");
            //    }
            //}
            while (totalResults > 0)
            {
                string url = $"https://www.bing.com/search?q={WebUtility.UrlEncode(searchQuery)}&first={(currentPage - 1) * resultsPerPage + 1}";
                string html = client.DownloadString(url);
                string regexPattern = @"<h2><a href=""([^""]+)""[^>]*>(.*?)<\/a><\/h2>";

                MatchCollection matches = Regex.Matches(html, regexPattern);
                if (matches.Count == 0) break;
                foreach (Match match in matches)
                {
                    string title = match.Groups[2].Value;
                    string link = match.Groups[1].Value;
                    Console.WriteLine("index: " + index);
                    Console.WriteLine("Title: " + WebUtility.HtmlDecode(title));
                    Console.WriteLine("URL: " + link);
                    Console.WriteLine("===============");
                    index++;
                    totalResults--;
                    if (totalResults == 0)
                        break;
                }
                currentPage++;
            }

            client.Dispose();


        }

        static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }
    }
}
