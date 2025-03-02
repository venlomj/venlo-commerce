using System.Reflection;
using Application.DTOs.Products;
using Application.Services;
using Application.UseCases.Products.Commands;
using Application.Validators;
using Domain.Entities;
using Domain.Repositories.Categories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Add MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            // Register FluentValidation and the MediatR pipeline behavior
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Add transient services
            services.AddTransient<IImageService, ImageService>();

            // Register command-specific validators
            services.AddTransient<IValidator<AddProductCommand>, AddProductCommandValidator>();

            return services;
        }

    }
}