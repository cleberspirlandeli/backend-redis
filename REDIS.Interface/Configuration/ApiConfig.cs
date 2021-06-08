using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace REDIS.Interface.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfig(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            RedisConfig.StartRedis(services, configuration);

            services.AddControllers().AddNewtonsoftJson();

            services.AddCors(options =>
            {
                options.AddPolicy("Development",
                    builder => builder
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyHeader());

                options.AddPolicy("Production",
                    builder => builder
                        .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                        .WithOrigins("https://meusistema.com.br", "https://meuoutrosistema.com.br")
                        .AllowAnyHeader());
            });

            return services;
        }

        public static IApplicationBuilder UseMvcConfiguration(this IApplicationBuilder app,
            IConfiguration configuration)
        {

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseSwaggerConfig(provider);

            return app;
        }
    }
}
