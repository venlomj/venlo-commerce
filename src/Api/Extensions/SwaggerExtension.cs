﻿using Microsoft.OpenApi.Models;

namespace Api.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "VenloCommerce API",
                    Version = "v1",
                    Description = "VenloCommerce is een eCommerce platform voor product-, order- en klantbeheer.",
                    Contact = new OpenApiContact
                    {
                        Name = "Murrel Venlo",
                        Email = "contact@venlocommerce.com",
                        Url = new Uri("https://venlocommerce.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
            });
            return services;
        }

        public static void UseSwaggerDocumentation(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "VenloCommerce API v1");
                c.DocumentTitle = "VenloCommerce API Documentation";
                c.RoutePrefix = "swagger";
            });
        }
    }
}