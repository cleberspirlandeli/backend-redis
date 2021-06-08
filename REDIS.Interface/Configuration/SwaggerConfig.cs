using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REDIS.Interface.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Taxa de Juros API",
                    Description = "Returno de taxa de juros atualizada",
                    TermsOfService = new Uri("https://github.com/cleberspirlandeli/backend-redis"),
                    Contact = new OpenApiContact
                    {
                        Name = "Cleber Rezende",
                        Email = "contato.spirlandeli@gmail.com",
                        Url = new Uri("https://github.com/cleberspirlandeli/backend-redis"),
                    },
                    
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            return app;
        }
    }
}
