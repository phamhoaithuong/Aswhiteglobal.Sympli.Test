namespace Sympli.Core.Options
{
    public class ApplicationOptions
    {
        public SearchEngineOptions SearchEngine { get; set; }

        public class SearchEngineOptions
        {
            public GoogleOptions Google { get; set; }
            public BingOptions Bing { get; set; }
            public RedisOptions Redis { get; set; }
        }
        public class GoogleOptions
        {
            public string Url { get; set; }
            public int RetryCount { get; set; }
            public int ClientLifeTime { get; set; }
        }

        public class BingOptions
        {
            public string Url { get; set; }
            public int RetryCount { get; set; }
            public int ClientLifeTime { get; set; }
        }

        public class RedisOptions
        {
            public string Host { get; set; }
            public string Password { get; set; }
            public string InstanceName { get; set; }
        }
    }

}
