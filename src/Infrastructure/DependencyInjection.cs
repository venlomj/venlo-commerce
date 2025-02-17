using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories.Products;
using Domain.UoW;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Products;
using Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductsReaderRepository, ProductsReaderRepository>();
            services.AddTransient<IProductWriterRepository, ProductWriterRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<VenloCommerceDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("VenloCommerceConnection"));
            });

            return services;
        }
    }
}
