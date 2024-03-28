using Sympli.Core.Interfaces;
using Sympli.Infrastructure.Services;
using static Sympli.Core.Options.ApplicationOptions;

namespace Aswhiteglobal.Sympli.HostingExtensions
{
    public static partial class HostingExtenstions
    {
        public static WebApplicationBuilder AddBing(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var options = builder.Configuration.GetSection("SearchEngine").Get<SearchEngineOptions>();

            builder.Services.AddHttpClient<IBingService, BingService>(
                client => client.AddBingConfiguration(options.Bing))
                .SetHandlerLifetime(TimeSpan.FromMinutes(options.Bing.ClientLifeTime));


            return builder;
        }

        private static void AddBingConfiguration(this HttpClient client, BingOptions options)
        {
            client.BaseAddress = new Uri(options.Url);
        }
    }
}
