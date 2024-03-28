using static Sympli.Core.Options.ApplicationOptions;

namespace Aswhiteglobal.Sympli.HostingExtensions
{
    public static partial class HostingExtenstions
    {
        public static WebApplicationBuilder AddRedis(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var redisOptions = builder.Configuration.GetSection("Redis").Get<RedisOptions>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName = redisOptions.InstanceName;
                options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
                {
                    Password = redisOptions.Password,
                    EndPoints =
                    {
                        redisOptions.Host
                    },
                };
            });

            return builder;
        }
    }
}
