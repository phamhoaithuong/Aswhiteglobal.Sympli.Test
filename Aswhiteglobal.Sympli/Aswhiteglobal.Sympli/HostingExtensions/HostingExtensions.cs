using Sympli.Core.Common.Constants;

namespace Aswhiteglobal.Sympli.HostingExtensions
{
    public static partial class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder
                .AddCommon()
                .AddDI()
                .AddGoogle()
                .AddBing()
                .AddRedis();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors(ApplicationConstants.AllowSpecificOrigins);
            app.MapControllers();
            return app;
        }
    }
}
