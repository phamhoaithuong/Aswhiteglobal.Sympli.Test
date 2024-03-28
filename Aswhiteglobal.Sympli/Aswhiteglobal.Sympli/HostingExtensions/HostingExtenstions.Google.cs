using Sympli.Core.Interfaces;
using Sympli.Infrastructure.Services;
using static Sympli.Core.Options.ApplicationOptions;

namespace Aswhiteglobal.Sympli.HostingExtensions
{
    public static partial class HostingExtensions
    {
        public static WebApplicationBuilder AddGoogle(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var options = builder.Configuration.GetSection("SearchEngine").Get<SearchEngineOptions>();

            builder.Services.AddHttpClient<IGoogleService, GoogleService>(
                client => client.AddGoogleConfiguration(options.Google))
                .SetHandlerLifetime(TimeSpan.FromMinutes(options.Google.ClientLifeTime));


            return builder;
        }

        private static void AddGoogleConfiguration(this HttpClient client, GoogleOptions options)
        {
            client.BaseAddress = new Uri(options.Url);
        }
    }
}
