using Sympli.Core.Interfaces;
using Sympli.Core.Services;
using Sympli.Infrastructure.Services;

namespace Aswhiteglobal.Sympli.HostingExtensions
{
    public static partial class HostingExtensions
    {
        public static WebApplicationBuilder AddDI(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            services.AddTransient<IGoogleService, GoogleService>();
            services.AddTransient<IBingService, BingService>();
            services.AddTransient<ISearchEngineFactory, SearchEngineFactory>();
            services.AddScoped<ICacheManager, CacheManager>();
            services.AddScoped<ISearchService, SearchService>();


            return builder;
        }
    }
}
