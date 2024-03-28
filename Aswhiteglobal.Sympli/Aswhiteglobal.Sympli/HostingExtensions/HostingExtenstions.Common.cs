using Aswhiteglobal.Sympli.Filters;
using Sympli.Core.Common.Constants;
using Sympli.Core.Options;

namespace Aswhiteglobal.Sympli.HostingExtensions
{
    public static partial class HostingExtensions
    {
        public static WebApplicationBuilder AddCommon(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            services.Configure<ApplicationOptions>(builder.Configuration);
            services.AddMemoryCache();
            services.AddHttpClient();
            services.AddControllers(opt => { opt.Filters.Add<ApiExceptionFilterAttribute>(); });

            services.AddCors(o => o.AddPolicy(ApplicationConstants.AllowSpecificOrigins, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            return builder;
        }
    }
}
